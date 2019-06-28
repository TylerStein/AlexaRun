using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using AlexaRun.Enums;
using AlexaRun.Interfaces;

public class PointStateListenerBehaviour : MonoBehaviour
{
    [SerializeField] public EBehaviourState listenForState = EBehaviourState.FAILED;
    [SerializeField] public FailablePointBehaviour linkedBehaviour = null;

    [SerializeField] public UnityEvent onStateMatch = new UnityEvent();

    private void Start() {
        linkedBehaviour.SubscribeToStateChange(OnStateChange);
    }

    public void OnStateChange() {
        if (linkedBehaviour.GetBehaviourState() == listenForState) onStateMatch.Invoke();
    }
}
