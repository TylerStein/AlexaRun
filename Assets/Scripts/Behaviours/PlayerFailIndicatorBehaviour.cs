using UnityEngine;

namespace AlexaRun.Behaviours.Player
{
    public class PlayerFailIndicatorBehaviour : MonoBehaviour
    {
        [SerializeField] public float maxOffset = 0.25f;
        [SerializeField] public float fadeoutTime = 0.6f;
        [SerializeField] public SpriteRenderer spriteRenderer = null;

        private float fadeTimer = 0f;
        private Color color;

        public void Show() {
            spriteRenderer.transform.localPosition = new Vector3(Random.Range(-maxOffset, maxOffset), Random.Range(-maxOffset, maxOffset), 0);
            fadeTimer = fadeoutTime;
            color = spriteRenderer.color;
        }

        private void Awake() {
            color = spriteRenderer.color;
            color.a = 0;
            spriteRenderer.color = color;
        }

        private void Update() {
            if (fadeTimer > 0f) { 
                fadeTimer -= Time.deltaTime;
                if (fadeTimer < 0) fadeTimer = 0;
                color.a = (fadeTimer / fadeoutTime);
                spriteRenderer.color = color;
            }
        }
    }
}
