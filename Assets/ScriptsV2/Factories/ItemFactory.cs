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

        public ItemBehaviour CreateItem(ItemDefinition itemDefinition) {
            GameObject itemObject = new GameObject();
            ItemBehaviour itemBehaviour = itemObject.AddComponent<ItemBehaviour>();
            SpriteRenderer spriteRenderer = itemObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sortingLayerName = itemSortingLayer;
            spriteRenderer.sprite = itemDefinition.texture;
            itemBehaviour.ItemDefinition = itemDefinition;
            return itemBehaviour;
        }
    }
}