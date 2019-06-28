using UnityEngine.Events;
using AlexaRun.Enums;

namespace AlexaRun.Interfaces
{
    /// <summary>
    /// A PointBehaviour with a fail state, tracked with an added observable EBehaviourState
    /// </summary>
    public abstract class FailablePointBehaviour : PointBehaviour, IBehaviourStateObservable
    {
        public abstract void SubscribeToStateChange(UnityAction listener);
        public abstract void UnsubscribeFromStateChange(UnityAction listener);
        public abstract EBehaviourState GetBehaviourState();

        protected FailablePointBehaviour() : base() {
            //
        }
    }
}
