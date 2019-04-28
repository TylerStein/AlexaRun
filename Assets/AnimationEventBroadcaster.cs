using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AnimationEvent : UnityEvent<string> { }

public class AnimationEventBroadcaster : MonoBehaviour
{
    public AnimationEvent animationEvent = new AnimationEvent();
    public AnimationEvent audioEvent = new AnimationEvent();

    public void BroadcastAnimationEvent(string name) {
        animationEvent.Invoke(name);
    }

    public void BroadcastAudioEvent(string name) {
        audioEvent.Invoke(name);
    }
}
