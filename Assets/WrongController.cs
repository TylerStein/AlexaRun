using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrongController : MonoBehaviour
{
    public float fadeRate = 1.0f;
    public SpriteRenderer spriteRenderer;

    private void Start() {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        float a = spriteRenderer.color.a;
        if (a <= 0) {
            Destroy(gameObject);
        } else {
            a -= fadeRate * Time.deltaTime;
            spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, a);
            if (a < 0) a = 0;
        }
    }
}
