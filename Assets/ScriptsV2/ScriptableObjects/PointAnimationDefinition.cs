using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Point Animation Definition", menuName = "Alexa Run/Point Animation Definition")]
    class PointAnimationDefinition : ScriptableObject
    {
        public float baseAnimationCycleTime = 0f;
    }
}
