using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using AlexaRun.ScriptableObjects;
using AlexaRun.Interfaces;

namespace AlexaRun.Behaviours.Player
{
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private PlayerInputController inputController;
        [SerializeField] private PlayerPointProximityController proximityController;
        [SerializeField] private PlayerSettings playerSettings;
        [SerializeField] private Stack<ItemBehaviour> itemStack = new Stack<ItemBehaviour>();

        public UnityEvent OnFailedInteraction = new UnityEvent();
        public UnityEvent OnSuccessfulInteraction = new UnityEvent();

        private void Update() {
            if (inputController.Interact) {
                IPointBehaviour nearestPoint = proximityController.NearestPoint;
                if (nearestPoint != null) {
                    bool success = nearestPoint.OnInteract(this);
                    if (success) OnSuccessfulInteraction.Invoke();
                    else OnFailedInteraction.Invoke();
                } else {
                    OnFailedInteraction.Invoke();
                }
            }

            transform.Translate(inputController.HorizontalMove * Vector3.right * Time.deltaTime * playerSettings.baseMetersPerSecond);
        }

        public ItemBehaviour PeekItemStack() {
            if (itemStack.Count == 0) return null;
            else return itemStack.Peek();
        }

        public ItemBehaviour PopItemStack() {
            if (itemStack.Count == 0) return null;
            else return itemStack.Pop();
        }

        public bool HasStackSpace() {
            return itemStack.Count < playerSettings.holdStackLimit;
        }

        public bool PushItemStack(ItemBehaviour item) {
            if (itemStack.Count >= playerSettings.holdStackLimit) return false;
            item.transform.parent = transform;
            item.transform.localPosition = item.ItemDefinition.GenerateItemStackPosition(itemStack.Count, true);
            item.transform.localRotation = item.ItemDefinition.GenerateItemStackRotation(true);
            itemStack.Push(item);
            return true;
        }
    }
}
