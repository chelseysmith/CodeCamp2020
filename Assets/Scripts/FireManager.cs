using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    [SerializeField] Material fire1;
    [SerializeField] Material fire2;
    [SerializeField] Material fire3;

    List<EntityStats> _burnableObjects = new List<EntityStats>();

    void Start()
    {
        var objects = GameObject.FindGameObjectsWithTag("Burnable");
        foreach (var obj in objects)
        {
            _burnableObjects.Add(obj.GetComponent<EntityStats>());
        }

        _burnableObjects[25].CatchFire(Enums.Fire.level1, fire1);
        _burnableObjects[75].CatchFire(Enums.Fire.level2, fire2);
        _burnableObjects[45].CatchFire(Enums.Fire.level3, fire3);

        StartCoroutine(FireUpdate());
    }

    IEnumerator FireUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);

            for (int i = 0; i < _burnableObjects.Count; i++)
            {
                if (_burnableObjects[i].Fire != Enums.Fire.na)
                {
                    _burnableObjects[i].Tick();
                    
                    if(_burnableObjects[i].Dead)
                    {
                        AttemptSpread(_burnableObjects[i]);

                        _burnableObjects[i].DestroyMe();
                        _burnableObjects.RemoveAt(i);
                        i--; 
                    }
                }
            }
        }
    }

    void AttemptSpread(EntityStats fireStarter)
    {
        var indices = new List<int>();
        for (int i = 0; i < _burnableObjects.Count; i++)
        {
            if (!_burnableObjects[i].Dead)
            {
                if (Vector3.Distance(fireStarter.transform.position, _burnableObjects[i].transform.position) < 1.5f)
                {
                    indices.Add(i);
                }
            }
        }

        foreach (var index in indices)
        {
            _burnableObjects[index].CatchFire(fireStarter.Fire, GetMaterialByType(fireStarter.Fire));
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
