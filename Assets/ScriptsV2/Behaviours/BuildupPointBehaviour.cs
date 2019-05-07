using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AlexaRun.ScriptableObjects;
using AlexaRun.Level;
using AlexaRun.Global;
using AlexaRun.Interfaces;
using AlexaRun.Behaviours.Player;
using AlexaRun.Enums;

namespace AlexaRun.Behaviours
{
    /// <summary>
    /// A Failable Point Behaviour that builds up a stack of items over time and can fail after the stack maxes out
    /// </summary>
    class BuildupPointBehaviour : FailablePointBehaviour
    {
        [SerializeField] private Transform stackPositionRoot = null;
        [SerializeField] private ItemObjectPool itemPool = null;
        [SerializeField] private BuildupPointDefinition definition = null;
        [SerializeField] private UnityEvent onStateChange = new UnityEvent();
        [SerializeField] private Stack<ItemBehaviour> itemStack = new Stack<ItemBehaviour>();
        [SerializeField] [ReadOnly] private float pointTimer = 0;
        [SerializeField] [ReadOnly] private EBehaviourState state = EBehaviourState.OK;
        [SerializeField] [ReadOnly] bool isEnabled = true;

        public override bool OnInteract(PlayerBehaviour player) {
            if (isEnabled && player.HasStackSpace() && itemStack.Count > 0) {
                player.PushItemStack(removeItemFromStack());
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
                    pushItemToStack();
                }
            }
        }

        private void updatePointBehaviour(float deltaTime) {
            if (state == EBehaviourState.FAILED || isEnabled == false) {
                return;
            } else if (state == EBehaviourState.OK) {
                float timeTarget = 1.0f / definition.baseAccumulationPerSecond;
                pointTimer += deltaTime;
                while (pointTimer >= timeTarget) {
                    pointTimer -= timeTarget;
                    pushItemToStack();
                }
            } else if (state == EBehaviourState.FAILING) {
                pointTimer += Time.deltaTime;
                if (pointTimer >= definition.baseFailStateTimeout) {
                    state = EBehaviourState.FAILED;
                    onStateChange.Invoke();
                }
            }
        }

        private ItemBehaviour removeItemFromStack() {
            ItemBehaviour item = itemStack.Pop();

            if (state == EBehaviourState.FAILING && itemStack.Count < definition.maxStackItemCount) {
                state = EBehaviourState.OK;
                pointTimer = 0;
                onStateChange.Invoke();
            }

            return item;
        }

        private void pushItemToStack() {
            ItemBehaviour item;
            itemPool.SpawnFromPool(out item, definition.accumulatingItem.itemName);
            item.SetSpriteLayer(Settings.instance.itemSortingLayer);
            item.SetSpriteLayerOrder(itemStack.Count + 1);
            item.transform.SetParent(stackPositionRoot);
            item.transform.localPosition = item.ItemDefinition.GenerateItemStackPosition(itemStack.Count, true);
            item.transform.localRotation = item.ItemDefinition.GenerateItemStackRotation(true);
            itemStack.Push(item);

            if (state == EBehaviourState.OK && itemStack.Count >= definition.maxStackItemCount) {
                state = EBehaviourState.FAILING;
                pointTimer = 0;
                onStateChange.Invoke();
            }
        }
    }
}
