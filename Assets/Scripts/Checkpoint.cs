using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    private TRADCONTROL GUMDROP;
    public bool isChecked;
    public float checkpointRadius;
    private Animator anim;
    public Transform checkpointRespawnPoint;
    private Transform target;
    public DialogueLookAt dla;

    // Start is called before the first frame update
    void Start()
    {
        GUMDROP = FindObjectOfType<TRADCONTROL>();
        anim = GetComponent<Animator>();
        target = PlayerManager.instance.player.transform;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetBool("InsideCheckpoint", true);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {

            anim.SetBool("InsideCheckpoint", false);
        }
    }

    private void Update()
    {
        float distance = Vector3.Distance(target.position, transform.position);

        // If inside the radius
        if (distance < checkpointRadius && gameObject.CompareTag("Checkpoint"))
        {
            dla.target = gameObject;
        }
        // If inside the radius and presses "Interact"
        if (Input.GetButtonDown("Interact") && (distance < checkpointRadius))
        {
            GUMDROP.respawnPoint = checkpointRespawnPoint;
            anim.SetTrigger("Checked");
        }
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, checkpointRadius);
    }
}
