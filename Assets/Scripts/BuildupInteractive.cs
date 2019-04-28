using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildupInteractive : Interactive
{
    public BuildupRoom room;

    public int startStackCount;
    public int maxStack = 10;

    public float failTimer = 10.0f;
    public float failTick = 0f;

    public float supplyDelay = 2.0f;
    public float supplyTick = 0.0f;

    public AudioSource audioSource;
    public AudioClip pickupSound;
    public AudioClip wrongSound;

    public void Awake() {
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        interactiveHighlight.enabled = false;
        if (!room) Debug.LogError("SupplyInteractive has no assigned room!");
        for (int i = 0; i < startStackCount; i++) {
            stack.Push();
        }
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
    void Update()
    {
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
        UpdateSupply();
    }


    public override void OnInteract(InteractKey key) {
        Debug.Log("Player attempted to take " + entityType.entityName);
        // If interact while holding the matching entity type
        if (player.CanHoldEntity()) {
            supplyTick = 0;
            Entity entity = stack.Pop();
            if (entity) {
                Debug.Log("Player took " + entity.entityType.entityName);
                player.Push(entity);
                if (pickupSound) audioSource.PlayOneShot(pickupSound);
                if (room.panic) {
                    room.SetPanic(false);
                    failTick = 0;
                }
            } else {
                Instantiate(gameMaster.wrongPrefab, transform.position + gameMaster.GetWrongPrefabOffset(), Quaternion.identity);
                if (wrongSound) audioSource.PlayOneShot(wrongSound);
            }
        } else {
            Instantiate(gameMaster.wrongPrefab, transform.position + gameMaster.GetWrongPrefabOffset(), Quaternion.identity);
            if (wrongSound) audioSource.PlayOneShot(wrongSound);
        }
    }

    public void AddSupply() {
        if (stack.spriteStack.Count < maxStack) {
            stack.Push();
        }


        if (stack.spriteStack.Count >= maxStack) {
            room.SetPanic(true);
            failTick = 0;
        }
    }

    public void UpdateSupply() {
        if (!room.alive) return;

        if (!room.panic) {
            if (supplyTick < supplyDelay) {
                supplyTick += Time.deltaTime;
            } else {
                supplyTick = 0;
                AddSupply();
            }
        } else {
            if (failTick < failTimer) {
                failTick += Time.deltaTime;
            } else {
                room.OnFail(true);
            }
        }
    }


}
