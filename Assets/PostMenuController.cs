using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using AlexaRun.Behaviours;

namespace AlexaRun.Level
{
    public class PostMenuController : MonoBehaviour
    {
        [SerializeField] private string restartLevelName = "level";
        [SerializeField] TextMeshProUGUI yourScoreText = null;
        [SerializeField] TextMeshProUGUI highScoreText = null;
        [SerializeField] InputController inputController = null;
        [SerializeField] ScoreController scoreController = null;

        public void OnRestart() {
            // SceneManager.SetActiveScene(SceneManager.GetSceneByName(restartLevelName));
            SceneManager.LoadScene(restartLevelName, LoadSceneMode.Single);
        }

        // Start is called before the first frame update
        void Start() {
            int levelScore = scoreController.LoadScore(restartLevelName);
            int highScore = scoreController.LoadHighScore();

            yourScoreText.SetText(levelScore.ToString());
            highScoreText.SetText(highScore.ToString());
        }

        // Update is called once per frame
        void Update() {
            if (inputController.AltInteract) {
                OnRestart();
            }
        }
    }
}