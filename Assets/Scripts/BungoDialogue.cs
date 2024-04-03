using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BungoDialogue : MonoBehaviour
{
    private DialogueInteraction Dinter;
    private Animator anim;
    public Animator UIanim;
    public DialogueManager Dmanager;
    public bool isBungo1;
    public bool isBungo2;
    public GameObject Bungo_MainCam;
    public GameObject CamOfInterest;
    public GameObject OtherBungo;
    public GameObject StarShard;
    private TRADCONTROL GUMDROP;
    public GameObject bungoExitPortal;

    // Start is called before the first frame update
    void Start()
    {
        Dinter = GetComponent<DialogueInteraction>();
        anim = GetComponent<Animator>();
        GUMDROP = FindObjectOfType<TRADCONTROL>();
        if (isBungo1 == true)
        {
            bungoExitPortal = null;
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Dinter.midconvo == true)
        {
            anim.SetBool("isTalking", true);
            Bungo_MainCam.SetActive(true);

            if (Input.GetButtonDown("Interact") & Dinter.CanPass == true & Dmanager.CPanim.GetCurrentAnimatorStateInfo(0).IsName("CanPassICON_ON"))
            {
                TalkingAnims1();
            }
            //for the deity reveal cutscene VVV
            //if (Dmanager.Snumber == 46 || Dmanager.Snumber == 47 || Dmanager.Snumber == 48 || Dmanager.Snumber == 49 || Dmanager.Snumber == 50 || Dmanager.Snumber == 51)
            //{
                //Dinter.CanPass = false;
                //Dmanager.CPanim.SetBool("ON", false);
            //}
            //for the race ending VVV
            if (Dmanager.Snumber >= 79)
            {
                Dinter.CanPass = false;
                Dmanager.CPanim.SetBool("ON", false);
            }
            if (isBungo2 == true && Dmanager.Snumber >= 17)
            {
                Dinter.CanPass = false;
                Dmanager.CPanim.SetBool("ON", false);
            }

            //VVV Used for instances of skippable dialogue.
            if (Dinter.canSkipDialogue == true && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
            {
                Debug.Log("SkippingDialogue");
                StartCoroutine(Dinter.SkipAllDialogue());
                Dinter.canSkipDialogue = false;
                StartCoroutine(CompleteSkippedDialogueTasks(1.10f));
            }
            //^^^
        }
        if (Dinter.midconvo == false)
        {
            anim.SetBool("isTalking", false);
            Bungo_MainCam.SetActive(false);
        }
    }

    public void TalkingAnims1()
    {
        if (isBungo1 == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber == 0)
            {
                anim.SetBool("Happy", true);
            }
            if (Dmanager.Snumber == 1)
            {
                anim.SetBool("Happy", false);
                anim.SetBool("Neutral", true);
            }
            if (Dmanager.Snumber == 2)
            {
                anim.SetBool("Happy", true);
                anim.SetBool("Neutral", false);
            }
            if (Dmanager.Snumber == 3)
            {
                anim.SetBool("Happy", false);
                anim.SetBool("Neutral", true);

                //Skippable dialogue scene
                Dinter.canSkipDialogue = true;
            }
            if (Dmanager.Snumber == 4)
            {
                anim.SetBool("Delighted", true);
                anim.SetBool("Neutral", false);
            }
            if (Dmanager.Snumber == 5)
            {
                anim.SetBool("Delighted", false);
                anim.SetBool("Happy", true);
            }
            if (Dmanager.Snumber == 6)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("Happy", false);
            }
            if (Dmanager.Snumber == 8)
            {
                anim.SetBool("Neutral", false);
                anim.SetTrigger("ReturnToNormal");
            }
            if (Dmanager.Snumber == 9)
            {
                anim.SetBool("Neutral", true);
            }
            if (Dmanager.Snumber == 10)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Delighted", true);
            }
            if (Dmanager.Snumber == 11)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("Delighted", false);
                Dmanager.nameText.text = "Bimpus";
            }
            if (Dmanager.Snumber == 16)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Delighted", true);
                CamOfInterest.SetActive(true);
            }
            if (Dmanager.Snumber == 17)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("Delighted", false);
            }
            if (Dmanager.Snumber == 18)
            {
                Destroy(CamOfInterest);
            }
            if (Dmanager.Snumber == 19)
            {
                anim.SetBool("Neutral", false);
                anim.SetTrigger("ReturnToNormal");
                CamOfInterest = GameObject.Find("Bungo_Cam1");
            }
            if (Dmanager.Snumber == 20)
            {
                anim.SetBool("Delighted", false);
                anim.SetBool("Neutral", true);
            }
            if (Dmanager.Snumber == 21)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("TPOSE", false);
            }
            if (Dmanager.Snumber == 23)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("TPOSE", true);
            }
            if (Dmanager.Snumber == 24)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("TPOSE", false);
            }
            if (Dmanager.Snumber == 26)
            {
                CamOfInterest.GetComponent<Camera>().enabled = true;
            }
            if (Dmanager.Snumber == 30)
            {
                Destroy(CamOfInterest);
            }
            if (Dmanager.Snumber == 32)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Delighted", true);
            }
            if (Dmanager.Snumber == 33)
            {
                anim.SetTrigger("ReturnToNormal");
                anim.SetBool("Delighted", false);
            }
            if (Dmanager.Snumber == 36)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("TPOSE", true);
            }
            if (Dmanager.Snumber == 37)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("TPOSE", false);
            }
            if (Dmanager.Snumber == 38)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Delighted", true);
            }
            if (Dmanager.Snumber == 39)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("Delighted", false);
            }
            if (Dmanager.Snumber == 42)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Nothing", true);
            }
            if (Dmanager.Snumber == 43)
            {
                anim.SetBool("Happy", true);
                anim.SetBool("Nothing", false);
            }
            if (Dmanager.Snumber == 45)
            {
                anim.SetBool("Happy", false);
                anim.SetBool("Neutral", true);
                UIanim.SetTrigger("DeityCutscene");
            }
            if (Dmanager.Snumber == 46)
            {
                anim.SetBool("Happy", false);
                anim.SetBool("Neutral", true);
                if (Input.GetButtonUp("Interact"))
                {
                    //start UI cutscene
                    //Dinter.CanPass = false;
                }
            }
            if (Dmanager.Snumber == 51)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Happy", true);
            }
            if (Dmanager.Snumber == 52)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("Happy", false);
            }
            if (Dmanager.Snumber == 55)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Lament", true);
            }
            if (Dmanager.Snumber == 56)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("Lament", false);
            }
            if (Dmanager.Snumber == 58)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Lament", true);
            }
            if (Dmanager.Snumber == 59)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("Lament", false);
            }
            if (Dmanager.Snumber == 62)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Lament", true);
            }
            if (Dmanager.Snumber == 64)
            {
                anim.SetBool("Lament", false);
                anim.SetBool("Neutral", true);
            }
            if (Dmanager.Snumber == 65)
            {
                //bimpis takes out his star shard
                anim.SetBool("Neutral", false);
                anim.SetTrigger("StarShardReveal");
            }
            if (Dmanager.Snumber == 71)
            {
                anim.SetTrigger("StarShardEnd");
            }
            if (Dmanager.Snumber == 72)
            {
                anim.SetBool("Neutral", true);
                CamOfInterest = GameObject.Find("Bungo_Cam2");
            }
            if (Dmanager.Snumber == 74)
            {
                CamOfInterest.GetComponent<Camera>().enabled = true;
            }
            if (Dmanager.Snumber == 76)
            {
                Destroy(CamOfInterest);
                anim.SetBool("Neutral", false);
                anim.SetBool("Delighted", true);

                //Skippability ends
                Dinter.canSkipDialogue = false;
            }
            if (Dmanager.Snumber == 77)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("Delighted", false);
            }
            if (Dmanager.Snumber == 78)
            {
                //begin ending sequence, dinter can pass = false
                anim.SetBool("Neutral", false);
                anim.SetTrigger("BeginRace");
            }
            if (Dmanager.Snumber >= 79)
            {
                Dinter.CanPass = false;
            }
        }
        if (isBungo2 == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber == 2)
            {
                //anim.SetBool("Neutral", true);
            }
            if (Dmanager.Snumber == 3)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Happy", true);
            }
            if (Dmanager.Snumber == 4)
            {
                anim.SetBool("Happy", false);
                anim.SetTrigger("ReturnToNormal");
            }
            if (Dmanager.Snumber == 5)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Miffed", true);
                CamOfInterest.SetActive(true);
            }
            if (Dmanager.Snumber == 6)
            {
                anim.SetBool("Neutral", true);
                anim.SetBool("Miffed", false);
                CamOfInterest.SetActive(false);
            }
            if (Dmanager.Snumber == 8)
            {
                //bimpis takes out his star shard
                anim.SetBool("Neutral", false);
                anim.SetTrigger("StarShardReveal");
            }
            if (Dmanager.Snumber == 9)
            {
                anim.SetBool("Neutral", true);
            }
            if (Dmanager.Snumber == 10)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Nothing", true);
                CamOfInterest.SetActive(true);
            }
            if (Dmanager.Snumber == 11)
            {
                anim.SetBool("Nothing", false);
                anim.SetBool("Aef", true);
            }
            if (Dmanager.Snumber == 13)
            {
                anim.SetBool("Aef", false);
                anim.SetBool("Neutral", true);
                CamOfInterest.SetActive(false);
            }
            if (Dmanager.Snumber == 14)
            {
                anim.SetBool("Neutral", false);
                anim.SetBool("Delighted", true);
            }
            if (Dmanager.Snumber == 15)
            {
                anim.SetBool("Delighted", false);
                anim.SetBool("Neutral", true);
            }
            if (Dmanager.Snumber == 16)
            {
                anim.SetBool("Neutral", false);
                anim.SetTrigger("Ending");
                CamOfInterest.SetActive(true);

            }
        }
    }
    public void DSkip()
    {
        Dmanager.SkipDialogue();
    }
    public void DSkipAndKillTrigger()
    {
        Dmanager.SkipDialogue();
        Destroy(GameObject.Find("Bungo_KillTrigger"));
    }

    public void BungoEnd1()
    {
        GUMDROP.tag = "Player";
        Dinter.notalk = true;
        FindObjectOfType<CameraRotation>()._LocalRotation = new Vector3(-90f, 0f, 0f);
    }
    public void BungoEnd2()
    {
        bungoExitPortal.SetActive(true);
        Destroy(gameObject);
    }
    public void Bungo_SS_Reveal()
    {
        StarShard.SetActive(true);
    }
    public void Bungo_SS_PutAway()
    {
        StarShard.GetComponent<Animator>().SetTrigger("StarShardDisappear");
    }
    public void Bungo_SS_PutAway2()
    {
        StarShard.SetActive(false);
    }
    public void Activate_OtherBungo()
    {
        OtherBungo.SetActive(true);
        Animator OB_anim = OtherBungo.GetComponent<Animator>();
        OB_anim.SetBool("isGone", false);
        OB_anim.SetTrigger("ReturnToNormal");
        Destroy(GameObject.Find("Bungo_KillTrigger"));
        Destroy(gameObject);
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
        anim.SetBool("Neutral", false);
        anim.SetBool("Happy", false);
        anim.SetBool("Delighted", false);
        anim.SetBool("Lament", false);
        anim.SetBool("Aef", false);
        anim.SetBool("Miffed", false);
        anim.SetBool("Nothing", false);
        anim.SetBool("TPOSE", false);
        anim.SetTrigger("BeginRace");
        Destroy(CamOfInterest);
    }
}