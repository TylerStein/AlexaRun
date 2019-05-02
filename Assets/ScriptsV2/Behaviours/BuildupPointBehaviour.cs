using UnityEngine;
using UnityEngine.Events;
using AlexaRun.ScriptableObjects;
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
        [SerializeField] private BuildupPointDefinition definition = null;
        [SerializeField] private UnityEvent onStateChange = new UnityEvent();
        [SerializeField] [ReadOnly] private EBehaviourState state = EBehaviourState.OK;
        [SerializeField] [ReadOnly] bool isEnabled = true;

        public override bool OnInteract(PlayerBehaviour player) {
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
    }
}
