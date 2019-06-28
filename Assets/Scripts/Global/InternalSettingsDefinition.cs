using UnityEngine;

namespace AlexaRun.Global
{
    [CreateAssetMenu(fileName = "New Internal Settings", menuName = "Alexa Run/Internal Settings")]
    public class InternalSettingsDefinition : ScriptableObject
    {
        [Header("Sprite Layers")]
        [SerializeField] public string backgroundSortingLayer = "Background";
        [SerializeField] public string propSortingLayer = "Props";
        [SerializeField] public string animationSortingLayer = "People";
        [SerializeField] public string itemSortingLayer = "Entities";
        [SerializeField] public string playerSortingLayer = "Player";
        [SerializeField] public string heldSortingLayer = "Held";
        [SerializeField] public string foregroundSortingLayer = "Foreground";
        [SerializeField] public string uiSoringLayer = "UI";

    }
}
