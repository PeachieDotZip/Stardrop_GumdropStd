using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileSpawnerTestScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletSpawnPoint;
    public bool isChallengeMode;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P) || isChallengeMode)
        {
            Instantiate(bullet, bulletSpawnPoint.transform.position, Quaternion.identity);
        }
    }
}
