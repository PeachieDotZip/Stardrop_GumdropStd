using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fryingpanscript : MonoBehaviour
{
    public int pearCount;
    public GameObject CurrentPear;
    private Animator panim;
    public MushroomDialogue MD;

    // Start is called before the first frame update
    void Start()
    {
        panim = GetComponent<Animator>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pear"))
        {
            panim.SetTrigger("PearDrop");
            pearCount += 1;
            CurrentPear = other.gameObject;
            CurrentPear.GetComponent<GrabbableObjectScript>().enabled = false;
            CurrentPear.GetComponent<SphereCollider>().enabled = false;
            CurrentPear.tag = "Null";
            Destroy(CurrentPear);
            //bruh.
        }
    }

    private void DestroyPears()
        {

        }
}
