using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableObjectScript : MonoBehaviour
{
    Transform GUM;
    public float grabRadius = 6f;
    public Rigidbody rb;
    public Transform Holdpoint;
    public TRADCONTROL Gumdrop;
    private UIScript ui;
    public Collider m1trigger;
    public Collider othercollider;
    public DialogueLookAt dla;
    public bool NearObject;
    public bool SelectedObject;
    public int currentItemID;

    // Start is called before the first frame update
    void Start()
    {
        GUM = PlayerManager.instance.player.transform;
        rb = GetComponent<Rigidbody>();
        Gumdrop = FindObjectOfType<TRADCONTROL>();
        ui = FindObjectOfType<UIScript>();
        m1trigger = GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector3.Distance(GUM.position, transform.position);

        if (NearObject == true)
        {
            dla.target = gameObject;
        }

        if (SelectedObject == true)
        {

            if (Input.GetButtonDown("Interact") && (NearObject == true))
            {
                if (Gumdrop.isHolding == false)
                {
                    Gumdrop.isHolding = true;
                    Gumdrop.anim.SetBool("canInteract", false);
                    StartCoroutine(Pickup(.20f));
                }
            }

            if (Gumdrop.isHolding == true)
            {
                rb.useGravity = false;
                rb.freezeRotation = true;
                transform.position = Holdpoint.position;
                transform.parent = GameObject.Find("GUMDROP").transform;
                grabRadius = -21f;
                m1trigger.enabled = false;
                othercollider.enabled = false;
                Gumdrop._doubleJumpMultiplier = 0.525f;
                Gumdrop._leapSpeed = 2.1f;
                Gumdrop._springSpeed = 1.75f;
                GameManager.itemID = currentItemID;
            }
        }

        if (Gumdrop.isHolding == true && NearObject == false)
        {
            grabRadius = -21f;
            m1trigger.enabled = false;
        }
        if (Input.GetKeyDown(KeyCode.Q) && NearObject == false)
        {
            if (Gumdrop.canalsoInteract == false)
            {
                StartCoroutine(Drop());
                Gumdrop.isHolding = false;
                transform.parent = null;
                rb.useGravity = true;
                rb.freezeRotation = false;
                grabRadius = 6f;
                m1trigger.enabled = true;
                othercollider.enabled = true;
                Gumdrop._doubleJumpMultiplier = 0.75f;
                Gumdrop._leapSpeed = 3f;
                Gumdrop._springSpeed = 2.5f;
            }
        }

        if (distance <= grabRadius)
        {
            NearObject = true;
        }

        if (distance >= grabRadius)
        {
            NearObject = false;
        }
    }

    public IEnumerator Drop()
    {
        GameManager.itemID = 0;
        rb.velocity = Vector3.zero;
        Gumdrop.isHolding = false;
        transform.parent = null;
        rb.useGravity = true;
        rb.freezeRotation = false;
        grabRadius = 6f;
        m1trigger.enabled = true;
        othercollider.enabled = true;
        Gumdrop._doubleJumpMultiplier = 0.75f;
        Gumdrop._leapSpeed = 3f;
        Gumdrop._springSpeed = 2.5f;
        Gumdrop.HoldPoint.DetachChildren();
        yield return new WaitForSeconds(0.21f);
        Gumdrop.anim.SetBool("canInteract", false);
        yield return new WaitForSeconds(0.3f);
        Gumdrop.anim.SetBool("canInteract", false);
    }
    public IEnumerator Pickup(float time)
    {
        yield return new WaitForSeconds(time);
        Gumdrop.anim.SetBool("canInteract", false);
        GameManager.itemID = currentItemID;
    }

    public void ForceDrop()
    {
        // Runs when player dies while holding an item
        StartCoroutine(Drop());
        Gumdrop.isHolding = false;
        transform.parent = null;
        rb.useGravity = true;
        rb.freezeRotation = false;
        grabRadius = 6f;
        m1trigger.enabled = true;
        othercollider.enabled = true;
        Gumdrop._doubleJumpMultiplier = 0.75f;
        Gumdrop._leapSpeed = 3f;
        Gumdrop._springSpeed = 2.5f;
        NearObject = false;
        SelectedObject = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, grabRadius);
    }
}
