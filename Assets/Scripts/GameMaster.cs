using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameMaster : MonoBehaviour
{
    public int postGameSceneIndex;
    public bool paused = true;
    public GameObject preGameOverlay;

    new public Camera camera;
    public List<Room> rooms = new List<Room>();

    public int currentScore = 0;

    public Vector3 failDest = Vector3.zero;
    public bool failAnim;
    public float failAnimSpeed = 1.0f;
    public float failZoomLevel = 2.2f;

    public GameObject gameOverObject;
    public GameObject wrongPrefab;

    public Sprite highlight_panic;
    public Sprite highlight_fail;

    public Player player;
    public AudioSource musicPlayer;
    public AudioClip musicClip;
    public AudioClip failClip;
    public Text scoreText;

    public AudioClip panicSound;

    public List<Room> panicRooms;

    public Vector3 GetWrongPrefabOffset() {
        return (new Vector3(0, 1.25f, 0) + (Random.insideUnitSphere * 0.25f));
    }

    public void Awake() {
        rooms.AddRange(FindObjectsOfType<Room>());
        camera = Camera.main;
        failAnim = false;
        gameOverObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
    }

    public void Start() {
        paused = true;
        preGameOverlay.SetActive(true);
        Time.timeScale = 0;

        currentScore = 0;
        scoreText.text = currentScore.ToString();

        musicPlayer.clip = musicClip;
        musicPlayer.loop = true;
        musicPlayer.Play();
    }


    public void IncrementScore() {
        currentScore++;
        scoreText.text = currentScore.ToString();
    }

    public void OnRoomFail(Room room) {
        PlayerPrefs.SetInt("lastScore", currentScore);

        musicPlayer.Stop();
        musicPlayer.clip = failClip;
        musicPlayer.Play();

        player.SetFailed();
        foreach (Room r in rooms) {
            if (r != room) r.OnFail(false);
        }
        failDest = room.gameObject.transform.position;
        failDest.z = camera.transform.position.z;
        gameOverObject.SetActive(true);
        failAnim = true;
    }

    public void Update() {
        if (paused && Input.GetButtonDown("Jump")) {
            Time.timeScale = 1.0f;
            paused = false;
            preGameOverlay.SetActive(false);
            return;
        }

        if (failAnim) {
            Vector3 diff = camera.transform.position - failDest;
            Vector3 newPos = Vector3.Lerp(camera.transform.position, failDest, Time.deltaTime * failAnimSpeed);

            float newSize = Mathf.Lerp(camera.orthographicSize, failZoomLevel, Time.deltaTime * failAnimSpeed);
            camera.orthographicSize = newSize;

            camera.transform.position = newPos;

            if (Input.GetButtonDown("Jump")) {
                SceneManager.LoadScene(postGameSceneIndex);
            }
        }
    }
}
