using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Buildup Point Defenition", menuName = "Alexa Run/Buildup Point Definition")]
    public class BuildupPointDefinition : ScriptableObject
    {
        public ItemDefinition accumulatingItem;
        public float baseAccumulationPerSecond;
    }
}