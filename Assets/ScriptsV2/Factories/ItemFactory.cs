using UnityEngine;
using AlexaRun.ScriptableObjects;
using AlexaRun.Behaviours;

namespace AlexaRun.Factories
{
    /// <summary>
    /// Factory for creating ItemBehaviour GameObjects based on a given ItemDefinition
    /// </summary>
    public class ItemFactory : MonoBehaviour
    {
        [SerializeField] public string itemSortingLayer = "Entities";

        private System.Type[] itemComponentTypes = { typeof(ItemBehaviour), typeof(SpriteRenderer) };

        public ItemBehaviour CreateItem(ItemDefinition itemDefinition) {
            GameObject itemObject = new GameObject("New " + itemDefinition.itemName, itemComponentTypes);
            ItemBehaviour itemBehaviour = itemObject.GetComponent<ItemBehaviour>();
            SpriteRenderer spriteRenderer = itemObject.GetComponent<SpriteRenderer>();
            itemBehaviour.Awake();
            spriteRenderer.sortingLayerName = itemSortingLayer;
            spriteRenderer.sprite = itemDefinition.texture;
            itemBehaviour.ItemDefinition = itemDefinition;
            return itemBehaviour;
        }
    }
}