using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortalScript : MonoBehaviour
{
    public Animator anim;
    public GameObject Gumdrop;
    public TRADCONTROL GUMDROP;
    public Transform PortalPoint;
    public GameObject PortalTP;
    public Animator UIanim;
    private UIScript UIS;
    private bool ready;


    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        UIS = FindObjectOfType<UIScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (ready == true)
        {
            Gumdrop.transform.position = Vector3.Lerp(Gumdrop.transform.position, PortalPoint.position, Time.deltaTime);
            Vector3 direction = (gameObject.transform.localPosition - Gumdrop.transform.position).normalized;
            Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
            Gumdrop.transform.rotation = Quaternion.Slerp(Gumdrop.transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            anim.SetTrigger("Touch");
            ready = true;
        }
    }
    public void StarEntranceProcess()
    {
        StartCoroutine(PortalEnter(1f, 2.3f, 6f));
    }
    private IEnumerator PortalEnter(float time1, float time2, float time3)
    {
        Gumdrop.tag = "PlayerTalking";
        gameObject.GetComponent<SphereCollider>().enabled = false;
        yield return new WaitForSeconds(time1);
        Debug.Log("portalmoment");
        yield return new WaitForSeconds(time2);
        Gumdrop.transform.position += Vector3.up * 50;
        GUMDROP._gravity = 0f;
        ready = false;
        UIS.TP = PortalTP;
        yield return new WaitForSeconds(time3);
        GUMDROP._gravity = 1f;
        UIanim.SetTrigger("EnterPortal");
        StartCoroutine(GUMDROP.EnableGD(1.21f));
    }
}
