using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Player Settings", menuName = "Alexa Run/Player Settings")]
    public class PlayerSettings : ScriptableObject
    {
        public int holdStackLimit = 3;
        public float baseMetersPerSecond = 0.7f;
    }
}
