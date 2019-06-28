using UnityEngine;
using UnityEditor;

using AlexaRun.Behaviours;
namespace AlexaRun.EditorScripts
{
    [CustomEditor(typeof(SoundEffectBehaviour))]
    public class SoundEffectInspector : Editor
    {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            if (GUILayout.Button("Play Sound")) {
                ((SoundEffectBehaviour)target).PlaySound();
            }

            if (GUILayout.Button("Pause Sound")) {
                ((SoundEffectBehaviour)target).PauseSound();
            }

            if (GUILayout.Button("Stop Sound")) {
                ((SoundEffectBehaviour)target).StopSound();
            }
        }
    }
}