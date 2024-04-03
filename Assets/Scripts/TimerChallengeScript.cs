// This is the main script for handling the timer challenge activation and process.
// The parent script is referenced and used for its animation stuff.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerChallengeScript : MonoBehaviour
{
    public Collider timerTrigger;
    public Animator anim;
    public TRADCONTROL gumdrop;

    // Start is called before the first frame update
    private void Start()
    {
        timerTrigger = GetComponent<Collider>();
        anim = GetComponentInParent<Animator>();
        gumdrop = FindObjectOfType<TRADCONTROL>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if ((other.gameObject.CompareTag("Player") || (other.gameObject.CompareTag("PlayerHurt"))) && gumdrop._isDashing == true)
        {
            timerTrigger.enabled = false;
            anim.SetTrigger("Triggered");
        }
    }
}
