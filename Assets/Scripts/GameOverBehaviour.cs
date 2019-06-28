using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using AlexaRun.Behaviours.Player;

namespace AlexaRun.Behaviours
{
    public class GameOverBehaviour : MonoBehaviour
    {
        [SerializeField] private string postGameLevelName = "PostMenu_Redux";
        [SerializeField] private GameObject gameOverOverlay = null;
        [SerializeField] private CameraFollowBehaviour cameraFollowBehaviour = null;
        [SerializeField] private InputController inputController = null;
        [SerializeField] private float targetCameraSize = 4f;
        [SerializeField] private SoundEffectBehaviour gameOverAudioBehaviour = null;
        [SerializeField] private bool gameOver = false;

        private void Start() {
            gameOverOverlay.SetActive(false);
        }

        private void Update() {
            if (gameOver && inputController.AltInteract) {
                // SceneManager.SetActiveScene(SceneManager.GetSceneByName(postGameLevelName));
                SceneManager.LoadScene(postGameLevelName, LoadSceneMode.Single);
            }
        }

        public void SetGameOver(PlayerBehaviour playerBehaviour) {
            gameOver = true;
            gameOverOverlay.SetActive(true);
            cameraFollowBehaviour.SetTarget(playerBehaviour.transform);
            cameraFollowBehaviour.SetOrthographicSize(targetCameraSize);
            gameOverAudioBehaviour.PlaySound();
        }
    }
}