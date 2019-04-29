using UnityEngine;
using AlexaRun.ScriptableObjects;
using AlexaRun.Behaviours;

namespace AlexaRun.Factories
{
    public class ItemFactory : MonoBehaviour
    {
        public ItemBehaviour CreateItem(ItemDefinition itemDefinition) {
            GameObject itemObject = new GameObject();
            ItemBehaviour itemBehaviour = itemObject.AddComponent<ItemBehaviour>();
            SpriteRenderer spriteRenderer = itemObject.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = itemDefinition.texture;
            itemBehaviour.ItemDefinition = itemDefinition;
            return itemBehaviour;
        }
    }
}