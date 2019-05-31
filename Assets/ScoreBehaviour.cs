using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace AlexaRun.Level {
    public class ScoreBehaviour : MonoBehaviour
    {
        [SerializeField] private int levelScore = 0;
        [SerializeField] private TextMeshProUGUI scoreDisplay = null;

        private void Start() {
            UpdateScoreDisplay();
        }

        public void IncrementScore(int amount) {
            levelScore += amount;
            UpdateScoreDisplay();
        }

        public int GetScore() {
            return levelScore;
        }

        public void UpdateScoreDisplay() {
            scoreDisplay.text = levelScore.ToString();
        }
    }
}
