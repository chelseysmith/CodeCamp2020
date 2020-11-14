using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinMoveScript00 : MonoBehaviour
{
    public float ElapsedTime = 0f;
    public float PerlineNoise = 0f;
    public float Multiplier = 0f;

    void Start()
    {

    }

    void Update()
    {
        ElapsedTime = Time.time;
        PerlineNoise = Mathf.PerlinNoise(ElapsedTime, 0);
        transform.position = new Vector3(transform.position.x, PerlineNoise, transform.position.y);
    }
}
