using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThornAppear : MonoBehaviour
{
    private float appearRadius = 25f;
    private Animator anim;
    Transform GUM;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        GUM = PlayerManager.instance.player.transform;
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(GUM.position, transform.position);

        if (distance <= appearRadius)
        {
            anim.SetBool("Appear", true);
        }
        if (distance >= appearRadius)
        {
            anim.SetBool("Appear", false);
        }
    }
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, appearRadius);
    }
}
