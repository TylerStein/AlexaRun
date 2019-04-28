using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StairsInteractive : Interactive
{
    public GameObject arrow;
    public StairsInteractive other;
    public bool topFloor = false;

    public AudioSource audioSource;
    public AudioClip stairsClip;

    public void Awake() {
        arrow.SetActive(false);
    }

    public void Update() {
        if (Vector3.Distance(player.transform.position, transform.position) < interactDistance) {
            if (!withinDistance) {
                // Player is within distance
                Debug.Log("Player is nearby stairs");
                withinDistance = true;
                player.nearbyInteractive = this;
                arrow.SetActive(true);
            }
        } else {
            if (withinDistance) {
                // Player left distance
                Debug.Log("Player left range for stairs");
                withinDistance = false;
                if (player.nearbyInteractive == this) player.nearbyInteractive = null;
                arrow.SetActive(false);
            }
        }
    }

    public override void OnInteract(InteractKey key) {
        player.transform.position = other.transform.position;
        player.nearbyInteractive = other;
        audioSource.PlayOneShot(stairsClip);
    }
}
