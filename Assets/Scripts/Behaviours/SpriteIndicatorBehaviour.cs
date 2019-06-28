using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteIndicatorBehaviour : MonoBehaviour
{
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private bool isEnabled = false;

    [SerializeField] private float amplitude = 1.0f;
    [SerializeField] private float frequency = 1.0f;

    private float waveTick = 0.0f;
    private Vector3 spriteOrigin = Vector3.zero;

    public void ShowSprite() {
        spriteRenderer.enabled = true;
        spriteOrigin = spriteTransform.position;
        isEnabled = true;
    }

    public void HideSprite() {
        spriteRenderer.enabled = false;
        spriteTransform.position = spriteOrigin;
        isEnabled = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (spriteTransform == null) spriteTransform = transform;
        if (spriteRenderer == null) spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.enabled = isEnabled;
        spriteOrigin = spriteTransform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (isEnabled) {
            waveTick += Time.deltaTime * frequency;
            float verticalPosition = spriteOrigin.y + (Mathf.Sin(waveTick) * amplitude);
            spriteTransform.SetPositionAndRotation(new Vector3(spriteTransform.position.x, verticalPosition, spriteTransform.position.z), spriteTransform.rotation);
        }
    }
}
