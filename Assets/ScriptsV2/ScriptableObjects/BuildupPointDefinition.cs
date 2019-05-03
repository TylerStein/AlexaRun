using UnityEngine;

using AlexaRun.Interfaces;
namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Buildup Point Defenition", menuName = "Alexa Run/Buildup Point Definition")]
    public class BuildupPointDefinition : ScriptableObject
    {
        public ItemDefinition accumulatingItem;
        public int initialStackItemCount;
        public int maxStackItemCount;
        public float baseAccumulationPerSecond;
        public float baseFailStateTimeout;
    }
}