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
        [SerializeField] private GameObject gameOverOverlay;
        [SerializeField] private CameraFollowBehaviour cameraFollowBehaviour;
        [SerializeField] private InputController inputController;
        [SerializeField] private float targetCameraSize = 4f;

        private void Start() {
            gameOverOverlay.SetActive(false);
        }

        private void Update() {
            if (inputController.AltInteract) {
                // SceneManager.SetActiveScene(SceneManager.GetSceneByName(postGameLevelName));
                SceneManager.LoadScene(postGameLevelName, LoadSceneMode.Single);
            }
        }

        public void SetGameOver(PlayerBehaviour playerBehaviour) {
            gameOverOverlay.SetActive(true);
            cameraFollowBehaviour.SetTarget(playerBehaviour.transform);
            cameraFollowBehaviour.SetOrthographicSize(targetCameraSize);
        }
    }
}