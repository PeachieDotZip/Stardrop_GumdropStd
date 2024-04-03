using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public GameObject Gumdrop;
    public Animator UIanim;
    public Transform doortelePoint;
    private BoxCollider bcollider;

    // Start is called before the first frame update
    void Start()
    {
        bcollider = GetComponent<BoxCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        if (UIanim.GetBool("Enter") == true)
        {
            bcollider.enabled = (false);
        }
        if (UIanim.GetBool("Enter") == false)
        {
            bcollider.enabled = (true);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Gumdrop)
        {
            UIanim.SetBool("Enter", true);
        }
    }
}
