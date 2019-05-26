using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Auto Supply Point Defintion", menuName = "Alexa Run/Auto Supply Point Definition")]
    public class AutoSupplyPointDefinition : ScriptableObject
    {
        public ItemDefinition suppliedItem;
        public float baseAccumulationPerSecond = 1.0f;
        public int maxStackItemCount = 10;
        public int initialStackItemCount = 10;
    }
}