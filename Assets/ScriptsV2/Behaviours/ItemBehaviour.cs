using UnityEngine;

using AlexaRun.ScriptableObjects;
namespace AlexaRun.Behaviours {
    /// <summary>
    /// Instance behaviour for Items, defined by an ItemDefinition
    /// </summary>
    public class ItemBehaviour : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private ItemDefinition itemDefinition;
        public ItemDefinition ItemDefinition {
            get { return itemDefinition; }
            set {
                itemDefinition = value;
                OnUpdateItemDefinition();
            }
        }

        public void OnReturnToPool() {
            //
        }

        public void OnSpawnFromPool() {
            //
        }

        public void SetSpriteLayer(string layer) {
            spriteRenderer.sortingLayerName = layer;
        }

        public void SetSpriteLayerOrder(int order) {
            spriteRenderer.sortingOrder = order;
        }

        public void Awake() {
            if (itemDefinition != null) OnUpdateItemDefinition();
            if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnUpdateItemDefinition() {
            //
        }
    }
}