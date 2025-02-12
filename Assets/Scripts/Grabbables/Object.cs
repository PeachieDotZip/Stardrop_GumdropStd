﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Object : MonoBehaviour
{
    public Transform ObjRespawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
     void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RespawnTrigger"))
        {
            gameObject.transform.position = ObjRespawnPoint.position;

            Debug.Log("Respawned " + gameObject.name);
        }
    }

}
