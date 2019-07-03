using UnityEngine;
using UnityEngine.UI;

public class QuitButton : MonoBehaviour
{
    [SerializeField] private bool isEnabled = true;
    [SerializeField] private Button button;

    // Start is called before the first frame update
    void Start()
    {
        if (Application.platform == RuntimePlatform.WebGLPlayer) {
            isEnabled = false;
            button.interactable = false;
        } else {
            isEnabled = true;
            button.interactable = true;
        }
    }

    public void QuitGame() {
        Debug.Log("QuitGame");
        if (isEnabled) {
            Application.Quit();
        }
    }
}
