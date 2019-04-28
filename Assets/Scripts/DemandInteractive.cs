using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DemandInteractive : Interactive
{
    public DemandRoom room;
   
    public int startStackCount = 5;
    public int maxStack = 10;

    public float failTimer = 10.0f;
    public float failTick = 0f;

    public float demandDelay = 2.0f;
    public float demandTick = 0.0f;

    public AudioSource audioSource;
    public AudioClip placeSound;
    public AudioClip wrongSound;

    public void Awake() {
        interactiveHighlight.enabled = false;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        if (!room) Debug.LogError("DemandInteractive has no assigned room!");
        if (animationEventBroadcaster) {
            animationEventBroadcaster.animationEvent.AddListener(OnAnimationEvent);
            animationEventBroadcaster.audioEvent.AddListener(OnAudioEvent);
        }
    }

    public void OnAnimationEvent(string e) {
        //Debug.Log("Got animation event " + e);
        //if (e == "take") {
        //    if (!room.panic && room.alive) demandTick = demandDelay + 1;
        //}
    }

    public void OnAudioEvent(string e) {
        AudioClip clip;
        if (audioClipDictionary.TryGetValue(e, out clip)) {
            // make a sound
        }
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
        UpdateDemand();
        UpdateState();
    }

    public void OnStackCountChange() {
        if (!room.alive) return;
        int count = stack.spriteStack.Count;
        if (count == 0 && !room.panic) {
            room.SetPanic(true);
            failTick = 0f;
        } else if (count != 0 && room.panic) {
            room.SetPanic(false);
            failTick = 0f;
        }
    }

    public override void OnInteract(InteractKey key) {
        Debug.Log("Player attempted to supply to " + entityType.entityName);
        // If interact while holding the matching entity type
        Entity topEntity = player.Peek();
        if (topEntity != null && topEntity.entityType.entityName == entityType.entityName) {
            Debug.Log("Player placed " + topEntity.entityType);
            stack.Push(player.Pop());
            if (placeSound) audioSource.PlayOneShot(placeSound);
            OnStackCountChange();
            gameMaster.IncrementScore();
        } else {
            if (wrongSound) audioSource.PlayOneShot(wrongSound);
            Instantiate(gameMaster.wrongPrefab, transform.position + gameMaster.GetWrongPrefabOffset(), Quaternion.identity);
        }
    }

    public void UpdateDemand() {
        if (stack.spriteStack.Count == 0) return;

        if (demandTick < demandDelay) {
            demandTick += Time.deltaTime;
        } else {
            demandTick = 0;
            Entity entity = stack.Pop();
            Destroy(entity.gameObject);
            OnStackCountChange();
        }
    }

    public void UpdateState() {
        if (room.alive) {
            if (room.panic) {
                if (failTick < failTimer) failTick += Time.deltaTime;
                else {
                    room.OnFail(true);
                }
            }
        }
    }
}
