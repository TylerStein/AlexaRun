using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessSupplyInteractive : Interactive
{
    public SupplyRoom room;
    public GameObject entityPrefab;

    public AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip wrongSound;

    public void Awake() {
        interactiveHighlight.enabled = false;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!room) Debug.LogError("SupplyInteractive has no assigned room!");

        if (animationEventBroadcaster) {
            animationEventBroadcaster.animationEvent.AddListener(OnAnimationEvent);
            animationEventBroadcaster.audioEvent.AddListener(OnAudioEvent);
        }
    }

    public void OnAnimationEvent(string e) {

    }

    public void OnAudioEvent(string e) {

    }

    // Update is called once per frame
    void Update() {
        if (Vector3.Distance(player.transform.position, transform.position) < interactDistance) {
            if (!withinDistance) {
                // Player is within distance
                Debug.Log("Player is nearby " + entityType.entityName);
                withinDistance = true;
                player.nearbyInteractive = this;
                interactiveHighlight.enabled = true;
            }
        } else {
            if (withinDistance) {
                // Player left distance
                Debug.Log("Player left range for " + entityType.entityName);
                withinDistance = false;
                player.nearbyInteractive = null;
                interactiveHighlight.enabled = false;
            }
        }
    }

    public override void OnInteract(InteractKey key) {
        Debug.Log("Player attempted to take " + entityType.entityName);
        // If interact while holding the matching entity type
        if (player.CanHoldEntity()) {
            Entity entity = Instantiate(entityPrefab).GetComponent<Entity>();
            player.Push(entity);
            if (pickupSound) audioSource.PlayOneShot(pickupSound);
        } else {
            Instantiate(gameMaster.wrongPrefab, transform.position + gameMaster.GetWrongPrefabOffset(), Quaternion.identity);
            if (wrongSound) audioSource.PlayOneShot(wrongSound);
        }
    }
}
