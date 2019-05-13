using UnityEngine;
using UnityEditor;

using AlexaRun.Global;
namespace AlexaRun.EditorScripts
{
    [CustomEditor(typeof(Settings))]
    public class PersistentSettingsEditor : Editor
    {
        public override void OnInspectorGUI() {
            DrawDefaultInspector();

            if (GUILayout.Button("Save Persistent Settings")) {
                if (Settings.Persistent) Settings.Persistent.StorePlayerPrefs();
            }

            if (GUILayout.Button("Load Persistent Settings")) {
                if (Settings.Persistent) Settings.Persistent.LoadPlayerPrefs();
            }
        }
    }
}