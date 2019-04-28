using UnityEngine;
using AlexaRun.Structures;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Modifier Point Definition", menuName = "Alexa Run/Modifier Point Definition")]
    public class ModifierPointDefinition : ScriptableObject
    {
        public NumberedItemSet inputItems;
        public ItemDefinition outputItem;
        public float baseConversionDelaySeconds;
    }
}