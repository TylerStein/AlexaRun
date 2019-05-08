using UnityEngine;
using AlexaRun.Behaviours.Player;

namespace AlexaRun.Interfaces
{
    /// <summary>
    /// An point that can be interacted with by the player
    /// </summary>
    public abstract class PointBehaviour : MonoBehaviour
    {
        public abstract bool OnInteract(PlayerBehaviour playerBehaviour);
        public abstract void SetEnabled(bool enabled);

        protected PointBehaviour() {
            //
        }
    }
}