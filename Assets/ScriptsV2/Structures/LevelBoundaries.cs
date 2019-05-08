using UnityEngine;

namespace AlexaRun.Structures
{
    [System.Serializable]
    public struct LevelBoundaries
    {
        [SerializeField] public float minX;
        [SerializeField] public float maxX;

        LevelBoundaries(float min, float max) {
            minX = min;
            maxX = max;
        }
    }
}
