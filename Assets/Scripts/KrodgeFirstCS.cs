using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KrodgeFirstCS : MonoBehaviour
{
    private Krodge krodgeDialogue;
    private DialogueInteraction Dinter;

    private void Start()
    {
        krodgeDialogue = GetComponent<Krodge>();
        Dinter = GetComponent<DialogueInteraction>();
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if (Dinter.AlreadyTalked == true)
        {
            krodgeDialogue.GetComponent<Animator>().SetTrigger("Exit");
            Destroy(this);
        }
    }
}
