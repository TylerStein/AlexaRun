using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EndController : MonoBehaviour
{
    public bool newHighScore = false;

    public Text highScoreText;
    public Text lastScoreText;

    public GameObject newHighScoreObject;

    public int highScore = 0;
    public int lastScore = 0;

    // Start is called before the first frame update
    void Start()
    {
       // newHighScoreObject.SetActive(false);
        lastScore = PlayerPrefs.GetInt("lastScore", 0);
        highScore = PlayerPrefs.GetInt("highScore", 0);

        if (lastScore > highScore) {
            newHighScore = true;
            highScore = lastScore;
           // newHighScoreObject.SetActive(true);
            PlayerPrefs.SetInt("highScore", highScore);
        }
    }
}
