using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// BUG FIX!!! : DRAG IN NEW KRODGE FROM PREFAB TO FIX SCRIPT NOT RESPONDING
/// </summary>

public class KrodgeTheSequel : MonoBehaviour
{
    private DialogueInteraction Dinter;
    private Animator anim;
    public DialogueManager Dmanager;
    public StarShard StarShardScript;
    public bool StarShardActive = false;
    public GameObject CustomCamera;
    public Transform talkPoint;
    public GameObject StarShard;
    private UIScript UIS;
    public Mouse1IconLookAt mouse1icon;
    private bool isHoldingGS = false;

    // Start is called before the first frame update
    void Awake()
    {
        Dinter = GetComponent<DialogueInteraction>();
        anim = GetComponent<Animator>();
        anim.SetInteger("randTalkInt", 2);
        UIS = GameObject.Find("Canvas").GetComponent<UIScript>();
        talkPoint = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Dinter.midconvo == true)
        {
            anim.SetBool("isTalking", true);
            Dinter.GUMDROP.tag = "PlayerTalking";

            if (Input.GetButtonDown("Interact") & Dinter.CanPass == true & Dmanager.CPanim.GetCurrentAnimatorStateInfo(0).IsName("CanPassICON_ON"))
            {
                if (isHoldingGS == false)
                {
                    Debug.Log("CLICK! " + Dmanager.Snumber);
                    int krodgeTalkNum = Random.Range(1, 3);//exclusive never prints the last only goes 1 to 2
                    Debug.Log("Krodge is playing talking anim number: " + krodgeTalkNum);
                    anim.SetInteger("randTalkInt", krodgeTalkNum);
                    WaitingOff();
                }
                else
                {
                    PlaceGumdrop();
                }
                KrodgeDialogueAnimations();
                Dinter.GUMDROP.tag = "PlayerTalking";

                if (CustomCamera.activeInHierarchy == true)
                {
                    //Makes Mouse1 icon face the new dialogue-specific camera instead of the main camera
                    mouse1icon.isFacingMainCam = false;
                    mouse1icon.Camera1 = CustomCamera.transform;
                }
            }
            //VVV Holding GS Cutscene
            if (Dinter.EventTrigger == true)
            {
                if (Dmanager.Snumber >= 6 && Dmanager.Snumber <= 11)
                {
                    Dinter.CanPass = false;
                    Dmanager.CPanim.SetBool("ON", false);
                }
                if (Dmanager.Snumber == 14)
                {
                    Dinter.CanPass = false;
                    Dmanager.CPanim.SetBool("ON", false);
                }
                if (Dmanager.Snumber >= 17)
                {
                    Dinter.CanPass = false;
                    Dmanager.CPanim.SetBool("ON", false);
                }
            }
        }

        if (Dinter.midconvo == false)
        {
            //CustomCamera.SetActive(false);
            anim.SetBool("isTalking", false);
        }

    }

    /// <summary>
    /// Teleports gumdrop to the Krodge's desired telepoint. Called by Dinter, uses UIS's function to teleport gumdrop.
    /// </summary>
    public void PlaceGumdrop()
    {
        if (talkPoint != null)
        {
            GameObject gumdrop = GameObject.Find("GUMDROP");
            gumdrop.transform.position = talkPoint.position;
            //UIS.TeleportGMDRP_ToPoint(talkPoint);
        }
    }

    public void KrodgeDialogueAnimations()
    {
        if (Dinter.AlreadyTalked == true)
        {
            if (Dmanager.Snumber == 4 || Dmanager.Snumber == 5 || Dmanager.Snumber == 6)
            {
                anim.SetBool("isConfused", true);
                anim.SetBool("isWaiting", true);
            }
            if (Dmanager.Snumber == 7)
            {
                anim.SetBool("isConfused", false);
                anim.SetBool("isWaiting", false);
                anim.SetBool("isTalking", false);
            }
        }
        if (Dinter.EventTrigger == true)
        {
            if (Dmanager.Snumber == 5)
            {
                anim.SetTrigger("receiveGS");
                Dinter.GUMDROP.Obj.ForceDrop();
                Destroy(GameObject.Find("GoldenSeed"));
            }
            if (Dmanager.Snumber == 13)
            {
                anim.SetTrigger("giveSS_GS");
            }
            if (Dmanager.Snumber == 16)
            {
                anim.SetTrigger("GS_end");
            }
        }
    }

    public void WaitingOn()
    {
        anim.SetBool("isWaiting", true);
    }
    public void WaitingOff()
    {
        if (anim != null)
        {
            anim.SetBool("isWaiting", false);
        }
    }
    public void EntranceOver()
    {
        anim.SetBool("IdlePuddleRipple", true);
        anim.SetBool("Enter", false);
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
    public void GiveStarShardCompletely()
    {
        Debug.Log("i sharted");
        StarShard.SetActive(true);
    }
    public void ReturnToIdleValues()
    {
        CustomCamera.SetActive(false);
        mouse1icon.isFacingMainCam = true;
        mouse1icon.Camera1 = null;
    }
    public void DSkip()
    {
        Dmanager.SkipDialogue();
        Debug.Log("Next Line!");
    }
    public void DSkip_EndHoldingCutscene()
    {
        Dmanager.SkipDialogue();
        isHoldingGS = true;
    }
    public void DSkip_EndExitCutscene()
    {
        Dmanager.SkipDialogue();
        Dinter.notalk = true;
    }
    public void DestroySelf()
    {
        FindObjectOfType<CameraRotation>()._LocalRotation = new Vector3(-11f, 0f, 0f);
        CustomCamera.SetActive(false);
    }
    public void DestroyKrodge()
    {
        GameObject.Find("MushroomActivator").GetComponent<Animator>().SetTrigger("Grow");
        //gameObject.SetActive(false);
        Destroy(gameObject);
    }
    private IEnumerator DestroyTelePoint(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(UIS.TP);
    }
}