using System.Collections.Generic;
using UnityEngine;

namespace AlexaRun.ScriptableObjects
{
    [CreateAssetMenu(fileName = "New Sound Effect", menuName = "Alexa Run/Sound Effect")]
    public class SoundEffectSettings : ScriptableObject
    {
        [SerializeField] private List<AudioClip> audioClips = new List<AudioClip>();

        [SerializeField] private bool loop;

        [SerializeField] private float minVolume = 0.98f;
        [SerializeField] private float maxVolume = 1.0f;

        [SerializeField] private float minPitchShift = 0.9f;
        [SerializeField] private float maxPitchShift = 1.1f;

        public AudioClip GetRandomAudioClip() {
            return audioClips[Random.Range(0, audioClips.Count)];
        }

        public float GetRandomVolume() {
            return Random.Range(minVolume, maxVolume);
        }

        public float GetRandomPitch() {
            return Random.Range(minPitchShift, maxPitchShift);
        }

        public bool isLooping() {
            return loop;
        }
    }
}