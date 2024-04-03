using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungoTrigger : MonoBehaviour
{
    [SerializeField]
    private BungoDialogue bungoDialogue;
    [SerializeField]
    private GameObject bungoCSTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (bungoDialogue.gameObject.activeInHierarchy)
            {
                bungoDialogue.Activate_OtherBungo();
                Destroy(bungoCSTrigger);
            }
            Destroy(gameObject);
        }
    }
}
