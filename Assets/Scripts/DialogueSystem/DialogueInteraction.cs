using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueInteraction : MonoBehaviour
{
    private DialogueTrigger Dtrigger;
    public DialogueManager Dmanager;
    Transform target;
    public float lookRadius = 10f;
    private float SavedlookRadius;
    public TRADCONTROL GUMDROP;
    public CameraRotation camrot;
    public DialogueLookAt dla;
    public GameObject StarShard;
    public bool midconvo;
    public bool AlreadyTalked;
    public bool EventTrigger;
    public bool EditDialogue;
    public bool TriggerEventPostDialogue;
    public bool isSnail;
    SnailDialogue SD;
    public bool CanPass;
    public bool notalk;
    [SerializeField]
    public bool canSkipDialogue;
    public bool GiveSS;
    public bool isKrodge;

    public void Start()
    {
        Dmanager = FindObjectOfType<DialogueManager>();
        Dtrigger = GetComponent<DialogueTrigger>();
        target = PlayerManager.instance.player.transform;
        SD = GetComponent<SnailDialogue>();
        CanPass = true;
        SavedlookRadius = lookRadius;
    }

    // Update is called once per frame
    public void Update()
    {
        //Fixes bug that lets you spam click through dialogue
        //at the beginning of a conversation
        //vvv

        if (Dmanager.Snumber == 1)
        {
            lookRadius = -11;
        }
        //vvv fixes a glitch that causes LOL
        if (Dmanager.Snumber == 0 && notalk == false)
        {
            lookRadius = SavedlookRadius;
        }

        // VVV Sets basic radius shit for the NPC to use. (How far is the "talk-range" or lookRadius, checks if the player is inside it, etc.)

        float distance = Vector3.Distance(target.position, transform.position);

        if (notalk == true)
        {
            lookRadius = -21f;
        }

        // If inside the radius
        if (distance < lookRadius && gameObject.CompareTag("NPC"))
        {
            dla.target = gameObject;

            //VVV Krodge-specific code.
            if (isKrodge == true)
            {
                if (GameManager.itemID == 3)
                {
                    TriggerEvent();
                }
            }
        }

        if (Input.GetButtonDown("Interact") && (distance < lookRadius))
        {

            if (GUMDROP._controller.isGrounded && midconvo == false)
            {
                Dtrigger.TriggerDialogue();
                GUMDROP.AllowedToLook = false;
                camrot.SaveRotationFloats();
                StartCoroutine(ChangeCamFloats(.1f));
                StartCoroutine(Setmidconvo(gameObject, .5f));
                Debug.Log("Began conversing with " + Dtrigger.dialogue.name);

                if (isSnail == true)
                {
                    SD.TurnIntoObject();
                }
                //VVV Krodge-specific code.
                if (isKrodge == true)
                {
                    //^^^ When the golden seed is brought to Krodge, event dialogue is triggered.
                    if (Dmanager.Snumber == 1 | Dmanager.Snumber == 2 | Dmanager.Snumber == 3)
                    {
                        KrodgeTheSequel krodge = GetComponent<KrodgeTheSequel>();
                        krodge.CustomCamera.SetActive(true);
                        krodge.talkPoint = GameObject.Find("SecondCSTelePoint").transform;
                        krodge.PlaceGumdrop();
                        //^^^ Sets up custom camera and gumdrop's position for proceeding dialogue.
                    }
                }
            }
            if (midconvo == true & CanPass == true & Dmanager.CPanim.GetCurrentAnimatorStateInfo(0).IsName("CanPassICON_ON"))
            {
                Dmanager.DisplayNextSentence();

                if (GUMDROP._controller.isGrounded)
                {
                    GUMDROP._jumpSpeed = 0;
                }
            }
        }
        if (midconvo == true)
        {
            lookRadius = 21f;
            //^^^ this is to keep the glitch where an NPC's animation or something causes the player
            // to drift outside of the lookRadius and be unable to progress through dialogue
        }
    }

    public void ApplySavedlookRadius()
    {
        lookRadius = SavedlookRadius;
    }
    public void TempTurnOffTalkAbility()
    {
        CanPass = false;
        notalk = true;
        GUMDROP.anim.SetBool("canInteract", false);
        Debug.Log("notalkie");
        // do StartCoroutine(TurnTalkingAbilityBackOn(___f)); separately
        //as well as turning off the NPC's specific talk trigger
    }
    public void TriggerEvent()
    {
        EventTrigger = true;
        AlreadyTalked = false;
    }
    public void GiveStarShard()
    {
        camrot.SaveCameraPosition();
        StarShard.GetComponentInChildren<StarShard>().FloatTowardsRestPoint = true;
        //StarShard.GetComponent<StarShard>().StartCoroutine(RestAtPoint(21f));
    }

    /// <summary>
    /// Skips the given conversation.
    /// Forces the game to fade to black, end the dialogue, tell the NPC-specific script to complete all needed tasks normally done DURING dialogue, then fade back in.
    /// </summary>
    /// <returns></returns>
    public IEnumerator SkipAllDialogue()
    {
        TempTurnOffTalkAbility();
        Animator animUI = GameObject.Find("Canvas").GetComponent<Animator>();
        animUI.SetBool("CutSceneStart", true);
        yield return new WaitForSeconds(1f);
        Dmanager.EndDialogue();
        //This is when the NPC-specific script completes its tasks.
        yield return new WaitForSeconds(1.5f);
        if (gameObject.activeInHierarchy)
        {
            StartCoroutine(TurnTalkingAbilityBackOn(.5f));
        }
        animUI.SetBool("CutSceneStart", false);

    }
    public IEnumerator TurnTalkingAbilityBackOn(float time)
    {
        yield return new WaitForSeconds(time);
        notalk = false;
        ApplySavedlookRadius();
        StartCoroutine(ReCanPass(.21f));
    }
    private IEnumerator Setmidconvo(GameObject target, float time)
    {

        yield return new WaitForSeconds(time);
        midconvo = true;
        Dmanager.DisplayNextSentence();
        GUMDROP.AllowedToLook = false;
        if (EventTrigger == true)
        {
            //Dmanager.Snumber = 2;
            Debug.Log("when there's no big gay stinky balls");
            //^^^ this is for fixing the weird desync glitch that happens with talking anims when eventtrigger = true
            //^^^ shut up bro idk whats wrong ok nvm it has nothing to do with eventtrigger and everything to do with the fact that the go is disabled, somehow it fucks everything up, use ladder lines of code VVV
        }
        //okay figured it out (at least i think) long story short- DO NOT DISABLE AN NPC UNDER ANY FUCKING CIRCUMSTANCES, JUST DISABLE ALL OF IT MESH RENDERERS OR SOMETHING IDK
        //^^^this is also wrong dude shut the fuck up, krodge gets disabled during the tutorial so this is obviously wrong
        //^^^^^ Imma fucking kms idk
    }
    private IEnumerator ChangeCamFloats(float time)
    {
        yield return new WaitForSeconds(time);
        camrot._CameraDistance = 7f;
        camrot._LocalRotation.y = -10f;
    }
    private IEnumerator ReCanPass(float time)
    {
        yield return new WaitForSeconds(time);
        CanPass = true;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
