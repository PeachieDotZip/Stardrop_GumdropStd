using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownSeed : MonoBehaviour
{
    public BrownSeedRespawner respawner;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RespawnTrigger"))
        {
            Debug.Log("Disabled " + gameObject.name);

            StartCoroutine(DeactivateSeed(gameObject, 1f));
            StartCoroutine(respawner.BrownRespawn(gameObject, 3f));

        }
    }
    private IEnumerator DeactivateSeed(GameObject gameObject, float time)
    {
        yield return new WaitForSeconds(time);
        GetComponent<Rigidbody>().isKinematic = true;
        GetComponent<Rigidbody>().useGravity = false;
    }
}
