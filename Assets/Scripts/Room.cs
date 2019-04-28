using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public enum RoomType
{
    DEMAND,
    SUPPLY,
    BUILDUP,
    DISPOSAL
}

public abstract class Room : MonoBehaviour
{
    public bool alive = true;
    public RoomType roomType;

    public SpriteRenderer roomHighlight;

    public abstract void OnFail(bool tellGameMaster = false);
}
