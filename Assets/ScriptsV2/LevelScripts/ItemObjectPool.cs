using UnityEngine;
using System.Collections.Generic;
using AlexaRun.Behaviours;
using AlexaRun.Structures;
using AlexaRun.ScriptableObjects;
using AlexaRun.Factories;

namespace AlexaRun.Level
{
    public class ItemObjectPool : MonoBehaviour
    {
        public Transform poolRoot = null;

        public ItemObjectPoolSettings settings;
        public Dictionary<string, Queue<ItemBehaviour>> objectPools = new Dictionary<string, Queue<ItemBehaviour>>();
        public Dictionary<string, ItemDefinition> registeredItemDefinitions = new Dictionary<string, ItemDefinition>();

        [SerializeField] private ItemFactory itemFactory;

        public void SpawnFromPool(out ItemBehaviour outObject, string itemName) {
            assertKeyExists(itemName);
            if (objectPools[itemName].Count > 0) {
                outObject = objectPools[itemName].Dequeue();
                outObject.gameObject.SetActive(true);
            } else {
                outObject = instantiateItem(registeredItemDefinitions[itemName]);
            }
        }

        public void SpawnFromPool(out ItemBehaviour outObject, string itemName, Vector3 position, Quaternion rotation) {
            assertKeyExists(itemName);
            if (objectPools[itemName].Count > 0) {
                outObject = objectPools[itemName].Dequeue();
            } else {
                outObject = instantiateItem(registeredItemDefinitions[itemName]);
            }

            outObject.transform.localPosition = position;
            outObject.transform.localRotation = rotation;
            outObject.gameObject.SetActive(true);
        }

        public void SpawnFromPool(out ItemBehaviour outObject, string itemName, Transform parent, Vector3 position, Quaternion rotation) {
            assertKeyExists(itemName);
            if (objectPools[itemName].Count > 0) {
                outObject = objectPools[itemName].Dequeue();
            } else {
                outObject = instantiateItem(registeredItemDefinitions[itemName]);
            }

            outObject.transform.SetParent(parent);
            outObject.transform.localPosition = position;
            outObject.transform.localRotation = rotation;
            outObject.gameObject.SetActive(true);
        }

        public void ReturnToPool(ItemBehaviour inObject) {
            ItemBehaviour item = inObject.GetComponent<ItemBehaviour>();
            if (item == null) throw new UnityException(string.Format("GameObject {0} is not recognised as an Item and cannot be returned to a pool!", inObject.name));

            ItemDefinition definition = item.ItemDefinition;
            if (definition == null) throw new UnityException(string.Format("Item component on GameObject {0} does not have a valid ItemDefinition!", inObject.name));

            assertKeyExists(definition.itemName);

            inObject.SetSpriteLayerOrder(0);
            inObject.transform.SetParent(poolRoot);
            inObject.transform.localPosition = Vector3.zero;
            inObject.transform.localRotation = Quaternion.identity;
            inObject.gameObject.SetActive(false);
            objectPools[definition.itemName].Enqueue(inObject);
        }

        private void Awake() {
            if (itemFactory == null) {
                Debug.LogWarning(string.Format("ItemObjectPool {0} did not have an ItemFactory set in the editor and will attempt to find one in the scene", gameObject.name));
                ItemFactory[] factories = FindObjectsOfType<ItemFactory>();
                if (factories.Length == 0) throw new UnityException(string.Format("ItemObjectPool {0} was unable to find an ItemFactory in the scene!", gameObject.name));
                itemFactory = factories[0];
            }

            objectPools.Clear();
            foreach (NumberedItem numberedItemSet in settings.initialItemCounts.items) {
                registeredItemDefinitions[numberedItemSet.item.itemName] = numberedItemSet.item;
                objectPools[numberedItemSet.item.itemName] = intantiateItemsToQueue(numberedItemSet.item, numberedItemSet.count, false);
            }
        }


        private Queue<ItemBehaviour> intantiateItemsToQueue(ItemDefinition item, int count, bool startActive = true) {
            Queue<ItemBehaviour> objectQueue = new Queue<ItemBehaviour>();

            for (int i = 0; i < count; i++) {
                objectQueue.Enqueue(instantiateItem(item, startActive));
            }

            return objectQueue;
        }

        private ItemBehaviour instantiateItem(ItemDefinition definition, bool startActive = true) {
            ItemBehaviour item = itemFactory.CreateItem(definition);
            if (startActive == false) {
                item.transform.parent = poolRoot;
                item.gameObject.SetActive(false);
            }
            return item;
        }

        private void assertKeyExists(string itemName) {
            if (objectPools.ContainsKey(itemName) == false) throw new UnityException(string.Format("No entry named {0} exists in the object pool!", itemName));
        }
    }
}