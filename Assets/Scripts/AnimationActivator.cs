using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationActivator : MonoBehaviour
{
    public GameObject Activatee;
    private Animator Aanim;

    // Start is called before the first frame update
    void Start()
    {
        Aanim = Activatee.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ActivateAnimation()
    {
        Aanim.SetTrigger("Activate_Animation");
    }
    public void Fuckholdonigottafixthisbimpisglitch()
    {
        Aanim.SetBool("isGone", false);
    }
}
