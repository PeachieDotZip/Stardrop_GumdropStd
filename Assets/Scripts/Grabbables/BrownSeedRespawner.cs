using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrownSeedRespawner : MonoBehaviour
{
    public BrownSeed BrownSeed;
    public Transform BSeed;
    public Transform BSeedSP;

    public IEnumerator BrownRespawn(GameObject BSeed, float time)
    {
        yield return new WaitForSeconds(time);
        Debug.Log("Enabled " + gameObject.name);
        BSeed.transform.position = BSeedSP.transform.position;
        BrownSeed.GetComponent<Rigidbody>().isKinematic = false;
        BrownSeed.GetComponent<Rigidbody>().useGravity = true;
    }

}
