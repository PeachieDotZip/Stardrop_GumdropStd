using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MiniGolf : MonoBehaviour
{
    public Rigidbody rb;
    public float swing;
    Transform target;
    public TRADCONTROL GUMDROP;
    public Text golfcountText;
    public int Gcount;
    public Animator UIanim;
    public bool isGolfing = false;
    public GameObject GolfCounter;
    public BoxCollider HitTrigger;
    private SphereCollider GolfBall;
    public GameObject GolfHole;
    public GameObject BudshroomGolf;
    public GameObject BudshroomGolf_Lose;
    public GameObject BallRespawnPoint;
    public GolfHoleText GHT;
    public bool GolfWon;
    public DialogueInteraction Dinter;
    private bool EventTriggeredBefore = false;
    public bool GToggle;
    private ParticleSystem HitParticle;


    // Start is called before the first frame update
    void Start()
    {
        Gcount = 0;
        rb = GetComponent<Rigidbody>();
        target = PlayerManager.instance.player.transform;
        GolfBall = GetComponent<SphereCollider>();
        HitTrigger = GetComponent<BoxCollider>();
        HitParticle = GetComponent<ParticleSystem>();
        HitTrigger.enabled = false;
        GToggle = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isGolfing == true)
        {
            GolfCounter.SetActive(true);
            golfcountText.enabled = true;
            rb.isKinematic = false;
        }
        else
        {
            GolfCounter.SetActive(false);
            golfcountText.enabled = false;
            HitTrigger.enabled = false;
        }
        if (GolfWon == true && EventTriggeredBefore == false)
        {
            EventTriggeredBefore = true;
            Dinter.TriggerEvent();
            rb.isKinematic = true;
        }
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            HitTrigger.enabled = false;
            StartCoroutine(BringTriggerBack(2f));
            GolfBallHit();
            SetCountText();
        }
        if (other.gameObject.CompareTag("RespawnTrigger"))
        {
            RestartGolf();
        }
    }
    void SetCountText()
    {
        if (Gcount > 7)
        {
            UIanim.SetTrigger("GolfNumberFail");
        }
    }
    public void GolfBallHit()
    {
        Gcount += 1;
        UIanim.SetTrigger("GolfNumberSpin");
        golfcountText.text = Gcount.ToString();
        HitParticle.Play();
    }
    public void RestartGolf()
    {
        StartCoroutine(RespawnGolfBall(.50f));
        UIanim.SetTrigger("GolfNumberFail");
    }
    public void GolfStart()
    {
        isGolfing = true;
        GolfHole.SetActive(true);
        Gcount = 0;
        GHT.FinalGolfScore = 0;
        golfcountText.text = "0";
        HitTrigger.enabled = true;
    }
    public void GetIntoHole()
    {
        rb.velocity = new Vector3(0, -.5f, 0);
        GolfBall.enabled = false;
    }
    public void FunniGolfRespawn()
    {
        GolfBall.transform.position = BallRespawnPoint.transform.position;
        rb.velocity = new Vector3(0, 0, 0);
    }
    public void ToggleGolfMode()
    {
        StartCoroutine(GolfModeIs(.1f));
        GolfBall.transform.position = BallRespawnPoint.transform.position;
        rb.velocity = new Vector3(0, 0, 0);
        if (GToggle == true)
        {
            //GToggle = !GToggle;
        }
    }
    private IEnumerator BringTriggerBack(float time)
    {
        yield return new WaitForSeconds(time);
        HitTrigger.enabled = true;
        HitParticle.Stop();
    }
    public IEnumerator GolfModeIs(float time)
    {
        yield return new WaitForSeconds(time);
        if (GToggle == true)
        {
            GolfStart();
            Debug.Log("CRACK");
        }
        if (GToggle == false)
        {
            isGolfing = false;
            Debug.Log("poop");
        }
    }
    public IEnumerator RespawnGolfBall(float time)
    {
            yield return new WaitForSeconds(time);
            HitTrigger.enabled = true;
            GolfBall.enabled = true;
            GolfBall.transform.position = BallRespawnPoint.transform.position;
            rb.velocity = new Vector3(0, 0, 0);
            GHT.GolfBall.SetActive(true);
        if (BudshroomGolf_Lose != null) { BudshroomGolf_Lose.GetComponent<DialogueInteraction>().EventTrigger = false; }
            Gcount = 0;
            rb.isKinematic = true;
            yield return new WaitForSeconds(1f);
            rb.velocity = new Vector3(0, 0, 0);
    }
}