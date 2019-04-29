using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Supply Point Defintion", menuName = "Alexa Run/Supply Point Definition")]
    public class SupplyPointDefinition : ScriptableObject
    {
        public ItemDefinition suppliedItem;
        public float baseAccumulationPerSecond = 1.0f;
        public int maxStackItemCount = 10;
        public int initialStackItemCount = 10;
    }
}