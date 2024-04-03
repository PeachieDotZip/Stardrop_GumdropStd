using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSnipper : MonoBehaviour
{
    public GameObject target;
    public TRADCONTROL GUMDROP;
    public Animator anim;
    public ParticleSystem deadParticle;
    public ParticleSystem chargeParticle;
    public int HP;
    public bool InRange = false;
    public float Sight = 25f;
    Transform GdpTransform;
    public bool isShooter;
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        target = PlayerManager.instance.player.gameObject;
        GUMDROP = target.GetComponent<TRADCONTROL>();
        GdpTransform = PlayerManager.instance.player.transform;
        anim.SetBool("Shooter", isShooter);
        if (isShooter == false)
        {
            bullet = null;
            chargeParticle = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(GdpTransform.position, transform.position);

        if (distance <= Sight)
        {
            InRange = true;
        }
        if (distance >= Sight)
        {
            InRange = false;
        }

        if (InRange == true)
        {
            Vector3 direction = (GdpTransform.position - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
        if (InRange == false)
        {
            transform.LookAt(null);
        }
        anim.SetBool("InRange", InRange);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == target && (GUMDROP._isDashing == true))
        {

            HP -= 1;

            if (HP == 0)
            {
                anim.SetBool("STDead", true);
                deadParticle.Play();
            }
            if (HP >= 1)
            {
                anim.SetTrigger("STHurt");
            }
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.gray;
        Gizmos.DrawWireSphere(transform.position, Sight);
    }
}
