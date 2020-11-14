using UnityEngine;
using UnityEditor.AI;
using UnityEngine.AI;

public class PerlinCubeGenerationScript00 : MonoBehaviour
{
    public float PerlinNoise = 0f;
    public float Refinement = 0f;
    public float Multiplier = 0f;
    public int Cubes = 0;
    public NavMeshSurface Surface;

    void Start()
    {
        GenerateCubes();
        Surface.BuildNavMesh();
    }

    private void GenerateCubes()
    {
        var minimum = float.MaxValue;

        for (int i = 0; i < Cubes; i++)
        {
            for (int j = 0; j < Cubes; j++)
            {
                PerlinNoise = Mathf.PerlinNoise(i * Refinement, j * Refinement);
                var gameObject0 = GameObject.CreatePrimitive(PrimitiveType.Cube);
                gameObject0.transform.position = new Vector3(i, PerlinNoise * Multiplier, j);

                if (minimum > PerlinNoise * Multiplier)
                    minimum = PerlinNoise * Multiplier;
            }
        }

        var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3(Mathf.Sqrt(Cubes) + 1, 1, Mathf.Sqrt(Cubes) + 1);
        plane.transform.position = new Vector3(Cubes / 2, minimum - 0.25f, Cubes / 2);
    }

    void Update()
    {

    }
}
