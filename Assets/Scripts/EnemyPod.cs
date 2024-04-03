using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPod : MonoBehaviour
{
    public GameObject towerSnipper;
    public GameObject scoutSnipper;
    public GameObject spawnEffect;


    public void SpawnSnipper()
    {
        Instantiate(spawnEffect, transform.position, Quaternion.identity);
        int snipperInt = Random.Range(1, 4);
        if (snipperInt == 1)
        {
            Instantiate(scoutSnipper, transform.position, Quaternion.identity);
        }
        else
        {
            Instantiate(towerSnipper, new Vector3(transform.position.x, 357.9f, transform.position.z), Quaternion.identity);
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            SpawnSnipper();
        }
    }
}
