using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BimpisExitPortal : MonoBehaviour
{
    private Animator anim;
    private UIScript UIS;
    public Animator UIanim;
    private Collider enterTrigger;
    public GameObject PortalTP_afterDR;
    private TRADCONTROL GUMDROP;

    //this simple script just allows the exit portal to do what it needs to do
    void OnEnable()
    {
        anim = GetComponent<Animator>();
        UIS = FindObjectOfType<UIScript>();
        enterTrigger = GetComponent<Collider>();
        GUMDROP = FindObjectOfType<TRADCONTROL>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Enter");
            StartCoroutine(PortalEnter_Bimpis());
        }
    }

    private IEnumerator PortalEnter_Bimpis()
    {
        enterTrigger.enabled = false;
        GUMDROP.gameObject.tag = "PlayerTalking";
        PortalTP_afterDR.SetActive(true);
        UIanim.SetTrigger("EnterPortal_Bimpis");
        StartCoroutine(GUMDROP.EnableGD(11f));
        yield return new WaitForSeconds(11f);
        UIS.TP = GameObject.Find("respawn point 1");
        Debug.Log("portalmoment_bimpis");
        yield return new WaitForSeconds(5f);
        Debug.Log("Removed Deitus Realm!");
        Destroy(PortalTP_afterDR);
        Destroy(gameObject);
    }
}
