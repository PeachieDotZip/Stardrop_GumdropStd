using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueLookAt : MonoBehaviour
{
    public GameObject target;
    private TRADCONTROL GUMDROP;

    void Start()
    {
        GUMDROP = GetComponent<TRADCONTROL>();
    }
    void Update()
    {
        if (GUMDROP.anim.GetCurrentAnimatorStateInfo(0).IsName("Gumdrop Idle") &&
            GUMDROP.anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f && target != null)
        {
            Vector3 direction = (target.transform.localPosition - transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }
}