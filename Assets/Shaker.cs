using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaker : MonoBehaviour
{
    SpriteRenderer sr;

    public float shakeXMin, shakeXMax, shakeYMin, shakeYMax;
    public float shakeInterval = 0f;
    private float shakeTimer = 0f;
    public bool shakeEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (shakeEnabled)
        {
            if (shakeTimer > 0f)
            {
                shakeTimer -= Time.deltaTime;
            }
            else
            {
                shakeTimer = shakeInterval;

                Vector2 r = UnityEngine.Random.insideUnitCircle;
                r.x = Mathf.Clamp(r.x, shakeXMin, shakeXMax);
                r.y = Mathf.Clamp(r.y, shakeYMin, shakeYMax);
                transform.localPosition = r;
            }
        }
    }
}
