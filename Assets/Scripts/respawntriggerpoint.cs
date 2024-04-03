using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class respawntriggerpoint : MonoBehaviour
{
    public TRADCONTROL GUMDROP;
    public Transform respawnPoint;
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player") || other.gameObject.CompareTag("PlayerHurt"))
        {
            //player.transform.position = respawnPoint.transform.position;
            GUMDROP.respawnPoint = respawnPoint;

        }
    }
}
