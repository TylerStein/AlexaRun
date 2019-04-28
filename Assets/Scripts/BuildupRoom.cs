using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildupRoom : Room
{
    public BuildupInteractive buildupInteractive;

    public bool panic;

    public GameMaster gameMaster;

    public List<Animator> roomAnimators;
    public EntityType entityType;

    public AudioSource panicSource;

    public void Awake() {
        roomHighlight.enabled = false;
        gameMaster = GameObject.FindGameObjectWithTag("GameMaster").GetComponent<GameMaster>();
        alive = true;
        //for (int i = 0; i < buildupInteractive.startStackCount; i++) {
        //    buildupInteractive.stack.Push();
        //}
        UpdateAnimationStates();
    }

    public override void OnFail(bool tellGameMaster = false) {
        alive = false;
        panicSource.Stop();
        roomHighlight.enabled = true;
        roomHighlight.sprite = gameMaster.highlight_fail;
        buildupInteractive.enabled = false;
        UpdateAnimationStates();
        if (tellGameMaster) gameMaster.OnRoomFail(this);
    }

    public void SetPanic(bool p) {
        if (p != panic) {
            if (p) {
                roomHighlight.enabled = true;
                roomHighlight.sprite = gameMaster.highlight_panic;
                PlayPanicSound();
            } else {
                roomHighlight.enabled = false;
                panicSource.Stop();
            }
        }
        panic = p;
        UpdateAnimationStates();
    }

    public void UpdateAnimationStates() {
        // Debug.Log("Update animation states");
        foreach (Animator a in roomAnimators) {
            a.SetBool("panic", panic);
            a.SetBool("alive", alive);
        }

    }

    public void PlayPanicSound() {
        panicSource.clip = gameMaster.panicSound;
        panicSource.loop = true;
        panicSource.Play();
    }
}
