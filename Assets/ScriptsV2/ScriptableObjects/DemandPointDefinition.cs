using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Demand Point Definition", menuName = "Alexa Run/Demand Point Definition")]
    public class DemandPointDefinition : ScriptableObject
    {
        public ItemDefinition demandItem;
        public float baseDepletionPerSecond = 1.0f;
        public float baseFailStateTimeout = 2.0f;
        public int maxStackItems = 10;
        public int initialStackItemCount = 10;
    }
}