using UnityEngine;
using UnityEngine.Events;
using AlexaRun.ScriptableObjects;
using AlexaRun.Interfaces;
using AlexaRun.Behaviours.Player;
using AlexaRun.Level;

namespace AlexaRun.Behaviours
{
    /// <summary>
    /// A Point Behaviour that builds up a stack of items over time, up to a limit
    /// </summary>
    class SupplyPointBehaviour : PointBehaviour
    {
        [SerializeField] private Transform stackPositionRoot = null;
        [SerializeField] private ItemObjectPool itemPool = null;
        [SerializeField] private SupplyPointDefinition definition = null;
        [SerializeField] private LevelBehaviour levelBehaviour = null;
        [SerializeField] [ReadOnly] private bool isEnabled = true;
        [SerializeField] [ReadOnly] private float supplyPointCooldownTimer = 0f;

        [SerializeField] public UnityEvent OnSupply = new UnityEvent();
        [SerializeField] public UnityEvent OnAvailable = new UnityEvent();

        public override bool OnInteract(PlayerBehaviour player) {
            if (isEnabled && player.HasStackSpace()) {
                player.PushItemStack(spawnNewItem());
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

        private void Awake() {
            if (stackPositionRoot == null) stackPositionRoot = transform;
            levelBehaviour = LevelBehaviour.Instance();
            levelBehaviour.pauseBehaviour.SubscribeToPauseState(SetEnabled);
        }

        private void updatePointBehaviour(float deltaTime) {
            if (isEnabled == false) return;

            if (supplyPointCooldownTimer > 0f) {
                supplyPointCooldownTimer -= deltaTime;
                if (supplyPointCooldownTimer <= 0f) {
                    supplyPointCooldownTimer = 0;
                    OnAvailable.Invoke();
                }
            }
        }

        private ItemBehaviour spawnNewItem() {
            ItemBehaviour newItem;
            itemPool.SpawnFromPool(out newItem, definition.suppliedItem.itemName);
            if (newItem == null) throw new UnityException(string.Format("Failed to initialize supply stack {0} with item {1}!", gameObject.name, definition.suppliedItem.name));
            supplyPointCooldownTimer = definition.cooldown;
            OnSupply.Invoke();
            return newItem;
        }
    }
}
