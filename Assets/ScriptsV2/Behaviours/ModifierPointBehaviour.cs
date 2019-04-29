using UnityEngine;
using AlexaRun.ScriptableObjects;
using AlexaRun.Behaviours.Player;
using AlexaRun.Interfaces;

namespace AlexaRun.Behaviours
{
    class ModifierPointBehaviour : MonoBehaviour, IPointBehaviour
    {
        [SerializeField] private ModifierPointDefinition definition;

        public bool OnInteract(PlayerBehaviour player) {
            return false;
        }

        public GameObject GetGameObject() {
            return gameObject;
        }
    }
}
