    using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum InteractKey
{
    SPACE,
    UP,
    DOWN
}

public abstract class Interactive : MonoBehaviour
{
    public float interactDistance = 1.0f;

    public EntityType entityType;

    public Player player;
    public EntityStack stack;

    public GameMaster gameMaster;

    public SpriteRenderer interactiveHighlight;

    public AnimationEventBroadcaster animationEventBroadcaster;

    [SerializeField]
    public AudioClipDictionary audioClipDictionary = new AudioClipDictionary();

    public bool withinDistance = false;

    public abstract void OnInteract(InteractKey key);
}
