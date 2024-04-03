using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarShard : MonoBehaviour
{
    private bool InIdle;
    public ParticleSystem ShineSparkle;
    public Animator anim;
    public GameObject StarShardCamera;
    public GameObject GumdropLocation;
    Transform target;
    private TRADCONTROL GUMDROP;
    private GameObject Gumdrop;
    public bool isGetting;
    private Collider GetTrigger;
    private CameraRotation cam;
    private CameraController camcon;
    private Camera _cameram;
    private UIScript UIS;
    public GameObject parent;
    public Animator UIanim;
    public Animator ICONanim;
    public bool FloatTowardsRestPoint;
    public Transform RestPoint0;
    private Vector3 RestPoint;
    public float tolerance = 1.21f;
    public float speed = 10f;

    private bool canClick = true;

    [SerializeField]
    private bool isProp;
    [SerializeField]
    private GameObject realStarShard;
    //^^^ this bool is used to define whether a starshard is a prop or not.
    // When spawned, props will just fly into the sky, activate the *real* starshard, and then despawn.
    // Non-props actually stay for Gumdrop to collect. Most starshards in the game are non-props.

    private void Start()
    {
        target = PlayerManager.instance.player.transform;
        GUMDROP = FindObjectOfType<TRADCONTROL>();
        Gumdrop = GameObject.Find("GUMDROP");
        GetTrigger = GetComponent<SphereCollider>();
        cam = FindObjectOfType<CameraRotation>();
        camcon = FindObjectOfType<CameraController>();
        _cameram = Camera.main;
        UIS = FindObjectOfType<UIScript>();
        tolerance = speed * Time.deltaTime;
        if (isProp == false && RestPoint0 != null)
        {
            RestPoint = RestPoint0.transform.position;
        }
    }
    /// <summary>
    /// Awake is used here for the prop variables due to a bug in which the "isProp" bool does not correctly read when put in Start.
    /// By running before Start, Awake fixes this issue.
    /// <summary>
    private void Awake()
    {
        if (isProp == false)
        {
            realStarShard = null;
        }
        anim.SetBool("isProp", isProp);
    }
    // Update is called once per frame
    void Update()
    {
        if (InIdle == true)
        {
            if (FloatTowardsRestPoint == true)
            {
                StarShardCamera.SetActive(true);
                anim.SetBool("StarShardMove", true);
                ShineSparkle.Play();
                Vector3 floating = RestPoint - parent.transform.position;
                parent.transform.position += (floating / floating.magnitude) * speed * Time.deltaTime;
                GUMDROP.CompareTag("PlayerTalking");
                cam.CameraDisabled = true;
                UIanim.SetBool("HideUI", true);
                if (floating.magnitude < tolerance)
                {
                    parent.transform.position = RestPoint;
                    StopFloating();
                }
            }
        }
        if (isGetting == true)
        {
            GUMDROP._gravity = 0f;
            GUMDROP._directionY = 0f;
        }


        if (anim.GetCurrentAnimatorStateInfo(0).IsName("StarShardGetIdle") && (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > .1f))
        {
            if (Input.GetButtonDown("Interact") && canClick == true)
            {
                GetStarShard_End();
                canClick = false;
            }
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == Gumdrop)
        {
            GetStarShard_Triggered();
        }
    }


    public void DestroySelf()
    {
        Destroy(parent);
    }
    void GetStarShard_Triggered()
    {
        GetTrigger.enabled = false;
        isGetting = true;
        anim.SetTrigger("StarShardGet");
        GUMDROP.anim.SetTrigger("Got");
    }
    public void ShineSparklePlay()
    {
        ShineSparkle.Play();
    }
    public void ShineSparkleStop()
    {
        ShineSparkle.Stop();
    }
    public void StarShardProp()
    {
        Debug.Log("Spawned Real StarShard for " + gameObject.name);
        if (realStarShard != null)
        {
            realStarShard.SetActive(true);
        }
    }

    public void ShardIdle()
    {
        InIdle = true;
    }
    public void GetStarShard()
    {
        cam.SaveRotationFloats();
        StartCoroutine(ChangeCamFloats());
        //GetStarShardCamera.SetActive(true);
        target.transform.position = GumdropLocation.transform.position;
        Gumdrop.tag = "PlayerTalking";
        GUMDROP.AllowedToLook = false;
        Gumdrop.transform.LookAt(_cameram.transform);
        cam.CameraDisabled = true;
        UIanim.SetBool("CollectStarShard", true);
    }
    public void GetStarShard_End()
    {
        GameManager GM = FindObjectOfType<GameManager>();
        Gumdrop.tag = "Player";
        isGetting = false;
        cam.CameraDisabled = false;
        UIanim.SetBool("CollectStarShard", false);
        anim.SetTrigger("StarShardDisappear");
        GUMDROP.anim.SetTrigger("GetAnimEnd");
        GUMDROP.AllowedToLook = true;
        GUMDROP._gravity = 2.21f;
        cam.ApplySavedFloats();
        ICONanim.SetTrigger("StarShardIconFill");
        GM.Starshard_Amount += 1;
        GM.PortalCheck();
        GM.UnblockFinalAreaCheck();
    }
    public void FaceCamera()
    {
        parent.transform.LookAt(_cameram.transform);
    }
    public void StopFloating()
    {
        FloatTowardsRestPoint = false;
        StartCoroutine(RestAtPoint(1.21f));
        parent.transform.position = RestPoint0.position;

    }
    private IEnumerator ChangeCamFloats()
    {
        cam._CameraDistance = 5.5f;
        cam._LocalRotation.y = -8.18f;
        yield return new WaitForSeconds(1.6f);
        cam._CameraDistance = 5f;
        cam._LocalRotation.y = -10f;
    }
    public IEnumerator RestAtPoint(float time)
    {
        if (isProp == false)
        {
            yield return new WaitForSeconds(time);
            _cameram.GetComponent<LookAt>().target = null;
            cam.ApplySavedFloats();
            cam.ApplySavedPosition();
            cam.CameraDisabled = false;
            GUMDROP.CompareTag("Player");
            StarShardCamera.SetActive(false);
            anim.SetBool("StarShardMove", false);
            UIanim.SetBool("HideUI", false);
        }
    }
}
