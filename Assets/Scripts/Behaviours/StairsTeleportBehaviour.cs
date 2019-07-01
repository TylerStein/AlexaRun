using UnityEngine;
using UnityEngine.Events;
using AlexaRun.Interfaces;
using AlexaRun.Behaviours.Player;
using AlexaRun.Level;

namespace AlexaRun.Behaviours
{
    [RequireComponent(typeof(Collider2D))]
    public class StairsTeleportBehaviour : PointBehaviour
    {
        [SerializeField] private StairsTeleportBehaviour destination = null;
        [SerializeField] private SoundEffectBehaviour teleportSoundEffect = null;
        [SerializeField] private SpriteIndicatorBehaviour teleportSpriteIndicator = null;
        [SerializeField] private LevelBehaviour levelBehaviour = null;
        [SerializeField] private string playerTag = "Player";

        public override bool OnInteract(PlayerBehaviour playerBehaviour) {
            playerBehaviour.gameObject.transform.SetPositionAndRotation(destination.transform.position, playerBehaviour.transform.rotation);
            teleportSoundEffect.PlaySound();
            playerBehaviour.IgnoreNextInteractionResult();
            return true;
        }

        public override void SetEnabled(bool enabled) {
            //
        }

        public void OnTriggerEnter2D(Collider2D collision) {
            if (collision.tag == playerTag) teleportSpriteIndicator.ShowSprite();
        }

        public void OnTriggerExit2D(Collider2D other) {
            if (other.tag == playerTag) teleportSpriteIndicator.HideSprite();
        }
    }
}