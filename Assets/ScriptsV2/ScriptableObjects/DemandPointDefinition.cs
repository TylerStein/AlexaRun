using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Demand Point Definition", menuName = "Alexa Run/Demand Point Definition")]
    public class DemandPointDefinition : ScriptableObject
    {
        public ItemDefinition demandItems;
        public float baseDepletionPerSecond;
    }
}