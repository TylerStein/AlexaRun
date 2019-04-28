using UnityEngine;

using AlexaRun.Structures;
namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Item Object Pool Settings", menuName = "Alexa Run/Item Object Pool Settings")]
    public class ItemObjectPoolSettings : ScriptableObject
    {
        public NumberedItemSet initialItemCounts;
    }
}