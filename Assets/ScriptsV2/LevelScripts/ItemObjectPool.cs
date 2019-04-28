using System.Collections.Generic;
using UnityEngine;

using AlexaRun.Behaviours;
using AlexaRun.Structures;
using AlexaRun.ScriptableObjects;
using AlexaRun.Factories;
namespace AlexaRun.Level
{
    public class ItemObjectPool : MonoBehaviour
    {
        public ItemObjectPoolSettings settings;
        public Dictionary<string, Queue<GameObject>> objectPools;
        public Dictionary<string, ItemDefinition> registeredItemDefinitions;

        private ItemFactory itemFactory;

        public void SpawnFromPool(out GameObject outObject, string itemName) {
            assertKeyExists(itemName);
            if (objectPools[itemName].Count > 0) {
                outObject = objectPools[itemName].Dequeue();
                outObject.SetActive(true);
            } else {
                outObject = instantiateItem(registeredItemDefinitions[itemName]);
            }
        }

        public void SpawnFromPool(out GameObject outObject, string itemName, Vector3 position, Quaternion rotation) {
            assertKeyExists(itemName);
            if (objectPools[itemName].Count > 0) {
                outObject = objectPools[itemName].Dequeue();
            } else {
                outObject = instantiateItem(registeredItemDefinitions[itemName]);
            }

            outObject.transform.SetPositionAndRotation(position, rotation);
            outObject.SetActive(true);
        }

        public void SpawnFromPool(out GameObject outObject, string itemName, Transform parent, Vector3 position, Quaternion rotation) {
            assertKeyExists(itemName);
            if (objectPools[itemName].Count > 0) {
                outObject = objectPools[itemName].Dequeue();
            } else {
                outObject = instantiateItem(registeredItemDefinitions[itemName]);
            }

            outObject.transform.SetParent(parent);
            outObject.transform.SetPositionAndRotation(position, rotation);
            outObject.SetActive(true);
        }

        public void ReturnToPool(GameObject inObject) {
            ItemBehaviour item = inObject.GetComponent<ItemBehaviour>();
            if (item == null) throw new UnityException(string.Format("GameObject {0} is not recognised as an Item and cannot be returned to a pool!", inObject.name));

            ItemDefinition definition = item.ItemDefinition;
            if (definition == null) throw new UnityException(string.Format("Item component on GameObject {0} does not have a valid ItemDefinition!", inObject.name));

            assertKeyExists(definition.itemName);

            inObject.transform.SetParent(null);
            inObject.transform.SetPositionAndRotation(Vector3.zero, Quaternion.identity);
            inObject.SetActive(false);
            objectPools[definition.itemName].Enqueue(inObject);
        }

        private void Awake() {
            objectPools.Clear();
            foreach (NumberedItem numberedItemSet in settings.initialItemCounts.items) {
                registeredItemDefinitions[numberedItemSet.item.itemName] = numberedItemSet.item;
                objectPools[numberedItemSet.item.itemName] = intantiateItemsToQueue(numberedItemSet.item, numberedItemSet.count);
            }
        }


        private Queue<GameObject> intantiateItemsToQueue(ItemDefinition item, int count) {
            Queue<GameObject> objectQueue = new Queue<GameObject>();

            for (int i = 0; i < count; i++) {
                objectQueue.Enqueue(instantiateItem(item));
            }

            return objectQueue;
        }

        private GameObject instantiateItem(ItemDefinition definition) {
            return itemFactory.CreateItem(definition);
        }

        private void assertKeyExists(string itemName) {
            if (objectPools.ContainsKey(itemName) == false) throw new UnityException(string.Format("No entry named {0} exists in the object pool!", itemName));
        }
    }
}