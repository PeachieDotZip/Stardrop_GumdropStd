using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Krodge : MonoBehaviour
{
    public bool FirstCS;
    private DialogueInteraction Dinter;
    private Animator anim;
    private SphereCollider TalkTrigger;
    public DialogueManager Dmanager;
    public bool StarShardActive = false;
    public GameObject CustomCamera;
    public GameObject StarShard;
    public StarShard StarShardScript;
    private UIScript UIS;
    public Mouse1IconLookAt mouse1icon;

    // Start is called before the first frame update
    void Awake()
    {
        Dinter = GetComponent<DialogueInteraction>();
        anim = GetComponent<Animator>();
        anim.SetInteger("randTalkInt", 2);
        TalkTrigger = GetComponent<SphereCollider>();
        UIS = GameObject.Find("Canvas").GetComponent<UIScript>();
        if (FirstCS == true)
        {
            UIS.TP = GameObject.Find("FirstCSTelePoint");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (FirstCS == true)
        {
            NameChange();
        }
        if (Dinter.midconvo == true)
        {
            anim.SetBool("isTalking", true);
            Dinter.GUMDROP.tag = "PlayerTalking";

            if (Input.GetButtonDown("Interact") & Dinter.CanPass == true & Dmanager.CPanim.GetCurrentAnimatorStateInfo(0).IsName("CanPassICON_ON"))
            {
                int pickAnumber = Random.Range(1, 3);//exclusive never prints the last only goes 1 to 2
                Debug.Log(pickAnumber);
                anim.SetInteger("randTalkInt", pickAnumber);
                WaitingOff();
                if (FirstCS == true)
                {
                    anim.SetTrigger("StarShardReveal_Progress");
                    StarShardReveal();
                    CustomCamera.SetActive(true);
                    UIS.TeleportGMDRP();
                    StartCoroutine(DestroyTelePoint(.1f));
                    Dinter.GUMDROP.tag = "PlayerTalking";
                }
            }
            if (CustomCamera.activeInHierarchy == true)
            {
                mouse1icon.isFacingMainCam = false;
                mouse1icon.Camera1 = CustomCamera.transform;
            }

            //VVV Used for instances of skippable dialogue.
            if (Dinter.canSkipDialogue == true && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
            {
                Debug.Log("SkippingDialogue");
                StartCoroutine(Dinter.SkipAllDialogue());
                Dinter.canSkipDialogue = false;
                StartCoroutine(CompleteSkippedDialogueTasks(1.25f));
            }
            //^^^
        }

        if (Dinter.midconvo == false)
        {
            anim.SetBool("isTalking", false);
        }

        if (StarShardActive == true && StarShard != null)
        {
            StarShard.SetActive(true);
        }
        if (StarShardActive == false && StarShard != null)
        {
            StarShard.SetActive(false);
        }

    }

    public void WaitingOn()
    {
        anim.SetBool("isWaiting", true);
    }
    public void WaitingOff()
    {
        anim.SetBool("isWaiting", false);
    }
    public void EntranceOver()
    {
        anim.SetBool("IdlePuddleRipple", true);
        anim.SetBool("Enter", false);
    }
    public void NameChange()
    {
        if (FirstCS == true)
        {
            if (Dmanager.sentences.Count == 8)
            {
                Dmanager.nameText.text = "Krôdge";
            }
            if (Dmanager.sentences.Count == 7)
            {
                anim.SetBool("isConfused", true);
                anim.SetBool("isWaiting", true);
            }
            if (Dmanager.sentences.Count == 6)
            {
                anim.SetBool("isConfused", false);

                //Skippability ends
                Dinter.canSkipDialogue = false;
            }
        }
    }
    public void StarShardReveal()
    {
        if (FirstCS == true)
        {
            if (Dmanager.sentences.Count == 23)
            {
                //Skippable dialogue scene
                Dinter.canSkipDialogue = true;
            }
            if (Dmanager.sentences.Count == 18)
            {
                anim.SetTrigger("StarShardReveal");
                anim.ResetTrigger("StarShardReveal_Progress");
                StarShardActive = false;
            }
            if (Dmanager.sentences.Count == 17)
            {
                anim.ResetTrigger("StarShardReveal");
            }
            if (Dmanager.sentences.Count == 16)
            {
                anim.ResetTrigger("StarShardReveal_Progress");
            }
            if (Dmanager.sentences.Count == 14)
            {
                anim.ResetTrigger("StarShardReveal_Progress");
            }
        }
    }
    public void StarShardRevealReset()
    {
        anim.ResetTrigger("StarShardReveal");
    }
    public void StarShardReveal_ProgressReset()
    {
        anim.ResetTrigger("StarShardReveal_Progress");
    }
    public void HideStarShard()
    {
        StarShardScript.anim.SetTrigger("StarShardDisappear");
    }
    public void DoSomething()
    {
        if (FirstCS == true)
        {
            Destroy(CustomCamera);
        }
        mouse1icon.isFacingMainCam = true;
        mouse1icon.Camera1 = null;
    }
    public void ReturnToIdleValues()
    {
        CustomCamera.SetActive(false);
        mouse1icon.isFacingMainCam = true;
        mouse1icon.Camera1 = null;
    }
    public void DestroyKrodge()
    {
        GameObject.Find("MushroomActivator").GetComponent<Animator>().SetTrigger("Grow");
        Destroy(GameObject.Find("Krodge_inviswall1"));
        Destroy(gameObject);
    }
    private IEnumerator DestroyTelePoint(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(UIS.TP);
    }
    /// <summary>
    /// Completes the needed tasks that would usually be done DURING the conversation.
    /// IMPORTANT: "time" value must be greater than 1f. This is so that code is run during the black screen.
    /// </summary>
    /// <param name="time"></param>
    /// <returns></returns>
    private IEnumerator CompleteSkippedDialogueTasks(float time)
    {
        yield return new WaitForSeconds(time);
        DoSomething();
    }
}