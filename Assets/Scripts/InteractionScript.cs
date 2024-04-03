using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    private TRADCONTROL GUMDROP;

    void Start()
    {
        GUMDROP = GetComponent<TRADCONTROL>();
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Refresh"))
        {
            StartCoroutine(Shrink(other.gameObject, 0.5f));
            StartCoroutine(ActivateObject(other.gameObject, 5));
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("EnemyWall"))
        {
            if (GUMDROP._isDashing == true)
            {
                GUMDROP.anim.SetTrigger("EnemyWallBump");
            }
        }

    }
    private IEnumerator ActivateObject(GameObject target, float time)
    {

        yield return new WaitForSeconds(time);
        target.SetActive(true);
    }

    private IEnumerator Shrink(GameObject target, float time)
    {
        yield return new WaitForSeconds(time);
        target.SetActive(false);
    }

}
