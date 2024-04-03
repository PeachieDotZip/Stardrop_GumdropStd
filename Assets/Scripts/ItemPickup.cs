using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    public Transform equipPosition;
    public float distance = 4f;
    public GameObject currentItem;
    GameObject it;
    public Transform pickupPrompt;

    public LayerMask itemLayer;

    [SerializeField] private Transform raycastSpawn;

    public bool canPickup;

    private void Update()
    {
        if (canPickup == true)
        {
            pickupPrompt.gameObject.SetActive(true);
        }
        else
        {
            pickupPrompt.gameObject.SetActive(false);
        }

        CheckForItems();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (currentItem != null)
            {
                Drop();

                Pickup();
            }
            else if (currentItem == null && canPickup == true)
            {
                Pickup();
            }
        }

        if (currentItem != null)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Drop();
            }
        }
    }

    private void CheckForItems()
    {
        RaycastHit hit = new RaycastHit();


        if (Physics.Raycast(raycastSpawn.position, raycastSpawn.forward, out hit, distance, itemLayer))
        {
            canPickup = true;
            it = hit.transform.gameObject;
        }
        else
            canPickup = false;
    }

    private void Pickup()
    {
        currentItem = it;
        currentItem.transform.position = equipPosition.position;
        currentItem.transform.parent = equipPosition;
        currentItem.transform.localEulerAngles = new Vector3(0f, 180f, 0f);
        currentItem.GetComponent<Rigidbody>().isKinematic = true;
    }

    private void Drop()
    {
        currentItem.transform.parent = null;
        currentItem.GetComponent<Rigidbody>().isKinematic = false;
        currentItem = null;
    }
}