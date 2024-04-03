using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletBehaviour : MonoBehaviour
{
    public Transform target;
    [SerializeField] private bool targetIsPlayer;
    public float projSpeed;
    Vector3 projDirection;
    public Rigidbody rb;
    [SerializeField] private int lifetime = 0;
    public GameObject explosion;

    /// <summary>
    /// This awake function is used to set the bullet to face its target and move in a forward direction when it is spawned.
    /// </summary>
    private void Awake()
    {
        if (targetIsPlayer)
        {
            target = FindObjectOfType<TRADCONTROL>().transform;
            transform.LookAt(target, Vector3.up);
            projDirection = (target.transform.position - transform.position).normalized * projSpeed;
            rb.velocity = new Vector3(projDirection.x, projDirection.y, projDirection.z);
            gameObject.tag = "Null";
        }
        else
        {
            target = null;
            //If desired, functionality can be added here to have bullet simple move in a straight line without a target.
        }
    }

    private void Update()
    {
        lifetime++;
        if (lifetime > 10)
        {
            gameObject.tag = "Hurt";
        }
        if (lifetime >= 777)
        {
            Debug.Log("Bullet deleted of old age");
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (lifetime > 10)
        {
            if (!other.gameObject.CompareTag("Hurt") & !other.gameObject.CompareTag("PlayerHurt")
                & !other.gameObject.CompareTag("EnemyWall") & !other.gameObject.CompareTag("BossWall")
                & !other.gameObject.CompareTag("BossGround") & !other.gameObject.CompareTag("NPC") & !other.gameObject.CompareTag("Null"))
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
        else
        {
            if (!other.gameObject.CompareTag("Hurt") & !other.gameObject.CompareTag("PlayerHurt")
               & !other.gameObject.CompareTag("EnemyWall") & !other.gameObject.CompareTag("BossWall")
               & !other.gameObject.CompareTag("BossGround") & !other.gameObject.CompareTag("NPC") & !other.gameObject.CompareTag("Player")
                & !other.gameObject.CompareTag("Null"))
            {
                Instantiate(explosion, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }
}
