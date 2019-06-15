using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace AlexaRun.Level
{
    public class BoolUnityEvent : UnityEvent<bool> { };

    public class PauseBehaviour : MonoBehaviour
    {
        [SerializeField] public GameObject pauseMenuRoot = null;

        [SerializeField] private float timeScale = 1.0f;
        public float TimeScale {
            get { return timeScale; }
            private set { timeScale = value; }
        }

    
        [SerializeField] private bool isPaused = false;
        public bool IsPaused {
            get { return isPaused; }
            private set { isPaused = value; }
        }

        [SerializeField] private UnityEvent<bool> onUpdatePlayState = new BoolUnityEvent();
        public void SubscribeToPauseState(UnityAction<bool> action) { onUpdatePlayState.AddListener(action); }
        public void UnsubscribeFromPauseState(UnityAction<bool> action) { onUpdatePlayState.RemoveListener(action); }

        // Start is called before the first frame update
        void Awake() {
            timeScale = 1.0f;
            isPaused = false;
            pauseMenuRoot.SetActive(false);
        }

        public void TogglePause(bool affectMenu = true) {
            if (isPaused) {
                UnPause(affectMenu);
            } else {
                Pause(affectMenu);
            }
            onUpdatePlayState.Invoke(!isPaused);
        }

        public void Pause(bool hideMenu = true) {
            if (hideMenu) pauseMenuRoot.SetActive(true);
            isPaused = true;
            timeScale = 0f;
        }

        public void UnPause(bool hideMenu = true) {
            if (hideMenu) pauseMenuRoot.SetActive(false);
            isPaused = false;
            timeScale = 1f;

        }
    }
}