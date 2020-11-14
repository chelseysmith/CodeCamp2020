using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerlinGenerationScript00 : MonoBehaviour
{
    public Vector2 PerlinPosition;
    public float PerlinNoise = 0f;

    void Start()
    {

    }

    void Update()
    {
        PerlinNoise = Mathf.PerlinNoise(PerlinPosition.x, PerlinPosition.y);
    }
}
