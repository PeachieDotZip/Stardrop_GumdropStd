using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MushroomDialogue : MonoBehaviour
{
    //I'm such a dogshit programmer it's not even funny
    private DialogueInteraction Dinter;
    private Animator anim;
    private SphereCollider TalkTrigger;
    public DialogueManager Dmanager;
    public GameObject CameraOfInterest;
    public Transform LookAtTarget;
    public bool isBudShroom1;
    public bool isBudShroom2;
    public bool isBudShroom3;
    public bool isBudShroom4;
    public bool isBudShroomFall;
    public bool isBudShroomGolf;
    public bool isBudShroomGolf_Lose;
    public bool isBudShroomChef;
    public bool isBudShroomConstruction;
    public GameObject ScaredOf;
    public GameObject ScaredOf2;
    public GameObject ScaredOf3;
    private ParticleSystem Sweat;
    public MiniGolf MG;
    private DialogueTrigger Dtrigger;
    private bool isSweating;
    private MushroomyGolfScript MGS;
    private int pC;
    private Fryingpanscript pan;
    public Animator panim;

    // Start is called before the first frame update
    void Start()
    {
        Dinter = GetComponent<DialogueInteraction>();
        anim = GetComponent<Animator>();
        TalkTrigger = GetComponent<SphereCollider>();
        Sweat = GetComponent<ParticleSystem>();
        MG = FindObjectOfType<MiniGolf>();
        Dtrigger = GetComponent<DialogueTrigger>();
        Sweat.Play();
        MGS = GetComponent<MushroomyGolfScript>();
        if (isBudShroomConstruction == true)
        {
            anim.SetBool("Sleep", true);
            //put code to play *SNORE* MIMIMIMMMIMI sound here, or just do it through other script
        }
        if (isBudShroomChef == true)
        {
            pan = FindObjectOfType<Fryingpanscript>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (Dinter.notalk == true)
        {
            TalkTrigger.enabled = false;
        }
        else
        {
            TalkTrigger.enabled = true;
        }

        if (isSweating == true)
        {
            Sweat.Play();
        }
        else
        {
            Sweat.Stop();
        }

        if (ScaredOf == null & ScaredOf2 == null & ScaredOf3 == null)
        {
            UnScared();

            if (Dinter.midconvo == true)
            {
                anim.SetBool("isTalking", true);
                TalkTrigger.radius = 10;
                //^^^thisshitdont work lol

                //dont do talking anims like this again, put it in the actual update section instead
                if (Input.GetButtonDown("Interact") & Dinter.CanPass == true & Dmanager.CPanim.GetCurrentAnimatorStateInfo(0).IsName("CanPassICON_ON"))
                {
                    TalkingAnims1();
                    TalkingAnims2();
                    TalkingAnims3();
                    TalkingAnims4();
                    TalkingAnimsGolf1();
                    TalkingAnimsGolf2();
                    TalkingAnimsChef();
                }

                //VVV Used for instances of skippable dialogue.
                if (Dinter.canSkipDialogue == true && (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter)))
                {
                    Debug.Log("SkippingDialogue");
                    StartCoroutine(Dinter.SkipAllDialogue());
                    Dinter.canSkipDialogue = false;
                    StartCoroutine(CompleteSkippedDialogueTasks(1.10f));
                }
            }
            if (Dinter.midconvo == false)
            {
                anim.SetBool("isTalking", false);
            }
        }
        else
        {
            anim.SetBool("isPissingPants", true);
            isSweating = true;
            Dinter.lookRadius = 0f;
            Dinter.notalk = true;
        }
        if (MG.GolfWon == true && isBudShroomGolf == true)
        {
            Dtrigger.dialogue.sentences3[1] = "Nice! A hole in " + MG.GHT.FinalGolfScore + "!";
        }
        if (isBudShroomChef == true)
        {
            pC = pan.pearCount;
            Dtrigger.dialogue.sentences2[1] = "Hm? Back so soon?";
            Dtrigger.dialogue.sentences2[2] = "Well then, let's see how many pears we got...";
            if (pC == 0)
            {
                if (Dmanager.Snumber == 3)
                {
                    anim.SetBool("isDazed", true);
                }
                if (Dmanager.Snumber == 6)
                {
                    anim.SetBool("isDazed", false);
                }
                Dtrigger.dialogue.sentences2[3] = "Umm... You... uh...";
                Dtrigger.dialogue.sentences2[4] = "Didn't get any...";
                Dtrigger.dialogue.sentences2[5] = "...Are you sure you're prepared for this job?";
            }
            if (pC == 1 || pC == 2)
            {
                Dtrigger.dialogue.sentences2[3] = "Only " + pC + "? Pssh... Pathetic!";
                Dtrigger.dialogue.sentences2[4] = pC + " is simply not enough! Go out and find me more!";
                Dtrigger.dialogue.sentences2[5] = "Remember: I need at least 3!";
            }
            if (pC == 3)
            {
                if (Dmanager.Snumber == 3)
                {
                    anim.SetBool("isDazed", true);
                }
                if (Dmanager.Snumber == 4)
                {
                    anim.SetBool("isDazed", false);
                }
                Dtrigger.dialogue.sentences2[3] = "Hm... wait... that's 3...?";
                Dtrigger.dialogue.sentences2[4] = "Wait... yeah! That's 3! You actually did it!";
                Dtrigger.dialogue.sentences2[5] = "Nice! Now I just need to complete the dish! One moment please...";
                if (Dmanager.Snumber == 5)
                {
                    Dinter.TriggerEventPostDialogue = true;
                }
            }
            if (Dinter.EventTrigger == true && Dinter.AlreadyTalked == false)
            {
                if (Dmanager.Snumber == 3)
                {
                    CameraOfInterest = GameObject.Find("BudshroomMeal");
                }
                if (Dmanager.Snumber == 4)
                {
                    Camera mealCam = CameraOfInterest.GetComponent<Camera>();
                    Renderer theMealInQuestion = CameraOfInterest.GetComponentInChildren<MeshRenderer>();
                    mealCam.enabled = true;
                    theMealInQuestion.enabled = true;
                    anim.SetBool("isDazed", true);
                }
                if (Dmanager.Snumber == 5)
                {
                    anim.SetBool("isDazed", false);
                    Destroy(CameraOfInterest);
                }
                if (Dmanager.Snumber == 11 || Dmanager.Snumber == 12)
                {
                    Dinter.CanPass = false;
                }
                if (Dmanager.Snumber == 13)
                {
                    Dmanager.nameText.text = ".....";
                }
            }
        }
    }

    //VVV These bools and voids are for assigning specific animations to specific Budshrooms during dialogue VVV
    //FOREWARNING: DO NOT USE MOST OF THIS STUPID FUCKING SHIT OMG, INSTEAD USE THE NEWLY-MADE "Snumber" INT TO COUNT SENTENCES INSTEAD
    public void TalkingAnims1()
    {
        if (isBudShroom1 == true && Dinter.AlreadyTalked == false && Dinter.EventTrigger == false)
        {
            if (Dmanager.sentences.Count == 14)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.sentences.Count == 13)
            {
                anim.SetBool("isDazed", false);
            }
            if (Dmanager.sentences.Count == 10)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.sentences.Count == 8)
            {
                anim.SetBool("isDazed", false);
            }
            if (Dmanager.sentences.Count == 7)
            {
                anim.SetBool("isWorried", true);
            }
            if (Dmanager.sentences.Count == 6)
            {
                CameraOfInterest.SetActive(true);
            }
            if (Dmanager.sentences.Count == 4)
            {
                CameraOfInterest.SetActive(false);
            }
            if (Dmanager.sentences.Count == 2)
            {
                anim.SetBool("isWorried", false);
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.sentences.Count == 1)
            {
                anim.SetBool("isDazed", false);
            }
        }
        if (isBudShroom1 == true && Dinter.AlreadyTalked == true && Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber == 2)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 3)
            {
                GameObject.Find("showoff_tree1").GetComponent<Camera>().enabled = true;
            }
            if (Dmanager.Snumber == 4)
            {
                GameObject.Find("showoff_tree2").GetComponent<Camera>().enabled = true;
                Destroy(GameObject.Find("showoff_tree1"));
            }
            if (Dmanager.Snumber == 5)
            {
                GameObject.Find("showoff_dirt").GetComponent<Camera>().enabled = true;
                Destroy(GameObject.Find("showoff_tree2"));
            }
            if (Dmanager.Snumber == 6)
            {
                GameObject.Find("showoff_mushrooms").GetComponent<Camera>().enabled = true;
                Destroy(GameObject.Find("showoff_dirt"));
            }
            if (Dmanager.Snumber == 7)
            {
                GameObject.Find("showoff_mountain").GetComponent<Camera>().enabled = true;
                Destroy(GameObject.Find("showoff_mushrooms"));
                anim.SetBool("isDazed", false);
            }
            if (Dmanager.Snumber == 10)
            {
                Destroy(GameObject.Find("showoff_mountain"));
                Dinter.TriggerEventPostDialogue = true;
            }
        }
        if (isBudShroom1 == true && Dinter.AlreadyTalked == false && Dinter.EventTrigger == true)
        {
            if (Dmanager.Snumber == 3)
            {
                anim.SetBool("isDazed", true);
            }
        }
        if (isBudShroom1 == true && Dinter.AlreadyTalked == true && Dinter.EventTrigger == true)
        {
            // me when the air smells like air
        }
    }
    public void TalkingAnims2()
    {
        if (isBudShroom2 == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber == 3)
            {
                CameraOfInterest.SetActive(true);
            }
            if (Dmanager.Snumber == 4)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 5)
            {
                CameraOfInterest.SetActive(false);
                anim.SetBool("isDazed", false);
            }
        }
        if (isBudShroom2 == true & Dinter.AlreadyTalked == true & Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber == 3)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 6)
            {
                if (Input.GetButtonDown("Interact"))
                {
                    anim.SetBool("isDazed", false);
                    anim.SetBool("isTalking", false);
                }

            }
        }
        if (isBudShroom2 == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == true)
        {
            if (Dmanager.Snumber == 1 || Dmanager.Snumber == 2)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 6)
            {
                anim.SetBool("isDazed", false);
            }
        }
        if (isBudShroom2 == true & Dinter.AlreadyTalked == true & Dinter.EventTrigger == true)
        {
            if (Dmanager.Snumber == 1 || Dmanager.Snumber == 2)
            {
                anim.SetBool("isDazed", false);
                anim.SetBool("isTalking", true);
            }
        }
    }

    public void TalkingAnims3()
    {
        if (isBudShroom3 == true)
        {
            if (Dmanager.Snumber == 4)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 5)
            {
                anim.SetBool("isDazed", false);
            }
        }
    }
    public void TalkingAnims4()
    {
        if (isBudShroom4 == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber == 3)
            {
                Debug.Log("sojkdoeinfuebfkusehchmcf");
                CameraOfInterest.SetActive(true);
            }
            if (Dmanager.Snumber == 5)
            {
                anim.SetBool("isWorried", true);
                Destroy(CameraOfInterest);
            }
            if (Dmanager.Snumber == 6)
            {
                anim.SetBool("isWorried", false);
            }
        }
        if (isBudShroom4 == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == true)
        {
            if (Dmanager.Snumber == 6)
            {
                Debug.Log("sojkdoeinfuebfkusehchmcf");
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 7)
            {
                anim.SetBool("isWorried", false);
            }
        }
    }

    public void TalkingAnimsGolf1()
    {
        if (isBudShroomGolf == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == false)
        {

            if (Dmanager.sentences.Count == 21)
            {
                anim.SetBool("isWorried", true);
                //Skippable dialogue scene
                Dinter.canSkipDialogue = true;
            }
            if (Dmanager.sentences.Count == 20)
            {
                anim.SetBool("isWorried", false);
            }
            if (Dmanager.sentences.Count == 19)
            {
                anim.SetBool("isDazed", true);
                CameraOfInterest = GameObject.Find("GolfballCamera");
            }
            if (Dmanager.sentences.Count == 18)
            {
                anim.SetBool("isDazed", false);
                CameraOfInterest.GetComponent<Camera>().enabled = true;
            }
            if (Dmanager.sentences.Count == 17)
            {
                CameraOfInterest.GetComponent<Camera>().enabled = false;
            }
            if (Dmanager.sentences.Count == 16)
            {
                CameraOfInterest = GameObject.Find("GlassCamera");
                CameraOfInterest.GetComponent<Camera>().enabled = true;
            }
            if (Dmanager.sentences.Count == 10)
            {
                MGS.camanim1.SetBool("Funni", true);
            }
            if (Dmanager.sentences.Count == 9)
            {
                Dinter.CanPass = false;
            }
            if (Dmanager.sentences.Count == 7)
            {
                MG.isGolfing = true;
            }
            if (Dmanager.sentences.Count == 6)
            {
                anim.SetBool("isDazed", true);
                CameraOfInterest.GetComponent<Camera>().enabled = false;
            }
            if (Dmanager.sentences.Count == 4)
            {
                anim.SetBool("isWorried", true);
                anim.SetBool("isDazed", false);
            }
            if (Dmanager.sentences.Count == 3)
            {
                anim.SetBool("isWorried", false);
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.sentences.Count == 2)
            {
                anim.SetBool("isDazed", false);
                MG.HitTrigger.enabled = true;
                CameraOfInterest.GetComponent<Camera>().enabled = false;

                //Skippability ends
                Dinter.canSkipDialogue = false;
            }
        }
        if (isBudShroomGolf == true & Dinter.AlreadyTalked == true & Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber == 0)
            {
                anim.SetBool("isWorried", false);
            }
            if (Dmanager.Snumber == 3)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 5)
            {
                anim.SetBool("isDazed", false);
                CameraOfInterest = GameObject.Find("marvincam");
            }
            if (Dmanager.Snumber == 9)
            {
                anim.SetBool("isDazed", true);
                Dmanager.nameText.text = ".....";
                CameraOfInterest.GetComponent<Camera>().enabled = true;
            }
            if (Dmanager.Snumber == 13)
            {
                Dmanager.nameText.text = "Budshroom";
                anim.SetBool("isDazed", false);
                CameraOfInterest.GetComponent<Camera>().enabled = false;
            }
        }
        if (isBudShroomGolf == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == true)
        {

            Dtrigger.dialogue.sentences3[1] = "Nice! A hole in " + MG.GHT.FinalGolfScore + "!";

            if (Dmanager.Snumber == 2)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 3)
            {
                anim.SetBool("isDazed", false);
            }
            if (Dmanager.Snumber == 4)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 6)
            {
                anim.SetBool("isDazed", false);
            }
            if (Dmanager.Snumber == 7)
            {
                anim.SetBool("isWorried", true);
                isSweating = true;
            }
            if (Dmanager.Snumber == 8)
            {
                anim.SetBool("isWorried", false);
                isSweating = false;
                Dinter.GiveSS = true;
                Dinter.StarShard.SetActive(true);
            }
            if (Dmanager.Snumber == 9)
            {
                anim.SetBool("isWorried", true);
            }
            if (Dmanager.Snumber == 10)
            {
                anim.SetBool("isWorried", false);
                CameraOfInterest = GameObject.Find("marvincam");
            }
            if (Dmanager.Snumber == 11)
            {
                anim.SetBool("isDazed", true);
                CameraOfInterest.GetComponent<Camera>().enabled = true;
            }
            if (Dmanager.Snumber == 13)
            {
                anim.SetBool("isDazed", false);
                CameraOfInterest.GetComponent<Camera>().enabled = false;
            }
            if (Dmanager.Snumber > 13)
            {
                StartCoroutine(NoTalkAfterDialogue(.0021f));
            }
        }
        if (isBudShroomGolf == true & Dinter.AlreadyTalked == true & Dinter.EventTrigger == true)
        {
            if (Dmanager.Snumber == 3)
            {
                Dmanager.nameText.text = "";
                MG.isGolfing = false;
                MG.GolfHole.SetActive(false);
            }
        }
    }
    public void TalkingAnimsChef()
    {
        if (isBudShroomChef == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber == 2)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 3)
            {
                anim.SetBool("isDazed", false);
            }
            if (Dmanager.Snumber == 4)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 5)
            {
                anim.SetBool("isDazed", false);
            }
            if (Dmanager.Snumber == 6)
            {
                anim.SetBool("isDazed", false);
            }
            if (Dmanager.Snumber == 8)
            {
                anim.SetBool("isWorried", true);
            }
            if (Dmanager.Snumber == 9)
            {
                anim.SetBool("isWorried", false);
            }
            if (Dmanager.Snumber == 11)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber >= 12)
            {
                anim.SetBool("isDazed", false);
                anim.SetBool("isTalking", false);
            }
        }
        if (isBudShroomChef == true & Dinter.AlreadyTalked == true & Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber > 5 && pC >= 3)
            {
                anim.SetTrigger("chefcook");
                panim.SetTrigger("Cook");
            }
        }
        if (isBudShroomChef == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == true)
        {
            if (Dmanager.Snumber == 6)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 7)
            {
                anim.SetBool("isDazed", false);
                Dinter.GiveSS = true;
                Dinter.StarShard.SetActive(true);
            }
            if (Dmanager.Snumber == 10)
            {
                anim.SetTrigger("chefleave");
            }
            if (Dmanager.Snumber == 12)
            {
                Dmanager.nameText.text = ".....";
            }
            if (Dmanager.Snumber == 13)
            {
                Dmanager.nameText.text = ".....";
            }
            if (Dmanager.Snumber > 13)
            {
                StartCoroutine(NoTalkAfterDialogue(.0021f));
                StartCoroutine(DeleteChef());
            }
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag("Player") && isBudShroomGolf == true)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                MG.GToggle = true;
                MG.ToggleGolfMode();
                Debug.Log("oi3qb6j");
            }
        }
    }

    public void TalkingAnimsGolf2()
    {
        if (isBudShroomGolf_Lose == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber == 1)
            {
                anim.SetBool("isWorried", true);
            }
            if (Dmanager.Snumber == 2)
            {
                anim.SetBool("isWorried", false);
            }
        }
        if (isBudShroomGolf_Lose == true & Dinter.AlreadyTalked == true & Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber == 3)
            {
                anim.SetBool("isDazed", true);
                CameraOfInterest = GameObject.Find("GolfballCamera");
                Dinter.TriggerEventPostDialogue = true;
            }
            if (Dmanager.Snumber == 4)
            {
                anim.SetBool("isDazed", false);
                anim.SetTrigger("badaboom");
            }
            if (Dmanager.Snumber == 6)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 7)
            {
                anim.SetBool("isDazed", false);
            }
        }
        if (isBudShroomGolf_Lose == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == true)
        {
            //these statments are the same as the ones for isBudShroom-Sentences2
            if (Dmanager.Snumber == 2)
            {
                CameraOfInterest.GetComponent<Camera>().enabled = false;
            }
            if (Dmanager.Snumber == 3)
            {
                anim.SetBool("isDazed", true);
                CameraOfInterest.GetComponent<Camera>().enabled = false;
            }
            if (Dmanager.Snumber == 6)
            {
                anim.SetBool("isDazed", false);
                CameraOfInterest = GameObject.Find("marvincam");
            }
            if (Dmanager.Snumber == 7)
            {
                anim.SetBool("isDazed", true);
                Dmanager.nameText.text = ".....";
                CameraOfInterest.GetComponent<Camera>().enabled = true;
            }
            if (Dmanager.Snumber == 8)
            {
                Dmanager.nameText.text = ".....";
            }
            if (Dmanager.Snumber == 9)
            {
                CameraOfInterest.GetComponent<Camera>().enabled = false;
            }
        }
        if (isBudShroomGolf_Lose == true & Dinter.AlreadyTalked == true & Dinter.EventTrigger == true)
        {
            if (Dmanager.Snumber == 1)
            {
                anim.SetBool("isTalking", true);
                anim.SetBool("isDazed", false);
            }
                if (Dmanager.Snumber == 3)
            {
                anim.SetBool("isDazed", true);
                MG.isGolfing = true;
            }
            if (Dmanager.Snumber == 4)
            {
                //cut to gumdrop's face
            }
            if (Dmanager.Snumber == 5)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 6)
            {
                anim.SetBool("isDazed", false);
            }
            if (Dmanager.Snumber == 7)
            {
                anim.SetBool("isDazed", true);
            }
            if (Dmanager.Snumber == 9)
            {
                CameraOfInterest = GameObject.Find("marvincam");
                CameraOfInterest.GetComponent<Camera>().enabled = true;
            }
            if (Dmanager.Snumber == 10)
            {
                Dmanager.nameText.text = ".....";
            }
            if (Dmanager.Snumber == 11)
            {
                CameraOfInterest.GetComponent<Camera>().enabled = false;
            }
        }
    }
    public void DSkip()
    {
        Dmanager.SkipDialogue();

        if (isBudShroomGolf_Lose == true)
        {
            MG.GolfStart();
            MG.FunniGolfRespawn();
            anim.ResetTrigger("badaboom");
        }

    }

    public void UnScared()
    {
        anim.SetBool("isPissingPants", false);
        Dinter.lookRadius = 4.5f;
        Dinter.notalk = false;
        isSweating = false;
    }
    public IEnumerator DeleteChef()
    {
        // Resets all variables relating to BudshroomChef and deletes them to stop them from effecting other parts of the game.
        pC = 21;
        //isBudShroomChef = false;
        yield return new WaitForSeconds(.21f);
        Destroy(gameObject);
        Debug.Log("Deleted BudshroomChef");
    }

        public IEnumerator NoTalkAfterDialogue(float time)
    {
        yield return new WaitForSeconds(time);
        Dinter.TempTurnOffTalkAbility();
        Dinter.GUMDROP.anim.SetBool("canInteract", false);

        if (isBudShroomChef == false)
        {
            StartCoroutine(Dinter.TurnTalkingAbilityBackOn(5f));
        }
        if (isBudShroomChef == true)
        {
            this.gameObject.tag = "Null";
            anim.SetTrigger("chefgone");
            panim.SetTrigger("Unsmoke");
            CameraOfInterest = GameObject.Find("ChefCam");
            Destroy(CameraOfInterest);
        }
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
        anim.SetBool("isPissingPants", false);
        anim.SetBool("isDazed", false);
        anim.SetBool("isWorried", false);
        MG.isGolfing = true;
        MG.HitTrigger.enabled = true;
        MG.GolfHole.SetActive(true);
        Destroy(CameraOfInterest);
    }
}