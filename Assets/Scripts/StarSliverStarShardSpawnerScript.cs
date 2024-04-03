using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarSliverStarShardSpawnerScript : MonoBehaviour
{
    public GameObject starShardToSpawn;
    public GameObject starSliverCamera;
    public ParticleSystem sparkRing;

    public void SummonStarShard()
    {
        starShardToSpawn.SetActive(true);
    }
    public void EnableCamera()
    {
        starSliverCamera.SetActive(true);
    }
    public void DestroyCamera()
    {
        Destroy(starSliverCamera);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            StartCoroutine(EndSequence());
        }
    }
    /// <summary>
    /// Called when StarShard is obtained, aka when the collider attached to this object is touched.
    /// </summary>
    /// <returns></returns>
    public IEnumerator EndSequence()
    {
        sparkRing.Stop();
        yield return new WaitForSeconds(10f);
        Debug.Log("Destroyed StarSliver-Related Objects");
        Destroy(GameObject.Find("StarSliver_StarShardSpot"));
        Destroy(gameObject);
    }
}
