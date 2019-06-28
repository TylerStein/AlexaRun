using UnityEngine;
using TMPro;
using AlexaRun.Global;


namespace AlexaRun.Level {
    public class ScoreController : MonoBehaviour
    {
        [SerializeField] private int levelScore = 0;
        [SerializeField] private TextMeshProUGUI scoreDisplay = null;

        [SerializeField] private float difficultyScoreModifier = 1f;
        [SerializeField] private float roomScoreModifier = 1f;

        private void Start() {
            if (scoreDisplay) UpdateScoreDisplay();
            Settings.Persistent.SubscribeToValueChanges(() => {
                SetDifficultyScoreModifier(Settings.Persistent.DifficultyScale);
            });
        }

        public void SetDifficultyScoreModifier(float modifier = 1f) {
            difficultyScoreModifier = modifier;
            Debug.Log("DifficultyScoreModifier Changed to " + modifier);
        }

        public void SetRoomScoreModifier(float modifier = 1f) {
            roomScoreModifier = modifier;
            Debug.Log("RoomScoreModifier Changed to " + modifier);
        }

        public void IncrementScore(float baseAmount) {
            levelScore += Mathf.RoundToInt(difficultyScoreModifier * baseAmount) + Mathf.RoundToInt(roomScoreModifier * baseAmount);
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
