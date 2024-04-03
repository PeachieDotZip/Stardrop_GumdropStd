using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;
    public Queue<string> sentences;
    public Queue<string> sentences2;
    public Queue<string> sentences3;
    public Queue<string> sentences4;
    public Animator anim;
    public Animator CPanim;
    public DialogueInteraction Dinter;
    public CameraRotation camrot;
    public TRADCONTROL GUMDROP;
    Transform target;
    public int Snumber = 0;
    private GameObject skipText;
    void Start()
    {
        sentences = new Queue<string>();
        sentences2 = new Queue<string>();
        sentences3 = new Queue<string>();
        sentences4 = new Queue<string>();
        target = PlayerManager.instance.player.transform;
        skipText = GameObject.Find("SkipDialogueText");
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("NPC"))
        {
            Dinter = other.gameObject.GetComponent<DialogueInteraction>();
        }
    }
    void Update()
    {
        CPanim.SetBool("ON", Dinter.CanPass);
        CPanim.SetBool("midconvo", Dinter.midconvo);
        skipText.SetActive(Dinter.canSkipDialogue);
    }
    public void StartDialogue(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);
        Snumber = 0;
        nameText.text = dialogue.name;
        sentences.Clear();
        anim.SetBool("isOpen", true);
        CPanim.ResetTrigger("Click");
        camrot.CameraDisabled = true;
        if (GUMDROP._controller.isGrounded)
        {
            GUMDROP.tag = ("PlayerTalking");
            GUMDROP.AllowedToLook = false;
        }
        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }
        CPanim.SetBool("ON", false);
        DisplayNextSentence();
    }
    public void StartDialogue2(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);
        Snumber = 0;
        nameText.text = dialogue.name;
        sentences2.Clear();
        anim.SetBool("isOpen", true);
        CPanim.ResetTrigger("Click");
        camrot.CameraDisabled = true;
        if (GUMDROP._controller.isGrounded)
        {
            GUMDROP.tag = ("PlayerTalking");
            GUMDROP.AllowedToLook = false;
        }
        foreach (string sentence2 in dialogue.sentences2)
        {
            sentences2.Enqueue(sentence2);
        }
        CPanim.SetBool("ON", false);
        DisplayNextSentence();
    }
        public void StartDialogue3(Dialogue dialogue)
        {
        Debug.Log("Starting conversation with " + dialogue.name);
        Snumber = 0;
        nameText.text = dialogue.name;
        sentences3.Clear();
        anim.SetBool("isOpen", true);
        CPanim.ResetTrigger("Click");
        camrot.CameraDisabled = true;
        if (GUMDROP._controller.isGrounded)
            {
                GUMDROP.tag = ("PlayerTalking");
               GUMDROP.AllowedToLook = false;
            }
            foreach (string sentence3 in dialogue.sentences3)
            {
                sentences3.Enqueue(sentence3);
            }
        CPanim.SetBool("ON", false);
        DisplayNextSentence();
    }
    public void StartDialogue4(Dialogue dialogue)
    {
        Debug.Log("Starting conversation with " + dialogue.name);
        Snumber = 0;
        nameText.text = dialogue.name;
        sentences4.Clear();
        anim.SetBool("isOpen", true);
        CPanim.ResetTrigger("Click");
        camrot.CameraDisabled = true;
        if (GUMDROP._controller.isGrounded)
        {
            GUMDROP.tag = ("PlayerTalking");
            GUMDROP.AllowedToLook = false;
        }
        foreach (string sentence4 in dialogue.sentences4)
        {
            sentences4.Enqueue(sentence4);
        }
        CPanim.SetBool("ON", false);
        DisplayNextSentence();
    }
    public void DisplayNextSentence()
    {
        if (sentences.Count == 0 & Dinter.AlreadyTalked == false & Dinter.EventTrigger == false)
        {
            EndDialogue();
            return;
        }
        if (sentences2.Count == 0 & Dinter.AlreadyTalked == true & Dinter.EventTrigger == false)
        {
            EndDialogue();
            return;
        }
        if (sentences3.Count == 0 & Dinter.EventTrigger == true & Dinter.AlreadyTalked == false)
        {
            EndDialogue();
            return;
        }
        if (sentences4.Count == 0 & Dinter.EventTrigger == true & Dinter.AlreadyTalked == true)
        {
            EndDialogue();
            return;
        }

        if (Dinter.AlreadyTalked == false && Dinter.EventTrigger == false)
        {
            string sentence = sentences.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence(sentence));
            Debug.Log("''" + sentence + "''");
        }
        if (Dinter.AlreadyTalked == true && Dinter.EventTrigger == false)
        {
            string sentence2 = sentences2.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence2(sentence2));
            Debug.Log("''" + sentence2 + "''");
        }
        if (Dinter.EventTrigger == true && Dinter.AlreadyTalked == false)
        {
            string sentence3 = sentences3.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence3(sentence3));
            Debug.Log("''" + sentence3 + "''");
        }
        if (Dinter.EventTrigger == true && Dinter.AlreadyTalked == true)
        {
            string sentence4 = sentences4.Dequeue();
            StopAllCoroutines();
            StartCoroutine(TypeSentence4(sentence4));
            Debug.Log("''" + sentence4 + "''");
        }
        Snumber += 1;
        StartCoroutine(DClick(.1f));
    }

    public IEnumerator DClick(float time)
    {
        CPanim.SetTrigger("Click");
        yield return new WaitForSeconds(time);
        CPanim.ResetTrigger("Click");
    }
        

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            Dinter.CanPass = false;
            dialogueText.text += letter;
            if (Input.GetButtonDown("Jump"))
            {
                dialogueText.text = sentence;
                break;
                //Prints entire sentence at once
            }
            yield return null;
        }
        yield return new WaitForSeconds(.05f);
        Dinter.CanPass = true;
    }
    IEnumerator TypeSentence2(string sentence2)
    {
        dialogueText.text = "";
        foreach (char letter in sentence2.ToCharArray())
        {
            Dinter.CanPass = false;
            dialogueText.text += letter;
            if (Input.GetButtonDown("Jump"))
            {
                dialogueText.text = sentence2;
                break;
                //Prints entire sentence at once
            }
            yield return null;
        }
        yield return new WaitForSeconds(.05f);
        Dinter.CanPass = true;
    }
    IEnumerator TypeSentence3(string sentence3)
    {
        dialogueText.text = "";
        foreach (char letter in sentence3.ToCharArray())
        {
            Dinter.CanPass = false;
            dialogueText.text += letter;
            if (Input.GetButtonDown("Jump"))
            {
                dialogueText.text = sentence3;
                break;
                //Prints entire sentence at once
            }
            yield return null;
        }
        yield return new WaitForSeconds(.05f);
        Dinter.CanPass = true;
    }
    IEnumerator TypeSentence4(string sentence4)
    {
        dialogueText.text = "";
        foreach (char letter in sentence4.ToCharArray())
        {
            Dinter.CanPass = false;
            dialogueText.text += letter;
            if (Input.GetButtonDown("Jump"))
            {
                dialogueText.text = sentence4;
                break;
                //Prints entire sentence at once
            }
            yield return null;
        }
        yield return new WaitForSeconds(.05f);
        Dinter.CanPass = true;
    }
    public IEnumerator Reset(float time)
    {
        yield return new WaitForSeconds(time);
        GUMDROP.tag = ("Player");
        GUMDROP.enabled = true;
        GUMDROP.AllowedToLook = true;
        camrot.ApplySavedFloats();
        CPanim.SetBool("ON", true);
        GUMDROP._jumpSpeed = 2f;
        //Snumber = 0;
    }
    public IEnumerator PostEndDialogueEventTrigger(float time)
    {
        yield return new WaitForSeconds(time);
        Dinter.TriggerEvent();
        Dinter.TriggerEventPostDialogue = false;
    }
    public IEnumerator GiveStarShardPostDialogue(float time)
    {
        yield return new WaitForSeconds(time);
        Dinter.GiveStarShard();
        Dinter.GiveSS = false;
    }
    public void SkipDialogue()
    {
        DisplayNextSentence();
    }
    public void EndDialogue()
    {
        Debug.Log("End of conversation.");
        anim.SetBool("isOpen", false);
        Dinter.midconvo = false;
        camrot.CameraDisabled = false;
        StartCoroutine(Reset(0.3f));
        Snumber = 0;
        Dinter.AlreadyTalked = true;
        Dinter.ApplySavedlookRadius();
        if (Dinter.TriggerEventPostDialogue == true)
        {
            StartCoroutine(PostEndDialogueEventTrigger(.021f));
        }
        if (Dinter.GiveSS == true)
        {
            StartCoroutine(GiveStarShardPostDialogue(.021f));
        }
    }

}