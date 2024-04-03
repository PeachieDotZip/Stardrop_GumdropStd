using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activator : MonoBehaviour
{
    private Animator anim;
    public Animator activatee_anim;
    private TRADCONTROL GUMDROP;
    public GameObject CScamera;
    public bool Button;
    public bool CS_Button;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponentInParent<Animator>();
        GUMDROP = FindObjectOfType<TRADCONTROL>();
        CScamera.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (Button == true)
        {
            if (collision.gameObject.CompareTag("Player")
                && GUMDROP._isDashing == true)
            {
                GUMDROP._directionY = 0.1f;
                GUMDROP.direction.x = 0.1f;
                GUMDROP.direction.z = 0.1f;
                GUMDROP._moveSpeed = 0f;
                GUMDROP.anim.SetTrigger("Press");
                anim.SetTrigger("Press");

                if (CS_Button == true)
                {
                    StartCoroutine(Button_Cutscene(CScamera, 5f));
                }
                else
                {
                    activatee_anim.SetTrigger("activate");
                }
            }
        }
    }
    public IEnumerator Button_Cutscene(GameObject CScamera, float pause)
    {
        yield return new WaitForSeconds(2.21f);
        CScamera.SetActive(true);
        activatee_anim.SetTrigger("activate");
        GUMDROP.gameObject.tag = "PlayerTalking";
        yield return new WaitForSeconds(pause);
        CScamera.SetActive(false);
        GUMDROP.gameObject.tag = "Player";
        GUMDROP.AllowedToLook = true;
        GUMDROP._jumpSpeed = 2f;
        Button = false;
    }
}
