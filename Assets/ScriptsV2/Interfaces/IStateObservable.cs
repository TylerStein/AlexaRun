using UnityEngine.Events;
using AlexaRun.Enums;

namespace AlexaRun.Interfaces
{
    /// <summary>
    /// Implement to provide observable access to an EBehaviourState
    /// </summary>
    public interface IBehaviourStateObservable
    {
        void SubscribeToStateChange(UnityAction listener);
        void UnsubscribeFromStateChange(UnityAction listener);
        EBehaviourState GetBehaviourState();
    }
}
