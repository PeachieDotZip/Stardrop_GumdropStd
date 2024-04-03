using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnailTrigger : MonoBehaviour
{
    public Animator UIanim;
    public UIScript UIS;
    private TRADCONTROL GUMDROP;
    public GameObject GumdropTpPoint_Snail;
    public GameObject SnailNPC;
    public GameObject SnailOBJ;
    public GameObject SnailNPC_Old;
    public SnailDialogue SD;


    // Start is called before the first frame update
    void Start()
    {
        GUMDROP = FindObjectOfType<TRADCONTROL>();

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Snail"))
        {
            StartCoroutine(Everythinglol(1f));
            StartCoroutine(Cleanup(1.50f));
            UIS.TP = GumdropTpPoint_Snail;
            UIanim.SetBool("CutSceneStart", true);
        }
    }
    public IEnumerator Everythinglol(float time)
    {
        yield return new WaitForSeconds(time);
        UIS.TeleportGMDRP();
        UIS.TP = GumdropTpPoint_Snail;
        //SnailNPC.SetActive(true);
        SD.TurnOnMeshes();
        SnailOBJ.SetActive(false);
        //SnailNPC.GetComponent<DialogueInteraction>().justEnabled = true;
    }
    public IEnumerator Cleanup(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(SnailOBJ);
        Destroy(GumdropTpPoint_Snail);
        Destroy(SnailNPC_Old);
        Destroy(gameObject);
        UIanim.SetBool("CutSceneStart", false);
    }
}
