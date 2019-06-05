using UnityEngine;
using UnityEngine.Events;
using System.Collections.Generic;
using AlexaRun.ScriptableObjects;
using AlexaRun.Interfaces;
using AlexaRun.Global;
using AlexaRun.Level;

namespace AlexaRun.Behaviours.Player
{
    /// <summary>
    /// Core PlayerBehaviour class, handles the player's held stack as well as interaction between various Player Controller scripts
    /// </summary>
    public class PlayerBehaviour : MonoBehaviour
    {
        [SerializeField] private InputController inputController = null;
        [SerializeField] private PlayerPointProximityController proximityController = null;
        [SerializeField] private PlayerAnimationController animationController = null;
        [SerializeField] private PlayerSoundEffectController soundEffectController = null;
        [SerializeField] private PlayerSettings playerSettings = null;
        [SerializeField] private Stack<ItemBehaviour> itemStack = new Stack<ItemBehaviour>();
        [SerializeField] private Transform stackHoldRoot = null;
        [SerializeField] private LevelBehaviour levelBehaviour = null;

        private bool isEnabled = true;

        public UnityEvent OnFailedInteraction = new UnityEvent();
        public UnityEvent OnSuccessfulInteraction = new UnityEvent();

        public void SetGameOver() {
            while (itemStack.Count > 0) PopItemStack();
            animationController.SetGameOver(true);
            SetEnabled(false);
        }

        public void SetEnabled(bool enabled) {
            if (enabled == false) {
                animationController.SetMovement(0);
                soundEffectController.SetMovement(0);
                animationController.UpdateAnimatorValues();
            } else {
                animationController.SetGameOver(false);
            }

            isEnabled = enabled;
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
            item.SetSpriteLayer(Settings.Constant.heldSortingLayer);
            item.SetSpriteLayerOrder(itemStack.Count + 1);
            item.transform.parent = stackHoldRoot;
            item.transform.localPosition = GetNextStackPosition();// item.ItemDefinition.GenerateItemStackPosition(itemStack.Count, true);
            item.transform.localRotation = item.ItemDefinition.GenerateItemStackRotation(true);
            itemStack.Push(item);
            return true;
        }

        public Vector3 GetNextStackPosition() {
            Vector3 offset = Vector3.zero;
            foreach (ItemBehaviour item in itemStack) {
                offset = item.ItemDefinition.GenerateItemStackOffset(offset.y);
            }
            return offset;
        }

        private void Awake() {
            if (stackHoldRoot == null) stackHoldRoot = transform;
            levelBehaviour = LevelBehaviour.Instance();
            levelBehaviour.pauseBehaviour.SubscribeToPauseState((isPaused) => SetEnabled(isPaused));
        }

        private void Update() {
            if (inputController.Pause) {
                SetEnabled(false);
                levelBehaviour.pauseBehaviour.TogglePause();
            }

            if (isEnabled == false) return;

            if (inputController.Interact) {
                PointBehaviour nearestPoint = proximityController.NearestPoint;
                if (nearestPoint != null) {
                    bool success = nearestPoint.OnInteract(this);
                    if (success) OnSuccessfulInteraction.Invoke();
                    else OnFailedInteraction.Invoke();
                } else {
                    OnFailedInteraction.Invoke();
                }
            }

            Vector3 projectedPosition = transform.position + (inputController.HorizontalMove * Vector3.right * Time.deltaTime * playerSettings.baseMetersPerSecond);
            projectedPosition.x = Mathf.Clamp(projectedPosition.x, levelBehaviour.Boundaries.minX, levelBehaviour.Boundaries.maxX);
            transform.Translate(projectedPosition - transform.position);

            soundEffectController.SetMovement(inputController.HorizontalMove);
            animationController.SetMovement(inputController.HorizontalMove);
            animationController.SetStackSize(itemStack.Count);
            animationController.UpdateAnimatorValues();
        }
    }
}
