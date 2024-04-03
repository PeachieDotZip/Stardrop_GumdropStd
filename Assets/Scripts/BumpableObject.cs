using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BumpableObject : MonoBehaviour
{
    private Animator anim;
    private TRADCONTROL GUMDROP;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        GUMDROP = FindObjectOfType<TRADCONTROL>();
    }


    void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") || (other.gameObject.CompareTag("PlayerHurt"))) && GUMDROP._isDashing == true)
        {
            anim.SetTrigger("Bump");
            Debug.Log("BUMPD");
        }
    }
}
