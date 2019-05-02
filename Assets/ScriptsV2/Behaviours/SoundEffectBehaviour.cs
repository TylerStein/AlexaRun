using UnityEngine;
using AlexaRun.ScriptableObjects;

namespace AlexaRun.Behaviours
{
    /// <summary>
    /// A generalized wrapper for playing sounds with variation to their volume and pitch based on given settings.
    /// This handles limits set by the audio portion the user's GameSettings
    /// </summary>
    public class SoundEffectBehaviour : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource = null;
        [SerializeField] private SoundEffectSettings settings = null;

        private bool isPaused = false;

        public void Awake() {
            if (settings == null) throw new UnityException(string.Format("SoundEffectBehaviour {0} is missing sound effect settings!", gameObject.name));
            if (audioSource == null) throw new UnityException(string.Format("SoundEffectBehaviour {0} is missing an audio source!", gameObject.name));
        }

        public void PlaySound() {
            if (isPaused) {
                audioSource.Play();
                isPaused = false;
            } else {
                audioSource.loop = settings.isLooping();
                audioSource.pitch = settings.GetRandomPitch();
                audioSource.clip = settings.GetRandomAudioClip();
                audioSource.volume = settings.GetRandomVolume();
                audioSource.Play();
            }       
        }

        public void PauseSound() {
            isPaused = true;
            audioSource.Pause();
        }

        public void StopSound() {
            audioSource.Stop();
        }
    }
}
