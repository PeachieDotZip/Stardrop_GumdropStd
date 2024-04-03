using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoNotResearch : MonoBehaviour
{
    public DialogueManager Dmanager;
    public Animator anim;
    public GameObject golfHole;

    public void Skip()
    {
        Dmanager.SkipDialogue();
        anim.SetBool("Funni", false);
        golfHole.SetActive(true);
    }
}
