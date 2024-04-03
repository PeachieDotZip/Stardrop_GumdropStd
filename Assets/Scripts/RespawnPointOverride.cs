using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnPointOverride : MonoBehaviour
{
    public TRADCONTROL GUMDROP;
    public Transform overridePoint;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            GUMDROP.respawnPoint = overridePoint;
        }
    }
}
