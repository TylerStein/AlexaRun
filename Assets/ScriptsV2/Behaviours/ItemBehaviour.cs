using UnityEngine;

using AlexaRun.ScriptableObjects;
namespace AlexaRun.Behaviours {
    public class ItemBehaviour : MonoBehaviour
    {
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

        private void Awake() {
            if (itemDefinition != null) OnUpdateItemDefinition();
        }

        private void OnUpdateItemDefinition() {
            //
        }
    }
}