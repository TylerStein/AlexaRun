using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EntityStack : MonoBehaviour
{
    public UnityEvent OnStackEmpty = new UnityEvent();

    public string SortingLayerName = "Entities";

    public GameObject entityPrefab;
    public EntityType entityType;
    
    public List<Entity> spriteStack = new List<Entity>();

    public float stackYSpace = 0.2f;
    public float maxXOffset = 0.05f;

    public void Awake() {

    }

    public void Push(Entity entity = null) {
        spriteStack.Add(entity == null ? InstantiateStackEntity() : PositionEntityForStack(entity));
    }

    public Entity Pop() {
        int count = spriteStack.Count;
        if (count == 0) return null;
        Entity entity = spriteStack[count - 1];
        spriteStack.RemoveAt(count - 1);
       // Debug.Log("EntityStack.Pop");
        Reorder();
        if (spriteStack.Count == 0) OnStackEmpty.Invoke();
        return entity;
    }

    public Entity Peek() {
        return spriteStack[spriteStack.Count - 1];
    }

    public Entity PositionEntityForStack(Entity entity) {
        entity.transform.parent = transform;
        entity.transform.localPosition = new Vector3(0, spriteStack.Count * stackYSpace, 0);
        entity.sprite.sortingOrder = spriteStack.Count;
        entity.sprite.sortingLayerName = SortingLayerName;
        return entity;
    }

    public Entity InstantiateStackEntity() {
        GameObject entityObject = Instantiate(entityPrefab, transform, false);
        entityObject.name = entityType.name + "_" + spriteStack.Count;

        SpriteRenderer spriteRenderer = entityObject.GetComponent<SpriteRenderer>();
        Entity entity = entityObject.GetComponent<Entity>();

        entityObject.transform.parent = transform;

        entityObject.transform.localPosition = new Vector3(Random.Range(-maxXOffset, maxXOffset), spriteStack.Count * stackYSpace, 0);

        entity.sprite.sortingLayerName = SortingLayerName;
        entity.sprite.sortingOrder = spriteStack.Count;

        entity.sprite = spriteRenderer;
        entity.SetEntityType(entityType);

        return entity;
    }

    public void Reorder() {
        int i = 0;
        foreach (Entity e in spriteStack) {
            e.transform.parent = transform;
            e.transform.localPosition.Set(e.transform.position.x, i * stackYSpace, e.transform.position.z);
            e.sprite.sortingOrder = i;
            i++;
        }
    }
}
