using UnityEngine;

namespace AlexaRun.Global
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private SettingsDefinition settingsDefinition;

        public static SettingsDefinition instance;

        private void Awake() {
            if (settingsDefinition == null) throw new UnityException("Settings behaviour must have a SettingsDefinition set!");
            if (instance != null) Debug.LogWarning("A Settings instance has already been created and will be replaced. Is there more than one Settings behaviour in the scene?");
            instance = settingsDefinition;
        }
    }
}
