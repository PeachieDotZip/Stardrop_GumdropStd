using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeanstalkScript : MonoBehaviour
{
    public GameObject BrownSeed;
    public Animator UIanim;
    public Transform doortelePoint;
    public Collider BeanTrigger;
    private Animator anim;
    private ParticleSystem DirtParticle;
    public TRADCONTROL GUMDROP;
    public GameObject RealBeanstalk;
    public DialogueInteraction Dinter;
    public int BeanstalkID;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        DirtParticle = GetComponentInChildren<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIanim.GetBool("CutSceneStart") == true)
        {
            BeanTrigger.enabled = (false);
            GUMDROP.anim.SetBool("canInteract", false);
        }
        if (UIanim.GetBool("CutSceneStart") == false)
        {
            BeanTrigger.enabled = (true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == BrownSeed && BeanstalkID == 0)
        {
            anim.SetTrigger("Grow");
            Destroy(BrownSeed);
            DirtParticle.Play();
            StartCoroutine(CutsceneStart(2f));
        }
        if (other.gameObject == BrownSeed && BeanstalkID == 1)
        {
            anim.SetTrigger("Grow_1");
            Destroy(BrownSeed);
        }
    }

    public void DirtParticlePlay()
    {
        DirtParticle.Play();
    }
    public void DirtParticleStop()
    {
        DirtParticle.Stop();
    }
    public void CutsceneStartEnd()
    {
        UIanim.SetBool("CutSceneStart", false);
    }

    public void RealBeanstalkEnable()
    {
        RealBeanstalk.SetActive(true);
        //StartCoroutine(FlashScreen(0.21f));
    }

    public void BeanstalkGrowFlash()
    {
        UIanim.SetTrigger("Flash");
    }
    public void SoilBudshroomEventTrigger()
    {
        Dinter.TriggerEvent();
    }

    public IEnumerator CutsceneStart(float time)
    {
        yield return new WaitForSeconds(time);
        UIanim.SetBool("CutSceneStart", true);
    }
    public IEnumerator FlashScreen(float time)
    {
        yield return new WaitForSeconds(time);
        UIanim.ResetTrigger("Flash");
    }
}