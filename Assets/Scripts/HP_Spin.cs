using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP_Spin : MonoBehaviour
{
    private Animator anim;
    private SphereCollider hp_sphere;
    private TRADCONTROL GUMDROP;

    void Start()
    {
        anim = GetComponent<Animator>();
        hp_sphere = GetComponent<SphereCollider>();
        GUMDROP = FindObjectOfType<TRADCONTROL>();
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 65, 0) * Time.deltaTime);
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            hp_sphere.enabled = false;
            anim.SetTrigger("Touched");
        }
    }
    public void GiveHealth()
    {
        GUMDROP.currentHealth += 1;
    }
    public void HealthDestroy()
    {
        Destroy(gameObject);
    }
}
