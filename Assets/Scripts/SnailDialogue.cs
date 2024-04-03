using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailDialogue : MonoBehaviour
{
    private DialogueInteraction Dinter;
    private Animator anim;
    private Collider InteractTrigger;
    public DialogueManager Dmanager;
    public GameObject CameraOfInterest;
    public GameObject giveObject;
    public bool Snail1;
    public bool Snail2;
    private ParticleSystem Joy;
    public Transform lookat;
    public bool facing = false;
    public GrabbableObjectScript ObjScriptS;
    public GameObject SnailObject;
    public MeshRenderer[] meshr;
    public MeshRenderer[] snailparts;

    // Start is called before the first frame update
    void Start()
    {
        Dinter = GetComponent<DialogueInteraction>();
        anim = GetComponent<Animator>();
        InteractTrigger = GetComponent<SphereCollider>();
        Joy = GetComponent<ParticleSystem>();
        CameraOfInterest.SetActive(false);
        snailparts = GetComponentsInChildren<MeshRenderer>();
        for (int i = 0; i < meshr.Length; i++)
        {
            meshr[i].GetComponentsInChildren<MeshRenderer>();
        }
        if (Snail2 == true)
        {
            Dinter.notalk = true;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Dinter.notalk == true)
        {
            InteractTrigger.enabled = false;
        }
        else
        {
            InteractTrigger.enabled = true;
        }

        if (Dinter.midconvo == true)
        {
            anim.SetBool("isTalking", true);
            Dinter.camrot._CameraDistance = 9.5f;
            Dinter.camrot._LocalRotation.y = -25f;
            facing = true;
            StartCoroutine(StopFacinglookat(2f));

            if (Input.GetButtonDown("Interact") & Dinter.CanPass == true & Dmanager.CPanim.GetCurrentAnimatorStateInfo(0).IsName("CanPassICON_ON"))
            {
                TalkingAnims1();
                TalkingAnims2();
            }
        }
        if (Dinter.midconvo == false)
        {
            anim.SetBool("isTalking", false);
            StartCoroutine(StopFacinglookat(.1f));
        }
        if (facing == true)
        {
            Facelookat();
            lookat = PlayerManager.instance.player.transform;
        }
        if (Dinter.AlreadyTalked == true && Dinter.EventTrigger == false)
        {
            SnailObject.SetActive(true);
            //^fix this so its not calling every frame, even after its already active
        }
        if (Dinter.midconvo == true && Dinter.AlreadyTalked == true)
        {
            InteractTrigger.enabled = false;
        }

    }
    public void JoyParticlePlay()
    {
        Joy.Play();
    }
    public void JoyParticleStop()
    {
        Joy.Stop();
    }
    public void TurnOnMeshes()
    {
        foreach (MeshRenderer go in snailparts)
        {
            snailparts[0].enabled = true;
            snailparts[1].enabled = true;
            snailparts[2].enabled = true;
            snailparts[3].enabled = true;
            snailparts[4].enabled = true;
            snailparts[5].enabled = true;
            InteractTrigger.enabled = true;
            Dinter.notalk = false;
            Dinter.lookRadius = 7.1f;
            Debug.Log("please");
        }
    }

    public void TalkingAnims1()
    {
        if (Snail1 == true)
        {
            if (Dmanager.sentences.Count == 4)
            {
                CameraOfInterest.SetActive(true);
            }
            if (Dmanager.sentences.Count == 3)
            {
                CameraOfInterest.SetActive(false);
            }

            if (Dmanager.sentences2.Count == 0 & (Dinter.midconvo == true) & Dinter.AlreadyTalked == true)
            {
                StartCoroutine(ObjScriptS.Pickup(.20f));
                StartCoroutine(TurnOffNPC(.10f));
            }
        }
    }
    public void TalkingAnims2()
    {
        if (Snail2 == true & Dinter.AlreadyTalked == false & Dinter.EventTrigger == false)
        {
            if (Dmanager.Snumber == 5)
            {
                anim.SetBool("Overjoyed", true);
            }
            if (Dmanager.Snumber == 7)
            {
                anim.SetBool("Overjoyed", false);
            }
            if (Dmanager.Snumber == 9)
            {
                anim.SetBool("Overjoyed", true);
                giveObject.SetActive(true);
            }
            if (Dmanager.Snumber == 11)
            {
                anim.SetBool("Overjoyed", false);
            }
            if (Dmanager.Snumber > 12)
            {
                StartCoroutine(NoTalkAfterDialogue(.0021f));
            }
        }
    }
        public void TurnIntoObject() 
    {
        if (Dinter.AlreadyTalked == true)
        {
            meshr[0].enabled = false;
            meshr[1].enabled = false;
            meshr[2].enabled = false;
            meshr[3].enabled = false;
            meshr[4].enabled = false;
            meshr[5].enabled = false;
            meshr[6].enabled = true;
            meshr[7].enabled = true;
            meshr[8].enabled = true;
            meshr[9].enabled = true;
            meshr[10].enabled = true;
            meshr[11].enabled = true;
            meshr[12].enabled = true;
            ObjScriptS.NearObject = true;
            ObjScriptS.SelectedObject = true;
            ObjScriptS.Gumdrop.isHolding = true;
            ObjScriptS.enabled = true;
        }
    }
    void Facelookat()
    {
        Vector3 direction = (lookat.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }
    public IEnumerator StopFacinglookat(float time)
    {
        yield return new WaitForSeconds(time);
        facing = false;
    }
    public IEnumerator TurnOffNPC(float time)
    {
        yield return new WaitForSeconds(time);
        this.gameObject.SetActive(false);
        ObjScriptS.Gumdrop.anim.SetBool("canInteract", false);
        ObjScriptS.rb.isKinematic = false;
    }
    private IEnumerator NoTalkAfterDialogue(float time)
    {
        yield return new WaitForSeconds(time);
        Dinter.TempTurnOffTalkAbility();
        StartCoroutine(Dinter.TurnTalkingAbilityBackOn(3.521f));
    }
}
