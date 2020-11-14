using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] Material fire1;
    [SerializeField] Material fire2;
    [SerializeField] Material fire3;
    [SerializeField] GameObject _prefab;
    [SerializeField] GameObject _fire1;

    List<EntityStats> _burnableObjects = new List<EntityStats>();

    public float PerlinNoise = 0f;
    public float Refinement = 0f;
    public float Multiplier = 0f;
    public int Cubes = 0;
    public NavMeshSurface Surface;

    void Start()
    {
        GenerateCubes();
        Surface.BuildNavMesh();

        var objects = GameObject.FindGameObjectsWithTag("Burnable");
        foreach (var obj in objects)
        {
            _burnableObjects.Add(obj.GetComponent<EntityStats>());
        }

        _burnableObjects[236].CatchFire(Enums.Fire.level1, fire1, _fire1);
        _burnableObjects[476].CatchFire(Enums.Fire.level2, fire2, _fire1);
        _burnableObjects[789].CatchFire(Enums.Fire.level3, fire3, _fire1);

        StartCoroutine(FireUpdate());
    }

    IEnumerator FireUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            //var objectsDestroyed = false;
            for (int i = 0; i < _burnableObjects.Count; i++)
            {
                if (_burnableObjects[i].Fire != Enums.Fire.na)
                {
                    _burnableObjects[i].Tick();
                    
                    if(_burnableObjects[i].Dead)
                    {
                        //bjectsDestroyed = true;
                        AttemptSpread(_burnableObjects[i]);

                        _burnableObjects[i].DestroyMe();
                        _burnableObjects.RemoveAt(i);
                        i--;
                    }
                }
            }

            //if (objectsDestroyed)
            //    StartCoroutine(BuildNavMesh());
        }
    }

    private void GenerateCubes()
    {
        var minimum = float.MaxValue;

        for (int i = 0; i < Cubes; i++)
        {
            for (int j = 0; j < Cubes; j++)
            {
                PerlinNoise = Mathf.PerlinNoise(i * Refinement, j * Refinement);
                //var gameObject0 = GameObject.CreatePrimitive(PrimitiveType.Cube);

                var gameObject = GameObject.Instantiate(_prefab, new Vector3(i, PerlinNoise * Multiplier, j), Quaternion.identity);
                gameObject.transform.parent = transform;

                //gameObject0.transform.position = new Vector3(i, PerlinNoise * Multiplier, j);

                if (minimum > PerlinNoise * Multiplier)
                    minimum = PerlinNoise * Multiplier;
            }
        }

        var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.localScale = new Vector3(Mathf.Sqrt(Cubes) + 1, 1, Mathf.Sqrt(Cubes) + 1);
        plane.transform.position = new Vector3(Cubes / 2, minimum - 0.25f, Cubes / 2);
    }

    void AttemptSpread(EntityStats fireStarter)
    {
        var indices = new List<int>();
        for (int i = 0; i < _burnableObjects.Count; i++)
        {
            if (!_burnableObjects[i].Dead && _burnableObjects[i].Fire == Enums.Fire.na)
            {
                if (Vector3.Distance(fireStarter.transform.position, _burnableObjects[i].transform.position) < 1.5f)
                {
                    indices.Add(i);
                }
            }
        }

        foreach (var index in indices)
        {
            _burnableObjects[index].CatchFire(fireStarter.Fire, GetMaterialByType(fireStarter.Fire), _fire1);
        }
    }

    Material GetMaterialByType(Enums.Fire fire)
    {
        switch(fire)
        {
            case Enums.Fire.level1:
                return fire1;
            case Enums.Fire.level2:
                return fire2;
            case Enums.Fire.level3:
                return fire3;
            default:
                return fire1;
        }
    }
}
