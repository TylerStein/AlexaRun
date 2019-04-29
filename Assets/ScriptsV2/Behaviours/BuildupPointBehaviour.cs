using UnityEngine;
using AlexaRun.ScriptableObjects;
using AlexaRun.Interfaces;
using AlexaRun.Behaviours.Player;

namespace AlexaRun.Behaviours
{
    class BuildupPointBehaviour : MonoBehaviour, IPointBehaviour
    {
        [SerializeField] private BuildupPointDefinition definition;

        public bool OnInteract(PlayerBehaviour player) {
            return false;
        }

        public GameObject GetGameObject() {
            return gameObject;
        }
    }
}
