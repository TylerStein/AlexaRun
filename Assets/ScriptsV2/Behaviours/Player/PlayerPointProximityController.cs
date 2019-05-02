using UnityEngine;
using System.Collections.Generic;

using AlexaRun.Interfaces;
namespace AlexaRun.Behaviours.Player
{
    /// <summary>
    /// Uses a Collider2D to track inersection with interaction points, to be used by a PlayerBehaviour script
    /// </summary>
    [RequireComponent(typeof(Collider2D))]
    public class PlayerPointProximityController : MonoBehaviour
    {
        [SerializeField] private string interactionPointTag = "InteractionPoint";
        [SerializeField] public PointBehaviour NearestPoint { get; private set; }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.CompareTag(interactionPointTag)) {
                PointBehaviour behaviour = collision.gameObject.GetComponent<PointBehaviour>();
                if (behaviour != null) NearestPoint = behaviour;
            }
        }

        private void OnTriggerExit2D(Collider2D collision) {
            if (collision.CompareTag(interactionPointTag)) {
                if (NearestPoint != null && collision.gameObject == NearestPoint.gameObject) NearestPoint = null;
            }
        }
    }
}
