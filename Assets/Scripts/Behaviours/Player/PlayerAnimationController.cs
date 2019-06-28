using UnityEngine;

namespace AlexaRun.Behaviours.Player
{
    /// <summary>
    /// Receives and passes values for the player Animator, to be used by a PlayerBehaviour script
    /// </summary>
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer playerSprite = null;
        [SerializeField] private Animator animator = null;

        [SerializeField] private string movementFieldName = "movement";
        [SerializeField] private string gameOverFieldName = "gameOver";
        [SerializeField] private string stackSizeFieldName = "stackSize";

        [SerializeField] private float lastMovementValue = 0f;
        [SerializeField] private bool lastGameOverValue = false;
        [SerializeField] private int lastStackSizeValue = 0;

        private bool lastFlipX = false;

        private void Awake() {
            if (animator == null) throw new UnityException(string.Format("PlayerAnimationController {0} does not have an animator set!", gameObject));
        }

        public void SetMovement(float movement = 0f) {
            lastMovementValue = movement;
        }

        public void SetGameOver(bool gameOver = true) {
            lastGameOverValue = gameOver;
        }

        public void SetStackSize(int stackSize) {
            lastStackSizeValue = stackSize;
        }

        public void UpdateAnimatorValues() {
            animator.SetBool(gameOverFieldName, lastGameOverValue);
            animator.SetFloat(movementFieldName, Mathf.Abs(lastMovementValue));
            animator.SetInteger(stackSizeFieldName, lastStackSizeValue);
            
            if (lastFlipX && lastMovementValue < 0) {
                lastFlipX = false;
                playerSprite.flipX = lastFlipX;
            } else if (!lastFlipX && lastMovementValue > 0) {
                lastFlipX = true;
                playerSprite.flipX = lastFlipX;
            }
        }
    }
}
