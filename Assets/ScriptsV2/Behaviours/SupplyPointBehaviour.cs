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
    class SupplyPointBehaviour : PointBehaviour
    {
        [SerializeField] private Transform stackPositionRoot = null;
        [SerializeField] private ItemObjectPool itemPool = null;
        [SerializeField] private SupplyPointDefinition definition = null;
        [SerializeField] private Stack<ItemBehaviour> itemStack = new Stack<ItemBehaviour>();
        [SerializeField] [ReadOnly] private bool isEnabled = true;
    
        private float supplyCounter = 0f;

        public override bool OnInteract(PlayerBehaviour player) {
            if (isEnabled && itemStack.Count > 0 && player.HasStackSpace()) {
                player.PushItemStack(popItemFromStack());
                return true;
            }

            return false;
        }

        public override void SetEnabled(bool enabled) {
            isEnabled = enabled;
        }

        private void Update() {
            updatePointState(Time.deltaTime);
        }

        private void Awake() {
            if (stackPositionRoot == null) stackPositionRoot = transform;
            initializeStackContent();
        }

        private void updatePointState(float deltaTime) {
            if (isEnabled == false) return;

            float timeTarget = 1.0f / definition.baseAccumulationPerSecond;
            supplyCounter += deltaTime;
            while (supplyCounter >= timeTarget) {
                supplyCounter -= timeTarget;
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
            if (itemStack.Count >= definition.maxStackItemCount) return;
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
