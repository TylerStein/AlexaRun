using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using AlexaRun.Global;

namespace AlexaRun.UIControllers
{
    public class PauseMenuController : MonoBehaviour
    {
        [SerializeField] public Slider musicVolumeSlider = null;
        [SerializeField] public Slider generalVolumeSlider = null;
        [SerializeField] public Slider difficultyScaleSlider = null;
        [SerializeField] public string mainMenuName = "MainMenu_Redux";

        private void OnDisable() {
            Settings.Persistent.StorePlayerPrefs();
        }

        private void OnEnable() {
            Settings.Persistent.LoadPlayerPrefs();
        }

        private void Awake() {
            if (Settings.Persistent) {
                musicVolumeSlider.SetValueWithoutNotify(Settings.Persistent.VolumeLevel_Music);
                generalVolumeSlider.SetValueWithoutNotify(Settings.Persistent.VolumeLevel_General);
                difficultyScaleSlider.SetValueWithoutNotify(Settings.Persistent.DifficultyScale);
            }

            musicVolumeSlider.onValueChanged.AddListener(UpdateMusicVolume);
            generalVolumeSlider.onValueChanged.AddListener(UpdateGeneralVolume);
            difficultyScaleSlider.onValueChanged.AddListener(UpdateDifficultyScale);
        }

        public void UpdateMusicVolume(float value) {
            if (Settings.Persistent) Settings.Persistent.VolumeLevel_Music = value;
        }

        public void UpdateGeneralVolume(float value) {
            if (Settings.Persistent) Settings.Persistent.VolumeLevel_General = value;
        }

        public void UpdateDifficultyScale(float value) {
            if (Settings.Persistent) Settings.Persistent.DifficultyScale = value;
        }

        public void ResetDifficulty() {
            if (Settings.Persistent) {
                Settings.Persistent.DifficultyScale = 1f;
                difficultyScaleSlider.SetValueWithoutNotify(Settings.Persistent.DifficultyScale);
            }
        }

        public void OnPressQuit() {
            SceneManager.LoadScene(mainMenuName, LoadSceneMode.Single);
        }

        public void OnPressRestart() {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
        }
    }
}