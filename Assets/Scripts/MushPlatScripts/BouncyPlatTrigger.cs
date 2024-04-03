using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class BouncyPlatTrigger : MonoBehaviour
{
    public Animator anim;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.SetBool("Bounced", true);
        }
    }
    void Bounced()
    {
        anim.SetBool("Bounced", false);
    }
}