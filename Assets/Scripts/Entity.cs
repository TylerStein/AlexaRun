    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Entity : MonoBehaviour
{
    public SpriteRenderer sprite;
    public EntityType entityType;

    public void Awake() {
        sprite = GetComponent<SpriteRenderer>();
    }


    public void SetEntityType(EntityType t) {
        entityType = t;
        sprite.sprite = t.stackItemSprite;
    }
}
