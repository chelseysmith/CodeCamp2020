﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    public Transform trackedObject;
    public float maxDistance = 10;
    public float moveSpeed = 20;
    public float updateSpeed = 10;

    [Range(0, 10)]
    public float currentDistnace = 5;
    private string moveAxis = "Mouse ScrollWheel";
    private GameObject ahead;
    private MeshRenderer _renderer;
    public float hideDistance = 1.5f;

    // Start is called before the first frame update
    void Start()
    {
        ahead = new GameObject("ahead");
        _renderer = trackedObject.gameObject.GetComponent<MeshRenderer>();
    }

    // LateUpdate is called once per frame
    void LateUpdate()
    {
        ahead.transform.position = trackedObject.position + trackedObject.forward * (maxDistance * 0.25f);
        currentDistnace += Input.GetAxisRaw(moveAxis) * moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, trackedObject.position + Vector3.up * currentDistnace - trackedObject.forward * (currentDistnace + maxDistance * 0.5f), updateSpeed * Time.deltaTime);
        transform.LookAt(ahead.transform);
    }
}
