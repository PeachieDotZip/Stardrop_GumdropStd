using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTrigger : MonoBehaviour
{
    public Animator UIanim;
    public TRADCONTROL GUMDROP;
    public GameObject CutSceneCam;
    public GameObject MainCam;
    public Animator Kanim;
    public GameObject Krodge;
    public CameraRotation camrot;
    public Krodge KrodgeScript;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            UIanim.SetBool("CutSceneStart", true);
            StartCoroutine(KrodgeEnable(1f));
            StartCoroutine(FadeIn(2f));
            StartCoroutine(Kend1(11.5f));
            StartCoroutine(Kend2(13f));
            GUMDROP.tag = ("PlayerTalking");
        }
    }
    IEnumerator KrodgeEnable(float time)
    {
        yield return new WaitForSeconds(time);
        Krodge.SetActive(true);
        camrot.CameraDisabled = true;
    }

    IEnumerator FadeIn(float time)
    {
        yield return new WaitForSeconds(time);
        MainCam.SetActive(false);
        UIanim.SetBool("CutSceneStart", false);
        Kanim.SetBool("Enter", true);
        CutSceneCam.SetActive(true);
    }
    IEnumerator Kend1(float time)
    {
        yield return new WaitForSeconds(time);
        UIanim.SetBool("CutSceneStart", true);
    }
    IEnumerator Kend2(float time)
    {
        yield return new WaitForSeconds(time);
        UIanim.SetBool("CutSceneStart", false);
        CutSceneCam.SetActive(false);
        GUMDROP.tag = ("Player");
        MainCam.SetActive(true);
        GUMDROP.AllowedToLook = true;
        camrot.CameraDisabled = false;
    }
}
