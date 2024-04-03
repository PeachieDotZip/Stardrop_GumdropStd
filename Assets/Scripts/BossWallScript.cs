using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWallScript : MonoBehaviour
{
    public int colliderID;
    private BossSnipper bossScript;
    [SerializeField] private GameObject player;
    public TRADCONTROL gumdrop;

    // Start is called before the first frame update
    void Start()
    {
        player = gumdrop.gameObject;
        bossScript = GetComponentInParent<BossSnipper>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == player & gumdrop._isDashing == true & colliderID == 0)
        {
            bossScript.PlayerCollidedWithShield();
            Debug.Log("Boss Should be confused");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == player & gumdrop._isDashing == true & colliderID == 1)
        {
            Debug.Log("Player Made Contact!");
            bossScript.BossDamaged();
        }
    }
}
