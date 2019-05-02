using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AlexaRun.Interfaces;
using AlexaRun.Enums;

namespace AlexaRun.Behaviours
{
    /// <summary>
    /// A behaviour for tracking the state of rooms, based on the Failable Point Behaviours they contain
    /// </summary>
    public class RoomBehaviour : MonoBehaviour, IBehaviourStateObservable
    {
        [SerializeField] private List<FailablePointBehaviour> trackedPoints = new List<FailablePointBehaviour>();
        [SerializeField] private UnityEvent onStateChange = new UnityEvent();
        [SerializeField] [ReadOnly] private EBehaviourState roomState = EBehaviourState.OK;

        [SerializeField] [ReadOnly] private bool isEnabled = true;

        public void Awake() {
            for (int i = 0; i < trackedPoints.Count; i++) {
                trackedPoints[i].SubscribeToStateChange(OnPointStateChange);
            }
        }

        public EBehaviourState GetBehaviourState() {
            return roomState;
        }

        public void SetEnabled(bool enabled) {
            isEnabled = enabled;
            for (int i = 0; i < trackedPoints.Count; i++) {
                trackedPoints[i].SetEnabled(enabled);
            }
        }

        public void SubscribeToStateChange(UnityAction listener) {
            onStateChange.AddListener(listener);
        }

        public void UnsubscribeFromStateChange(UnityAction listener) {
            onStateChange.RemoveListener(listener);
        }

        private void OnPointStateChange() {
            if (roomState == EBehaviourState.FAILED || isEnabled == false) return;

            int failCount = 0;
            bool failing = false;
            for (int i = 0; i < trackedPoints.Count; i++) {
                EBehaviourState pointState = trackedPoints[i].GetBehaviourState();
                
                if (pointState == EBehaviourState.FAILING) {
                    failing = true;
                } else if (pointState == EBehaviourState.FAILED) {
                    failCount++;
                }
            }

            if (failCount == trackedPoints.Count) {
                // All points in room failed, kill room
                roomState = EBehaviourState.FAILED;
                onStateChange.Invoke();
            } else if (roomState == EBehaviourState.OK && failing) {
                // Room became failing
                roomState = EBehaviourState.FAILING;
                onStateChange.Invoke();
            } else if (roomState == EBehaviourState.FAILING && !failing) {
                // Room was failing and is OK now
                roomState = EBehaviourState.OK;
                onStateChange.Invoke();
            }
        }
    }
}
