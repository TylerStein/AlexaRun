using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposalInteractive : Interactive
{
    public Animator animator;
    public AudioSource audioSource;
    public AudioClip disposeSound;
    public AudioClip wrongSound;

    public void Awake() {
        interactiveHighlight.enabled = false;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (animationEventBroadcaster) {
            animationEventBroadcaster.animationEvent.AddListener(OnAnimationEvent);
            animationEventBroadcaster.audioEvent.AddListener(OnAudioEvent);
        }
    }

    public void OnAnimationEvent(string e) {

    }

    public void OnAudioEvent(string e) {

    }

    private void Update() {
        if (Vector3.Distance(player.transform.position, transform.position) < interactDistance) {
            if (!withinDistance) {
                // Player is within distance
                withinDistance = true;
                player.nearbyInteractive = this;
                interactiveHighlight.enabled = true;
            }
        } else {
            if (withinDistance) {
                // Player left distance
                withinDistance = false;
                player.nearbyInteractive = null;
                interactiveHighlight.enabled = false;
            }
        }
    }

    public override void OnInteract(InteractKey key) {
        Entity held = player.Peek();
        if (held != null && held.entityType.name == entityType.name) {
            Entity e = player.Pop();
            Destroy(e.gameObject);
            animator.SetTrigger("interact");
            gameMaster.IncrementScore();
            if (disposeSound) audioSource.PlayOneShot(disposeSound);
        } else {
            Instantiate(gameMaster.wrongPrefab, transform.position + gameMaster.GetWrongPrefabOffset(), Quaternion.identity);
            if (wrongSound) audioSource.PlayOneShot(wrongSound);
        }

    }
}
