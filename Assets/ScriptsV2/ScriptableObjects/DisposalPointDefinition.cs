using UnityEngine;
using AlexaRun.Structures;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Disposal Point Definition", menuName = "Alexa Run/Disposal Point Definition")]
    public class DisposalPointDefinition : ScriptableObject
    {
        public ItemSet allowedItems;
        public bool allowAll;
        public float cooldown = 0f;

        public bool IsItemAllowed(ItemDefinition item) {
            if (allowAll) return true;
            else return allowedItems.items.Contains(item);
        }
    }
}