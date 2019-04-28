using UnityEngine;
using UnityEngine.SceneManagement;

public class TitleSceneChanger : MonoBehaviour
{
    public int titleSceneIndex = 0;

    public void Update() {
        if (Input.GetButtonDown("Jump")) {
            SceneManager.LoadScene(titleSceneIndex);
        }
    }
}
