using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutSceneTrigger2 : MonoBehaviour
{
    public Animator UIanim;
    public Animator Banim;
    public TRADCONTROL GUMDROP;
    public GameObject CutSceneCam;
    public GameObject MainCam;
    public CameraRotation camrot;

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.CompareTag("Player"))
        {
            this.gameObject.GetComponent<BoxCollider>().enabled = false;
            UIanim.SetBool("CutSceneStart", true);
            StartCoroutine(BungoStart());
            StartCoroutine(BungoEnd());
            GUMDROP.tag = ("PlayerTalking");

        }
    }
    IEnumerator BungoStart()
    {
        yield return new WaitForSeconds(2f);
        camrot.CameraDisabled = true;
        Banim.SetBool("isGone", false);
        UIanim.SetBool("CutSceneStart", false);
        CutSceneCam.SetActive(true);
    }
    IEnumerator BungoEnd()
    {
        yield return new WaitForSeconds(12f);
        UIanim.SetBool("CutSceneStart", true);
        yield return new WaitForSeconds(1f);
        Banim.SetTrigger("ReturnToNormal");
        yield return new WaitForSeconds(1f);
        camrot.CameraDisabled = false;
        Banim.SetBool("isGone", false);
        Destroy(CutSceneCam);
        UIanim.SetBool("CutSceneStart", false);
        GUMDROP.tag = ("Player");
        GUMDROP.AllowedToLook = true;
    }
}
