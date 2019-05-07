using UnityEngine;

namespace AlexaRun.Behaviours
{
    public class SpriteFadeBehaviour : MonoBehaviour
    {

        [SerializeField] private SpriteRenderer spriteRenderer = null;
        [SerializeField] private float fadeRate = 1.0f;

        [SerializeField] private Color spriteColor = Color.white;
        [SerializeField] private float targetAlpha = 0.0f;

        [SerializeField] private bool pingPong = false;

        public void SetAlpha(float alpha) {
            alpha = Mathf.Clamp01(alpha);
            spriteColor = spriteRenderer.color;
            spriteColor.a = alpha;
            spriteRenderer.color = spriteColor;
        }

        public void SetTargetAlpha(float target) {
            targetAlpha = Mathf.Clamp01(target);
        }

        public void EnablePingPong() {
            pingPong = true;
            targetAlpha = (targetAlpha >= 0.5f) ? 0.0f : 1.0f;
        }

        public void DisablePingPong() {
            pingPong = false;
        }

        void Start() {
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
            SetAlpha(targetAlpha);
        }

        void Update() {
            spriteColor = spriteRenderer.color;
            float fadeValue = spriteColor.a;
            if (fadeValue <= targetAlpha) {
                // increase to target
                fadeValue += fadeRate * Time.deltaTime;
                if (fadeValue >= targetAlpha) {
                    if (pingPong) {
                        fadeValue = targetAlpha + (targetAlpha - fadeValue);
                        targetAlpha = 0;
                    } else {
                        fadeValue = targetAlpha;
                    }
                }
            } else if (fadeValue >= targetAlpha) {
                // decrease to target
                fadeValue -= fadeRate * Time.deltaTime;
                if (fadeValue <= targetAlpha) {
                    if (pingPong) {
                        fadeValue = -fadeValue;
                        targetAlpha = 1;
                    } else {
                        fadeValue = targetAlpha;
                    }
                }
            }
            spriteColor.a = fadeValue;
            spriteRenderer.color = spriteColor;
        }
    }
}