using UnityEngine;
using UnityEngine.Events;

namespace AlexaRun.Global
{
    [CreateAssetMenu(fileName = "New Persistent Settings", menuName = "Alexa Run/Persistent Settings")]
    public class PersistentSettingsDefinition : ScriptableObject
    {
        /** Setting storage keys */
        private static readonly string Key_VolumeLevel_General = "VOL_GENERAL";
        private static readonly string Key_VolumeLevel_Music = "VOL_MUSIC";
        private static readonly string Key_DifficultyScale = "DIFFICULTY";
        private static readonly string Key_Initialized = "INITIALIZED";

        private UnityEvent OnValueChange = new UnityEvent();

        public float VolumeLevel_General {
            get { return volumeLevel_General; }
            set {
                volumeLevel_General = Mathf.Clamp01(value);
                OnValueChange.Invoke();
            }
        }

        public float VolumeLevel_Music {
            get { return volumeLevel_Music; }
            set {
                volumeLevel_Music = Mathf.Clamp01(value);
                OnValueChange.Invoke();
            }
        }

        public float DifficultyScale {
            get { return difficultyScale; }
            set {
                difficultyScale = Mathf.Clamp(value, 0, 10);
                OnValueChange.Invoke();
            }
        }

        [SerializeField] private float volumeLevel_General = 1.0f;
        [SerializeField] private float volumeLevel_Music = 1.0f;
        [SerializeField] private float difficultyScale = 1.0f;

        public void SubscribeToValueChanges(UnityAction action) {
            OnValueChange.AddListener(action);
        }

        public void UnsubscribeFromValueChanges(UnityAction action) {
            OnValueChange.RemoveListener(action);
        } 

        public void StorePlayerPrefs() {
            PlayerPrefs.SetInt(Key_Initialized, 1);
            PlayerPrefs.SetFloat(Key_VolumeLevel_General, VolumeLevel_General);
            PlayerPrefs.SetFloat(Key_VolumeLevel_Music, VolumeLevel_Music);
            PlayerPrefs.SetFloat(Key_DifficultyScale, DifficultyScale);
        }

        public void LoadPlayerPrefs() {
            if (PlayerPrefs.HasKey("INITIALIZED") == false) StorePlayerPrefs();
            
            VolumeLevel_General = PlayerPrefs.GetFloat(Key_VolumeLevel_General);
            VolumeLevel_Music = PlayerPrefs.GetFloat(Key_VolumeLevel_Music);
            DifficultyScale = PlayerPrefs.GetFloat(Key_DifficultyScale);

            OnValueChange.Invoke();
        }
    }
}