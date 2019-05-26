using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Item Definition", menuName = "Alexa Run/Item Definition")]
    public class ItemDefinition : ScriptableObject
    {
        public string itemName = "New Item";
        public Sprite texture;

        public float itemStackHeight = 0.08f;
        public float maxHorizontalOffset = 0.02f;
        public float maxRotationalOffset = 4.0f;

        public Vector3 GenerateItemStackPosition(int index, bool horizontalShift = true) {
            Vector3 offset = Vector3.up * (index * itemStackHeight);
            if (horizontalShift) offset += Vector3.right * Random.Range(-maxHorizontalOffset, maxHorizontalOffset);
            return offset;
        }

        public Vector3 GenerateItemStackOffset(float baseHeight, bool horizontalShift = true) {
            Vector3 offset = Vector3.up * (baseHeight + itemStackHeight);
            if (horizontalShift) offset += Vector3.right * Random.Range(-maxHorizontalOffset, maxHorizontalOffset);
            return offset;
        }

        public Quaternion GenerateItemStackRotation(bool allowRotation = false) {
            if (allowRotation) {
                return Quaternion.Euler(0, 0, Random.Range(-maxRotationalOffset, maxRotationalOffset));
            } else {
                return Quaternion.identity;
            }
        }
    }
}