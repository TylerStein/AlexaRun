using UnityEngine;
using UnityEngine.UI;
using AlexaRun.Global;

namespace AlexaRun.UIControllers
{
    public class SettingsViewController : MonoBehaviour
    {
        [SerializeField] public Slider musicVolumeSlider = null;
        [SerializeField] public Slider generalVolumeSlider = null;
        [SerializeField] public Slider difficultyScaleSlider = null;

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
    }
}