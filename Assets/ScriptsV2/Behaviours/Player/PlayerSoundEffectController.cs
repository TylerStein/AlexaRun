using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexaRun.Behaviours.Player
{
    /// <summary>
    /// Recieves and handles values for the player sound effects, to be used by a PlayerBehaviour script
    /// </summary>
    public class PlayerSoundEffectController : MonoBehaviour
    {
        [SerializeField] private SoundEffectBehaviour soundEffectBehaviour = null;

        [SerializeField] private float moveInputThreshold = 0.05f;
        [SerializeField] private bool isMoving = false;

        public void SetMovement(float movement = 0) {
            float absMovement = Mathf.Abs(movement);
            if (isMoving && absMovement < moveInputThreshold) {
                isMoving = false;
                soundEffectBehaviour.PauseSound();
            } else if (isMoving == false && absMovement >= moveInputThreshold) {
                soundEffectBehaviour.PlaySound();
                isMoving = true;
            }
        }
        
    }
}