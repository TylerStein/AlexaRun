using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AlexaRun.Behaviours.Player;

namespace AlexaRun.Behaviours
{
    public class GameOverBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject gameOverOverlay;
        [SerializeField] private CameraFollowBehaviour cameraFollowBehaviour;
        [SerializeField] private float targetCameraSize = 4f;

        private void Start() {
            gameOverOverlay.SetActive(false);
        }

        public void SetGameOver(PlayerBehaviour playerBehaviour) {
            gameOverOverlay.SetActive(true);
            cameraFollowBehaviour.SetTarget(playerBehaviour.transform);
            cameraFollowBehaviour.SetOrthographicSize(targetCameraSize);
        }
    }
}