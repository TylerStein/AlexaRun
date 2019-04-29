using UnityEngine;
using System.Collections.Generic;

using AlexaRun.Interfaces;
namespace AlexaRun.Behaviours.Player
{
    [RequireComponent(typeof(Collider2D))]
    public class PlayerPointProximityController : MonoBehaviour
    {
        [SerializeField] private string interactionPointTag = "InteractionPoint";
        [SerializeField] public IPointBehaviour NearestPoint { get; private set; }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag(interactionPointTag)) {
                IPointBehaviour behaviour = collision.gameObject.GetComponent<IPointBehaviour>();
                if (behaviour != null) NearestPoint = behaviour;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag(interactionPointTag)) {
                if (NearestPoint != null && collision.gameObject == NearestPoint.GetGameObject()) NearestPoint = null;
            }
        }
    }
}
