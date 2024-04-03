using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounce : MonoBehaviour
{
    float mass = 2.5f; // defines the character mass
    Vector3 impact = Vector3.zero;
    private CharacterController _controller;

    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // call this function to add an impact force:
    void AddImpact(Vector3 dir, float force)
    {
       
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
        Debug.Log(impact);
    }

    void Update()
    {
        // apply the impact force:
        if (impact.magnitude > 0.1) _controller.Move(impact * Time.deltaTime);
        // consumes the impact energy each cycle:
        impact = Vector3.Lerp(impact, Vector3.zero, 2 * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "BouncyPlat")
        {
            Debug.Log("Hello: " + gameObject.name);
            AddImpact(Vector3.up, 333f);

        }
        if (other.tag == "Hurt")
        {
            Debug.Log("Hello: " + gameObject.name);
            AddImpact(Vector3.up, 333f);
        }
    }
}