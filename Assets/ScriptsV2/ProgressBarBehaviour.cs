using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlexaRun.ScriptableObjects.UI;
using AlexaRun.Global;

namespace AlexaRun.Behaviours.UI
{
    public class ProgressBarBehaviour : MonoBehaviour
    {
        [SerializeField] private ProgressBarSpriteList sprites = null;
        [SerializeField] private Transform stackRoot = null;
        [SerializeField] private int maxTicks = 10;
        [SerializeField] private int currentTicks = 0;
        [SerializeField] [ReadOnly] private List<SpriteRenderer> ticks = new List<SpriteRenderer>();

        public void SetTicks(int ticks) {
            currentTicks = Mathf.Clamp(ticks, 0, maxTicks);
            updateTicks();
        }

        public void IncrementTicks() {
            SetTicks(currentTicks + 1);
        }

        public void DecrementTicks() {
            SetTicks(currentTicks - 1);
        }

        public int GetTicks() {
            return currentTicks;
        }

        void Awake() {
            if (stackRoot == null) stackRoot = transform;
            if (currentTicks > maxTicks) currentTicks = maxTicks;
            if (maxTicks < 3) throw new UnityException("Cannot populate progress bar tick instances with less than 3 ticks!");
            populateTickInstances();    
        }

        void populateTickInstances() {
            clearTickInstances();

            for (int i = 0; i < maxTicks; i++) {
                bool filled = i < currentTicks;
                SpriteRenderer tick = null;
                if (i == 0) {
                    // Bottom_Cap
                    tick = instantiateTick(i);
                } else if (i == (maxTicks - 1)) {
                    // Top_Cap
                    tick = instantiateTick(i);
                } else {
                    // Middle
                    tick = instantiateTick(i);
                }
                ticks.Add(tick);
            }
            updateTicks();
        }

        void clearTickInstances() {
            for (int i = 0; i < ticks.Count; i++) {
                Destroy(ticks[i].gameObject);
            }
            ticks.Clear();
        }

        void updateTicks() {
            for (int i = 0; i < maxTicks; i++) {
                bool filled = i < currentTicks;
                if (i == 0) {
                    // Bottom_Cap
                    ticks[i].sprite = filled ? sprites.CapBottomFull : sprites.CapBottomEmpty;
                } else if (i == (maxTicks - 1)) {
                    // Top_Cap
                    ticks[i].sprite = filled ? sprites.CapTopFull : sprites.CapTopEmpty;
                } else {
                    // Middle
                    ticks[i].sprite = filled ? sprites.BodyFull : sprites.BodyEmpty;
                }
            }
        }

        SpriteRenderer instantiateTick(int index) {
            GameObject tickObject = new GameObject("Tick " + index);
            SpriteRenderer spriteRenderer = tickObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = Settings.Constant.uiSoringLayer;
            float halfHeight = sprites.VerticalOffset * maxTicks / 2f;
            tickObject.transform.position = new Vector3(0, (sprites.VerticalOffset * index) - halfHeight);
            tickObject.transform.SetParent(stackRoot, false);
            return spriteRenderer;
        }

        private void OnDrawGizmos() {
            float halfWidth = 0.5f;
            float halfHeight = sprites.VerticalOffset * maxTicks / 2;

            Vector3 root = stackRoot.position;

            Vector3 topLeft = root + new Vector3(-halfWidth, halfHeight);
            Vector3 topRight = root + new Vector3(halfWidth, halfHeight);
            Vector3 botLeft = root + new Vector3(-halfWidth, -halfHeight);
            Vector3 botRight = root + new Vector3(halfWidth, -halfHeight);

            Gizmos.DrawLine(topLeft, topRight);
            Gizmos.DrawLine(topRight, botRight);
            Gizmos.DrawLine(botRight, botLeft);
            Gizmos.DrawLine(botLeft, topLeft);
        }
    }
}