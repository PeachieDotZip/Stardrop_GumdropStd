using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiDialogue : MonoBehaviour
{
    private DialogueInteraction Dinter;
    private Animator anim;
    public DialogueManager Dmanager;
    // Start is called before the first frame update
    void Start()
    {
        Dinter = GetComponent<DialogueInteraction>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Dinter.midconvo == true)
        {
            anim.SetBool("isTalking", true);

            if (Input.GetButtonDown("Interact") & Dinter.CanPass == true & Dmanager.CPanim.GetCurrentAnimatorStateInfo(0).IsName("CanPassICON_ON"))
            {
            }
        }
        if (Dinter.midconvo == false)
        {
            anim.SetBool("isTalking", false);
        }
    }
}