using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BobAnim : MonoBehaviour
{
    public float frequency = 1.0f;
    public float amplitude = 0.25f;

    public float ticker = 0.0f;
    public float value = 0.0f;

    public Vector3 startPosition = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        ticker += Time.deltaTime * frequency;
        if (ticker > Mathf.PI * 2) {
            ticker = ticker - (Mathf.PI * 2);
        }

        value = Mathf.Sin(ticker) * amplitude;
        transform.position = startPosition + new Vector3(0, value, 0);


        // transform.position += new Vector3(0, Mathf.Sin(ticker), 0);
    }
}
