using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleLookAt : MonoBehaviour
{
    public Transform target;
    public Animator anim;

    private void Start()
    {
        anim = GetComponent<Animator>();
        transform.LookAt(target, Vector3.up);
    }
    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(target);

        // Same as above, but setting the worldUp parameter to Vector3.up
        transform.LookAt(target, Vector3.up);
    }


    void OnTriggerEnter(Collider other)
    {
        if (gameObject.CompareTag("Refresh"))
        {

            if (other.gameObject.CompareTag("Player"))
            {
                anim.SetTrigger("Shrink");
                //this part of the script is just for setting the trigger for the "Shrink" animation
                //for the bubble refresher
            }
        }
    }
}