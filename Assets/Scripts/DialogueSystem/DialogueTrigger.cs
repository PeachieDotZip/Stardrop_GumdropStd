using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    public DialogueManager Dmanager;

    public void TriggerDialogue()
    {
        if (Dmanager.Dinter.AlreadyTalked == false && Dmanager.Dinter.EventTrigger == false)
        {
            Dmanager.StartDialogue(dialogue);
        }
        if (Dmanager.Dinter.AlreadyTalked == true && Dmanager.Dinter.EventTrigger == false)
        {
            Dmanager.StartDialogue2(dialogue);
        }
        if (Dmanager.Dinter.EventTrigger == true && Dmanager.Dinter.AlreadyTalked == false)
        {
            Dmanager.StartDialogue3(dialogue);
        }
        if (Dmanager.Dinter.AlreadyTalked == true && Dmanager.Dinter.EventTrigger == true)
        {
            Dmanager.StartDialogue4(dialogue);
        }
    }
}
