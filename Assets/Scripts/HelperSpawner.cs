using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelperSpawner : MonoBehaviour
{
    public float spawnRadius;
    public GameObject enemyPod;
    public GameObject[] rocks;

    /// <summary>
    /// This function was retuned to work with spawning both enemies AND rocks.
    /// Which thing to spawn is defined by the parameter "type".
    /// </summary>
    /// <param name=""></param>
    public void SpawnEnemyPod(int type)
    {
        switch (type)
        {
            case 0:
                Instantiate(enemyPod, Random.insideUnitSphere * spawnRadius + transform.position, Quaternion.identity);
                break;
            case 1:
                SpawnRandomRock(Random.Range(0, 5)); //random number doesn't include dirt pillar
                break;
            case 2:
                SpawnRandomRock(Random.Range(0, 6)); //random number does include dirt pillar
                break;
            default:
                Debug.Log("Type outside of valid range!");
                break;
        }
    }
    /// <summary>
    /// Spawns random rock from array of rock prefabs.
    /// </summary>
    /// <param name="whichRock"></param>
    public void SpawnRandomRock(int whichRock)
    {
        Instantiate(rocks[whichRock], Random.insideUnitSphere * spawnRadius + transform.position, Quaternion.Euler(new Vector3(0, Random.Range(0, 360), 0)));
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
