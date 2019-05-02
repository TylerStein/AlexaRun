using UnityEngine;

namespace AlexaRun.Global
{
    [CreateAssetMenu(fileName = "New Global Settings", menuName = "Alexa Run/Global Settings")]
    public class SettingsDefinition : ScriptableObject
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
