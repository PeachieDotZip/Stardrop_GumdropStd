using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistractedKiwi : MonoBehaviour
{
    public GameObject ThingOfInterest;
    private KiwiNPC KNPC;
    private DialogueInteraction Dinter;
    // Start is called before the first frame update
    void Start()
    {
        KNPC = GetComponent<KiwiNPC>();
        Dinter = GetComponent<DialogueInteraction>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ThingOfInterest == null)
        {
            StartCoroutine(DKET(1f));
        }
    }
    private IEnumerator DKET(float time)
    {
        Dinter.TriggerEvent();
        yield return new WaitForSeconds(time);
        Destroy(this);
    }
}
