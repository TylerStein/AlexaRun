using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AlexaRun.ScriptableObjects;
using AlexaRun.Interfaces;
using AlexaRun.Behaviours.Player;
using AlexaRun.Level;
namespace AlexaRun.Behaviours
{
    class SupplyPointBehaviour : MonoBehaviour, IPointBehaviour
    {
        [SerializeField] private Transform stackPositionRoot;
        [SerializeField] private ItemObjectPool itemPool;
        [SerializeField] private SupplyPointDefinition definition;
        [SerializeField] private Stack<ItemBehaviour> itemStack = new Stack<ItemBehaviour>();
    
        private float supplyCounter = 0f;

        public bool OnInteract(PlayerBehaviour player) {
            if (itemStack.Count > 0 && player.HasStackSpace()) {
                player.PushItemStack(popItemFromStack());
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

        private void updatePointState(float deltaTime) {
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
            item.transform.SetParent(stackPositionRoot);
            item.transform.localPosition = item.ItemDefinition.GenerateItemStackPosition(itemStack.Count, true);
            item.transform.localRotation = item.ItemDefinition.GenerateItemStackRotation(true);
            itemStack.Push(item);
        }
    }
}
