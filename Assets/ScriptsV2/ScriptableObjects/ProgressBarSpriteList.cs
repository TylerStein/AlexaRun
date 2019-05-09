using UnityEngine;
namespace AlexaRun.ScriptableObjects.UI
{
    [CreateAssetMenu(fileName = "New Progress Bar Sprite List", menuName = "Alexa Run/ProgressBarSpriteList")]
    public class ProgressBarSpriteList : ScriptableObject
    {
        public float VerticalOffset = 0.5f;
        public Sprite CapBottomEmpty;
        public Sprite CapBottomFull;
        public Sprite CapTopEmpty;
        public Sprite CapTopFull;
        public Sprite BodyEmpty;
        public Sprite BodyFull;
    }
}
