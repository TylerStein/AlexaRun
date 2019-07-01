using System.Collections.Generic;
using UnityEngine;
using AlexaRun.ScriptableObjects;
using AlexaRun.Interfaces;
using AlexaRun.Behaviours.Player;
using AlexaRun.Level;
using AlexaRun.Global;

namespace AlexaRun.Behaviours
{
    /// <summary>
    /// A Point Behaviour that builds up a stack of items over time, up to a limit
    /// </summary>
    class AutoSupplyPointBehaviour : PointBehaviour
    {
        [SerializeField] private Transform stackPositionRoot = null;
        [SerializeField] private ItemObjectPool itemPool = null;
        [SerializeField] private LevelBehaviour levelBehaviour = null;
        [SerializeField] private AutoSupplyPointDefinition definition = null;
        [SerializeField] private PointAnimationDefinition animationDefinition = null;
        [SerializeField] private Animator animator = null;
        [SerializeField] private Stack<ItemBehaviour> itemStack = new Stack<ItemBehaviour>();
        [SerializeField] private Transform nearestHighlightParent;
        [SerializeField] [ReadOnly] private bool isEnabled = true;
        [SerializeField] [ReadOnly] private float secondsPerChange = 0f;
        [SerializeField] [ReadOnly] private float pointTimer = 0f;

        public override bool OnInteract(PlayerBehaviour player) {
            if (isEnabled && itemStack.Count > 0 && player.HasStackSpace()) {
                player.PushItemStack(popItemFromStack());
                return true;
            }

            return false;
        }

        public override void SetEnabled(bool enabled) {
            isEnabled = enabled;
            updateSpeed();
        }

        private void Update() {
            updatePointBehaviour(Time.deltaTime);
        }

        private void Awake() {
            if (stackPositionRoot == null) stackPositionRoot = transform;
            updateSpeed();
            initializeStackContent();
            levelBehaviour = LevelBehaviour.Instance();
            levelBehaviour.pauseBehaviour.SubscribeToPauseState(SetEnabled);
        }

        private void updateSpeed() {
            if (!isEnabled && levelBehaviour.pauseBehaviour.IsPaused) {
                animator.SetFloat("speed", 0);
                return;
            }

            secondsPerChange = 1.0f / definition.baseAccumulationPerSecond;
            secondsPerChange /= Settings.Persistent.DifficultyScale;

            float animationSpeed = animationDefinition.baseAnimationCycleTime / secondsPerChange;

            animator.SetInteger("state", isEnabled ? 1 : 0);
            animator.SetFloat("speed", animationSpeed);
        }

        private void updatePointBehaviour(float deltaTime) {
            if (isEnabled == false) return;

            pointTimer += deltaTime;
            while (pointTimer >= secondsPerChange) {
                pointTimer -= secondsPerChange;
                pushNewItemToStack();
            }
        }

        private void initializeStackContent() {
            int pendingItemCount = definition.initialStackItemCount - itemStack.Count;
            if (pendingItemCount > 0) {
                for (int i = 0; i < pendingItemCount; i++) {
                    pushNewItemToStack();
                }
            }
        }

        private ItemBehaviour popItemFromStack() {
            ItemBehaviour item = itemStack.Pop();
            return item;
        }

        private void pushNewItemToStack() {
            if (itemStack.Count >= definition.maxStackItemCount) {
                animator.SetInteger("state", 0);
                return;
            }
            animator.SetInteger("state", 1);
            ItemBehaviour newItem;
            itemPool.SpawnFromPool(out newItem, definition.suppliedItem.itemName);
            if (newItem == null) throw new UnityException(string.Format("Failed to initialize supply stack {0} with item {1}!", gameObject.name, definition.suppliedItem.name));
            pushItemToStack(newItem);
        }

        private void pushItemToStack(ItemBehaviour item) {
            if (itemStack.Count >= definition.maxStackItemCount) return;
            item.SetSpriteLayer(Settings.Constant.itemSortingLayer);
            item.SetSpriteLayerOrder(itemStack.Count + 1);
            item.transform.SetParent(stackPositionRoot);
            item.transform.localPosition = item.ItemDefinition.GenerateItemStackPosition(itemStack.Count, true);
            item.transform.localRotation = item.ItemDefinition.GenerateItemStackRotation(true);
            itemStack.Push(item);
        }
    }
}
