using UnityEngine;

namespace AlexaRun.Global
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private InternalSettingsDefinition internalSettingsDefinition = null;
        [SerializeField] private PersistentSettingsDefinition persistentSettingsDefinition = null;

        public static InternalSettingsDefinition Constant;
        public static PersistentSettingsDefinition Persistent;

        private void OnEnable() {
            DontDestroyOnLoad(gameObject);

            if (internalSettingsDefinition == null) throw new UnityException("Settings behaviour must have an InternalSettingsDefinition set!");
            if (persistentSettingsDefinition == null) throw new UnityException("Settings behaviour must have a PersistentSettingsDefinition set!");

            Settings[] existingSettings = FindObjectsOfType<Settings>();
            for (int i = 0; i < existingSettings.Length; i++) {
                if (existingSettings[i] != this) Destroy(existingSettings[i].gameObject);
            }

            if (Constant != null || Persistent != null) Debug.LogWarning("A Settings instance has already been created and will be replaced. Is there more than one Settings behaviour in the scene?");

            Constant = internalSettingsDefinition;
            Persistent = persistentSettingsDefinition;

            Persistent.LoadPlayerPrefs();
        }
    }
}
