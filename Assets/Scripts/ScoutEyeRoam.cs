﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutEyeRoam : MonoBehaviour
{
    private Rigidbody rb;
    public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target);
    }
}
