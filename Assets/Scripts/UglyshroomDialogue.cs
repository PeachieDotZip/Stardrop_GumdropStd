using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UglyshroomDialogue : MonoBehaviour
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
        }
        else
        {
            anim.SetBool("isTalking", false);
        }
    }
}
