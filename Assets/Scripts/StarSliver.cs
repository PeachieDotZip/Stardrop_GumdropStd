using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StarSliver : MonoBehaviour
{ 
    private Animator anim;
    private Collider touchCollider;
    private GameManager GM;
    private TextMeshPro starsliverText;
    private void Start()
    {
        anim = GetComponent<Animator>();
        touchCollider = GetComponent<Collider>();
        GM = FindObjectOfType<GameManager>();
        starsliverText = GetComponentInChildren<TextMeshPro>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            touchCollider.enabled = false;
            GM.Starsliver_Amount += 1;
            print("starsliverCount: " + GM.Starsliver_Amount);
            anim.SetTrigger("starsliverDisappear");
            starsliverText.text = GM.Starsliver_Amount.ToString();
            this.transform.parent = null;
        }
    }
    public void Remove()
    {
        
        GM.StarSliverCheck();
        Debug.Log("Destroyed " + gameObject.name);
        Destroy(gameObject);
    }
}
