using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AlexaRun.Interfaces;
using AlexaRun.Level;
using AlexaRun.ScriptableObjects;
using AlexaRun.Behaviours.Player;

namespace AlexaRun.Behaviours
{
    class DemandPointBehaviour : MonoBehaviour, IPointBehaviour
    {
        [SerializeField] private Transform stackPositionRoot;
        [SerializeField] private ItemObjectPool itemPool;
        [SerializeField] private DemandPointDefinition definition;
        [SerializeField] private Stack<ItemBehaviour> itemStack = new Stack<ItemBehaviour>();

        private float consumeCounter = 0f;

        public bool OnInteract(PlayerBehaviour player) {
            ItemBehaviour item = player.PeekItemStack();
            if (itemStack.Count < definition.maxStackItems && item != null && definition.demandItem == item.ItemDefinition) {
                pushItemToStack(player.PopItemStack());
                return true;
            }

            return false;
        }

        public GameObject GetGameObject() {
            return gameObject;
        }

        private void Update() {
            updatePointState(Time.deltaTime);
        }

        private void Awake() {
            if (stackPositionRoot == null) stackPositionRoot = transform;
            initializeStackContent();
        }

        private void initializeStackContent() {
            int pendingItemCount = definition.initialStackItemCount - itemStack.Count;
            if (pendingItemCount > 0) {
                for (int i = 0; i < pendingItemCount; i++) {
                    ItemBehaviour newItem;
                    itemPool.SpawnFromPool(out newItem, definition.demandItem.itemName);
                    if (newItem == null) throw new UnityException(string.Format("Failed to initialize demand stack {0} with item {1}!", gameObject.name, definition.demandItem.name));
                    pushItemToStack(newItem);
                }
            }
        }

        private void updatePointState(float deltaTime) {
            float timeTarget = 1.0f / definition.baseDepletionPerSecond;
            consumeCounter += deltaTime;
            while (consumeCounter >= timeTarget) {
                consumeCounter -= timeTarget;
                removeItemFromStack();
            }
        }

        private void removeItemFromStack(bool invokeMessage = true) {
            if (itemStack.Count == 0) {
                handleEmptyStack();
            } else {
                itemPool.ReturnToPool(itemStack.Pop());
            }
        }

        private void pushItemToStack(ItemBehaviour item) {
            item.transform.SetParent(stackPositionRoot);
            item.transform.localPosition = item.ItemDefinition.GenerateItemStackPosition(itemStack.Count, true);
            item.transform.localRotation = item.ItemDefinition.GenerateItemStackRotation(true);
            itemStack.Push(item);
        }

        private void handleEmptyStack() {
            //
        }
    }
}
