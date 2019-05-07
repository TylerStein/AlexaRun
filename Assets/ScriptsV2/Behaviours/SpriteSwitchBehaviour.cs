using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AlexaRun.Behaviours
{
    public class SpriteSwitchBehaviour : MonoBehaviour
    {
        [SerializeField] public List<Sprite> sprites = new List<Sprite>();
        [SerializeField] [ReadOnly] private int activeSpriteIndex = 0;
        [SerializeField] private SpriteRenderer spriteRenderer = null;


        public void SetActiveSpriteIndex(int index) {
            if (activeSpriteIndex >= sprites.Count) throw new UnityException("Index out of sprite range");
            activeSpriteIndex = index;
            updateSprite();
        }

        public void Next() {
            activeSpriteIndex++;
            if (activeSpriteIndex >= sprites.Count) activeSpriteIndex = 0;
            updateSprite();
        }

        public void Prev() {
            activeSpriteIndex--;
            if (activeSpriteIndex <= 0) activeSpriteIndex = sprites.Count - 1;
            updateSprite();
        }

        private void Start() {
            updateSprite();
        }

        private void updateSprite() {
            spriteRenderer.sprite = sprites[activeSpriteIndex];
        }
    }
}
