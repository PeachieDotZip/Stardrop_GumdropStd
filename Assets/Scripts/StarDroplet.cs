using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDroplet : MonoBehaviour
{
    private Animator anim;
    private Collider touchCollider;
    public PlayerCounter Count;
    private void Start()
    {
        anim = GetComponent<Animator>();
        touchCollider = GetComponent<Collider>();
        Count = FindObjectOfType<PlayerCounter>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touchCollider.enabled = false;
            anim.SetTrigger("Disappear");
            Count.Collect();
        }
    }

    public void AddStarDroplet()
    {
        //Count.Collect();
    }
    public void Remove()
    {
        Debug.Log("Destroyed " + gameObject.name);
        Destroy(gameObject);
    }
}
