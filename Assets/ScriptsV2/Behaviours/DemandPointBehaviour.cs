using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AlexaRun.Interfaces;
using AlexaRun.Level;
using AlexaRun.ScriptableObjects;
using AlexaRun.Behaviours.Player;
using AlexaRun.Enums;
using AlexaRun.Global;
using AlexaRun.Behaviours.UI;

namespace AlexaRun.Behaviours
{
    /// <summary>
    /// A Failable Point Behaviour that drains a stack of items and can fail after the stack is empty
    /// </summary>
    class DemandPointBehaviour : FailablePointBehaviour
    {
        [SerializeField] private Transform stackPositionRoot = null;
        [SerializeField] private ItemObjectPool itemPool = null;
        [SerializeField] private DemandPointDefinition definition = null;
        [SerializeField] private Stack<ItemBehaviour> itemStack = new Stack<ItemBehaviour>();
        [SerializeField] private UnityEvent onStateChange = new UnityEvent();
        [SerializeField] [ReadOnly] private float pointTimer = 0f;
        [SerializeField] [ReadOnly] private EBehaviourState state = EBehaviourState.OK;
        [SerializeField] [ReadOnly] bool isEnabled = true;

        [SerializeField] public ProgressBarBehaviour progressBarBehaviour;

        public override bool OnInteract(PlayerBehaviour player) {
            ItemBehaviour item = player.PeekItemStack();
            if (isEnabled && state != EBehaviourState.FAILED 
                && itemStack.Count < definition.maxStackItems 
                && item != null
                && definition.demandItem == item.ItemDefinition) {
                pushItemToStack(player.PopItemStack());
                return true;
            }

            return false;
        }

        public override void SetEnabled(bool enabled) {
            isEnabled = enabled;
        }

        public override void SubscribeToStateChange(UnityAction listener) {
            onStateChange.AddListener(listener);
        }

        public override void UnsubscribeFromStateChange(UnityAction listener) {
            onStateChange.RemoveListener(listener);
        }

        public override EBehaviourState GetBehaviourState() {
            return state;
        }

        private void Update() {
            updatePointBehaviour(Time.deltaTime);
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
            progressBarBehaviour.SetTicks(itemStack.Count);
        }

        private void updatePointBehaviour(float deltaTime) {
            if (state == EBehaviourState.FAILED || isEnabled == false) {
                return;
            } else if (state == EBehaviourState.OK) {
                float timeTarget = 1.0f / definition.baseDepletionPerSecond;
                pointTimer += deltaTime;
                while (pointTimer >= timeTarget) {
                    pointTimer -= timeTarget;
                    removeItemFromStack();
                }
            } else if (state == EBehaviourState.FAILING) {
                pointTimer += Time.deltaTime;
                if (pointTimer >= definition.baseFailStateTimeout) {
                    state = EBehaviourState.FAILED;
                    onStateChange.Invoke();
                }
            }
        }

        private void removeItemFromStack(bool invokeMessage = true) {
            int stackCount = itemStack.Count;
            if (stackCount > 0) itemPool.ReturnToPool(itemStack.Pop());

            if ((stackCount - 1) <= 0 && state != EBehaviourState.FAILING) {
                state = EBehaviourState.FAILING;
                pointTimer = 0;
                onStateChange.Invoke();
            }
            progressBarBehaviour.SetTicks(itemStack.Count);
        }

        private void pushItemToStack(ItemBehaviour item) {
            item.SetSpriteLayer(Settings.instance.itemSortingLayer);
            item.SetSpriteLayerOrder(itemStack.Count + 1);
            item.transform.SetParent(stackPositionRoot);
            item.transform.localPosition = item.ItemDefinition.GenerateItemStackPosition(itemStack.Count, true);
            item.transform.localRotation = item.ItemDefinition.GenerateItemStackRotation(true);
            itemStack.Push(item);
            if (state == EBehaviourState.FAILING) {
                state = EBehaviourState.OK;
                pointTimer = 0;
                onStateChange.Invoke();
            }
            progressBarBehaviour.SetTicks(itemStack.Count);
        }
    }
}
