using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Item Definition", menuName = "Alexa Run/Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        public string itemName;
        public Sprite texture;
    }
}