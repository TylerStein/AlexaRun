using UnityEngine;
using UnityEngine.Events;
using AlexaRun.ScriptableObjects;
using AlexaRun.Interfaces;
using AlexaRun.Behaviours.Player;

namespace AlexaRun.Behaviours
{
    /// <summary>
    /// A Point Behaviour that consumes a given range of items with a cooldown between consumptions
    /// </summary>
    class DisposalPointBehaviour : PointBehaviour
    {
        [SerializeField] public UnityEvent OnDisposal = new UnityEvent();
        [SerializeField] public UnityEvent OnAvailable = new UnityEvent();

        [SerializeField] private DisposalPointDefinition definition = null;
        [SerializeField] [ReadOnly] private float disposalCooldownTimer = 0f;
        [SerializeField] [ReadOnly] private bool isEnabled = true;

        public override bool OnInteract(PlayerBehaviour player) {
            ItemBehaviour item = player.PeekItemStack();
            if (isEnabled && item != null && disposalCooldownTimer == 0f && definition.IsItemAllowed(item.ItemDefinition)) {
                disposeItem(player.PopItemStack());
                return true;
            }

            return false;
        }

        public override void SetEnabled(bool enabled) {
            isEnabled = enabled;
        }

        private void Update() {
            updatePointBehaviour(Time.deltaTime);
        }

        private void updatePointBehaviour(float deltaTime) {
            if (isEnabled == false) return;

            if (disposalCooldownTimer > 0f) {
                disposalCooldownTimer -= deltaTime;
                if (disposalCooldownTimer <= 0f) {
                    disposalCooldownTimer = 0f;
                    OnAvailable.Invoke();
                }
            }
        }

        private void disposeItem(ItemBehaviour item) {
            disposalCooldownTimer = definition.cooldown;
            OnDisposal.Invoke();
            Destroy(item.gameObject);
        }
    }
}
