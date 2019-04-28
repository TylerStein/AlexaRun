using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameMaster gameMaster;
    public EntityStack entityStack;
    public int stackLimit = 3;
    public float stackOffset = 0.12f;
    public float maxStackXOffset = 0.1f;

    public float maxX = 5;
    public float minX = 5;

    public Animator spriteAnimator;
    public SpriteRenderer spriteRenderer;
    public float moveSpeed = 2.0f;
    public Entity heldEntity;
    public Interactive nearbyInteractive;
    public Transform holdTransform;

    public AudioSource runAudioSource;

    public AudioClip runAudioClip;

    public bool lookRight;
    public bool fail;

    public bool moving = false;

    public void Start() {
        runAudioSource.loop = true;
        runAudioSource.clip = runAudioClip;
        runAudioSource.Play();
        runAudioSource.Pause();
        fail = false;
        spriteAnimator.SetBool("fail", false);
        spriteAnimator.SetBool("holding", false);
    }

    public void Interact(InteractKey key) {
        if (fail) return;

        if (nearbyInteractive != null) {
            nearbyInteractive.OnInteract(key);
        }
    }

    public bool CanHoldEntity() {
        return (entityStack.spriteStack.Count < stackLimit);
    }

    public bool Push(Entity value) {
        if (entityStack.spriteStack.Count >= stackLimit) return false;
        value.sprite.sortingLayerName = spriteRenderer.sortingLayerName;
        entityStack.Push(value);
        spriteAnimator.SetBool("holding", true);
        return true;
    }

    public Entity Pop() {
        if (entityStack.spriteStack.Count == 0) return null;
        Entity e = entityStack.Pop();
        if (entityStack.spriteStack.Count == 0) spriteAnimator.SetBool("holding", false);
        return e;
    }

    public Entity Peek() {
        if (entityStack.spriteStack.Count == 0) return null;
        return entityStack.Peek();
    }

    public void Update() {
        if (fail || gameMaster.paused) return;

        float horizontal = Input.GetAxis("Horizontal");

        if (horizontal > 0 && !lookRight) {
            lookRight = true;
            spriteRenderer.flipX = true;
            UpdateStackFlip();
        } else if (horizontal < 0 && lookRight) {
            lookRight = false;
            spriteRenderer.flipX = false;
            UpdateStackFlip();
        }

        float absHorizontal = Mathf.Abs(horizontal);
        if(absHorizontal > 0.05) {
            if (!moving) {
                moving = true;
                runAudioSource.UnPause();
            }
        } else if (moving) {
            runAudioSource.Pause();
            moving = false;
        }

        spriteAnimator.SetFloat("absMove", absHorizontal);

        float moveAmt = horizontal * Time.deltaTime * moveSpeed;
        transform.position = transform.position + Vector3.right * moveAmt;
        if (transform.position.x > maxX) {
            transform.position = new Vector3(maxX - 0.1f, transform.position.y, transform.position.z);
        } else if (transform.position.x < minX) {
            transform.position = new Vector3(minX + 0.1f, transform.position.y, transform.position.z);
        }

        if (Input.GetButtonDown("Jump")) {
            Interact(InteractKey.SPACE);
        } else if (Input.GetButtonDown("Up")) {
            Interact(InteractKey.UP);
        } else if (Input.GetButtonDown("Down")) {
            Interact(InteractKey.DOWN);
        }
    }

    public void UpdateStackFlip() {
        foreach (Entity e in entityStack.spriteStack) {
            e.sprite.flipX = !lookRight;
        }
    }

    public void SetFailed() {
        runAudioSource.Stop();
        fail = true;
        spriteAnimator.SetBool("fail", true);
        spriteAnimator.SetBool("holding", false);
        spriteAnimator.SetFloat("absMove", 0);
        foreach (Entity e in entityStack.spriteStack) {
            Destroy(e.gameObject);
        }
        entityStack.spriteStack.Clear();
    }
}
