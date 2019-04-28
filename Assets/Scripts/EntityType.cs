using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "NewEntity", menuName = "SmartHomeDash/Entity", order = 1)]
public class EntityType : ScriptableObject
{
    public string entityName;
    public Sprite stackHoverSprite;
    public Sprite stackItemSprite;
    public Sprite heldItemSprite;
    public float interactRange = 1.0f;
}
