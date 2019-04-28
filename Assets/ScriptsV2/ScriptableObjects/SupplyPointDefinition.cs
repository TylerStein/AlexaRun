using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Supply Point Defintion", menuName = "Alexa Run/Supply Point Definition")]
    public class SupplyPointDefinition : ScriptableObject
    {
        public ItemDefinition suppliedItem;
        public float baseAccumulationPerSecond;
    }
}