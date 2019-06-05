using UnityEngine.SceneManagement;
using UnityEngine;

namespace AlexaRun.Level
{
    public class MainMenuBehaviour : MonoBehaviour
    {
        [SerializeField] private GameObject creditsPanel;
        [SerializeField] private GameObject mainPanel;
        [SerializeField] private string mainSceneName;

        public void OnClickStart() {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(mainSceneName));
        }

        public void GoToCredits() {
            mainPanel.SetActive(false);
            creditsPanel.SetActive(true);
        }

        public void GoToMain() {
            creditsPanel.SetActive(false);
            mainPanel.SetActive(true);
        }
    }
}