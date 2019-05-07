using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Camera Follow Settings", menuName = "Alexa Run/Camera Follow Settings")]
    public class CameraFollowSettings : ScriptableObject
    {
        public float HorizontalSpeed = 1.0f;
        public float VerticalSpeed = 1.0f;
    }
}
