using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public Transform player;
    public Transform respawnPoint;
    public GameObject GUMDROP;
    public TRADCONTROL gumdropcontrol;
    public HealthBar healthBar;
    public int maxHealth = 3;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //player.transform.position = respawnPoint.transform.position;
            gumdropcontrol.TakeDamage(10);

        }
        if (other.gameObject.CompareTag("PlayerHurt"))
        {
            player.transform.position = respawnPoint.transform.position;
        }
    }
    void FixedUpdate()
    {
        if (gumdropcontrol.currentHealth <= 0)
        {
            StartCoroutine(Resp(gameObject, 3f));
            gumdropcontrol.anim.SetBool("Dead", true);
            gumdropcontrol.anim.SetBool("Hurt", false);
            player.transform.position = respawnPoint.transform.position;
           
        }
    }

    private IEnumerator Resp(GameObject target, float time)
    {

        yield return new WaitForSeconds(time);
        gumdropcontrol.healthBar.SetHealth(3);
        gumdropcontrol.currentHealth=maxHealth;
        gumdropcontrol.anim.SetBool("Dead", false);

    }
}
