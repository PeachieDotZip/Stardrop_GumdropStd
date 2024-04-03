using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoutSnipper : MonoBehaviour
{
    public GameObject target;
    private ScoutSnipper selfscript;
    public TRADCONTROL GUMDROP;
    private Animator anim;
    public ParticleSystem deadParticle;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.gameObject;
        GUMDROP = target.GetComponent<TRADCONTROL>();
        anim = GetComponentInParent<Animator>();
        selfscript = GetComponent<ScoutSnipper>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(target.transform.position);
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target && (GUMDROP._isDashing == true))
        {
            anim.SetBool("dead", true);
            selfscript.enabled = false;
            deadParticle.Play();
        }
    }
    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject == target && (GUMDROP._isDashing == true))
        {
            anim.SetBool("dead", true);
            selfscript.enabled = false;
            deadParticle.Play();
        }
    }
}
