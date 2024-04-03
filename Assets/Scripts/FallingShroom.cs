using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingShroom : MonoBehaviour
{
    public MushroomDialogue MD;
    public DialogueInteraction Dinter;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (MD.isBudShroomFall == true && Dinter.AlreadyTalked == true)
        {
            MD.GetComponent<Animator>().SetTrigger("Fall");
        }
    }
    public void DestroyScripts()
    {
        Destroy(MD);
        Dinter.lookRadius = 0f;
        Destroy(this);
        Debug.Log("Deleted MD and Dinter of FallingShroom");
    }
}
