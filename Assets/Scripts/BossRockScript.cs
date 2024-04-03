using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRockScript : MonoBehaviour
{
    public bool isPillar;
    [SerializeField] private int parentCount;
    public GameObject burst;

    private void OnTriggerEnter(Collider other)
    {
        //this is lazy as hell but like whateverrrrrr
        if (other.gameObject == BossObjectManager.instance.bossHitboxes[0] ||
            other.gameObject == BossObjectManager.instance.bossHitboxes[1] ||
            other.gameObject == BossObjectManager.instance.bossHitboxes[2] ||
            other.gameObject == BossObjectManager.instance.bossHitboxes[3] ||
            other.gameObject == BossObjectManager.instance.bossHitboxes[4] ||
            other.gameObject == BossObjectManager.instance.bossHitboxes[5])
        {
            CollidedWithBoss();
        }
    }

    private void CollidedWithBoss()
    {
        if (isPillar == false)
        {
            Instantiate(burst, transform.position, Quaternion.identity);
            switch (parentCount)
            {
                case 0:
                    Destroy(gameObject);
                    break;
                case 1:
                    Destroy(gameObject.transform.parent.gameObject);
                    break;
                case 2:
                    Destroy(gameObject.transform.parent.parent.gameObject); //<--- wtf
                    break;
                default:
                    Debug.Log("parentCount is outside of knowable range!");
                    break;
            }
        }
        else
        {
            BossSnipper snipper = FindObjectOfType<BossSnipper>();
            snipper.currentTarget = gameObject.transform.parent.parent.transform;
            snipper.BossStunned();
            Debug.Log("i just farted");
            Instantiate(burst, new Vector3(transform.position.x, (transform.position.y + 9f), transform.position.z), Quaternion.identity);
        }
    }
}
