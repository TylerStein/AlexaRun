using UnityEngine;
using AlexaRun.Structures;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Disposal Point Definition", menuName = "Alexa Run/Disposal Point Definition")]
    public class DisposalPointDefinition : ScriptableObject
    {
        public ItemSet allowedItems;
        public bool allowAll;
    }
}