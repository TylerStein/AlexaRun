using UnityEngine;
using UnityEngine.Events;
using AlexaRun.ScriptableObjects;
using AlexaRun.Interfaces;
using AlexaRun.Behaviours.Player;

namespace AlexaRun.Behaviours
{
    class DisposalPointBehaviour : MonoBehaviour, IPointBehaviour
    {
        [SerializeField] private DisposalPointDefinition definition;
        public UnityEvent OnDispose;

        public bool OnInteract(PlayerBehaviour player) {
            ItemBehaviour item = player.PeekItemStack();
            if (item != null && definition.IsItemAllowed(item.ItemDefinition)) {
                disposeItem(player.PopItemStack());
                return true;
            }

            return false;
        }

        public GameObject GetGameObject() {
            return gameObject;
        }

        private void disposeItem(ItemBehaviour item) {
            OnDispose.Invoke();
            Destroy(item.gameObject);
        }
    }
}
