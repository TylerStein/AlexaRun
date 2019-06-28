using UnityEngine;
using AlexaRun.ScriptableObjects;
using AlexaRun.Behaviours.Player;
using AlexaRun.Interfaces;

namespace AlexaRun.Behaviours
{
    /// <summary>
    /// A Point Behaviour that can receive a set of items and output a specific item, with a cooldown
    /// </summary>
    class ModifierPointBehaviour : PointBehaviour
    {
        [SerializeField] private ModifierPointDefinition definition;
        [SerializeField] [ReadOnly] bool isEnabled = true;

        public override bool OnInteract(PlayerBehaviour player) {
            return false;
        }

        public override void SetEnabled(bool enabled) {
            isEnabled = enabled;
        }
    }
}
