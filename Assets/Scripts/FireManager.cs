using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireManager : MonoBehaviour
{
    GameObject[] _burnableObjects;
    void Start()
    {
        _burnableObjects = GameObject.FindGameObjectsWithTag("Burnable");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
