using UnityEngine;
using TMPro;


namespace AlexaRun.Level {
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private int levelScore = 0;
        [SerializeField] private TextMeshProUGUI scoreDisplay = null;

        private void Start() {
            if (scoreDisplay) UpdateScoreDisplay();
        }

        public void IncrementScore(int amount) {
            levelScore += amount;
            if (scoreDisplay) UpdateScoreDisplay();
        }

        public int GetScore() {
            return levelScore;
        }

        public void UpdateScoreDisplay() {
            scoreDisplay.text = levelScore.ToString();
        }

        public void StoreScore(string levelTitle) {
            PlayerPrefs.SetInt(levelTitle + "_score", levelScore);
            UpdateHighScore(levelScore);
        }

        public int LoadScore(string levelTitle) {
            return PlayerPrefs.GetInt(levelTitle + "_score", 0);
        }

        public int LoadHighScore() {
            return PlayerPrefs.GetInt("highscore", 0);
        }

        public void UpdateHighScore(int amount) {
            int lastHighScore = LoadHighScore();
            PlayerPrefs.SetInt("highscore", amount > lastHighScore ? amount : lastHighScore);
        }
    }
}
