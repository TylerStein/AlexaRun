using UnityEngine;
using AlexaRun.Behaviours.Player;

namespace AlexaRun.Interfaces
{
    public interface IPointBehaviour
    {
        bool OnInteract(PlayerBehaviour playerHeldStack);
        GameObject GetGameObject();
    }
}