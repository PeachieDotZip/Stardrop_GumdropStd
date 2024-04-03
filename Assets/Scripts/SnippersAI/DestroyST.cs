using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyST : MonoBehaviour
{
    private TowerSnipper eyescript;
    [SerializeField] private bool spawnsObj;
    [SerializeField] private GameObject objToSpawn;
    [SerializeField] private bool helper;

    // Start is called before the first frame update
    void Start()
    {
        eyescript = GetComponentInChildren<TowerSnipper>();
        if (spawnsObj == false)
        {
            objToSpawn = null;
        }
    }
    public void STDead_Destroy()
    {
        if (spawnsObj == true && helper == false)
        {
            objToSpawn.SetActive(true);
            GameObject.Find("Budshroomy (7)").GetComponent<DialogueInteraction>().TriggerEvent();
        }
        if (spawnsObj == true && helper == true)
        {
            int healChance = Random.Range(1, 5);
            if (healChance == 4)
            {
                Instantiate(objToSpawn, new Vector3(transform.position.x, transform.position.y + 2, transform.position.z), Quaternion.identity);
            }
        }
        Destroy(gameObject);
    }
    public void STHurt_Reset()
    {
        eyescript.anim.ResetTrigger("STHurt");
    }
    public void HurtparticleOn()
    {
        eyescript.deadParticle.Play();
        eyescript.InRange = false;
    }
    public void HurtparticleOff()
    {
        eyescript.deadParticle.Stop();
        eyescript.InRange = true;
    }
    public void ShootBullet()
    {
        Instantiate(eyescript.bullet, eyescript.transform.position, Quaternion.identity);
    }
    public void ChargeOn()
    {
        if (eyescript.chargeParticle != null)
        {
            eyescript.chargeParticle.Play();
        }
    }
    public void ChargeOff()
    {
        if (eyescript.chargeParticle != null)
        {
            eyescript.chargeParticle.Stop();
        }
    }
}
