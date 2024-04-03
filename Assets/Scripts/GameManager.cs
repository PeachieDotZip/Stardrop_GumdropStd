using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;
    private void Awake()
    {
        instance = this;
        Time.timeScale = 1f;
    }
    #endregion
    public int Stardrop_Amount;
    public int Starshard_Amount;
    public int Starsliver_Amount;
    public Transform Player_Position;
    private PlayerCounter PlayerCounter;
    private GameObject Portal;
    private GameObject Rock;
    [SerializeField] private GameObject StarsliverActivator;
    private Animator UIanim;
    public bool mainMenu;

    //Global variables to be used throughout entire project.
    public static bool isPaused;
    public static int itemID = 0;
    public static bool blurFX = true;
    public static bool hasBeatenGame;
    public static float sensitivity;
    public static float FOV;

    void Start()
    {
        if (mainMenu)
        {
            Player_Position = null;
            PlayerCounter = null;
            Portal = null;
            Rock = null;
            UIanim = GameObject.Find("Canvas").GetComponent<Animator>();
            GameObject.Find("Gumdrop").GetComponent<Animator>().SetBool("mainMenu", true);
            Cursor.lockState = CursorLockMode.None;
            //set default settings values
            sensitivity = 3f;
            FOV = 70f;
        }
        else
        {
            Player_Position = PlayerManager.instance.player.transform;
            PlayerCounter = FindObjectOfType<PlayerCounter>();
            Portal = GameObject.Find("DR_Portal");
            Portal.SetActive(false);
            Rock = GameObject.Find("FinalAreaRock");
            UIanim = GameObject.Find("Canvas").GetComponent<Animator>();
            Cursor.lockState = CursorLockMode.Locked;
            //vvv for testing, remove later
            sensitivity = 3f;
            FOV = 70f;
        }

        //Sets starting values for application (debug)
        Application.targetFrameRate = 60;
    }

    void Update()
    {
        if (isPaused == false && mainMenu == false)
        {
            Stardrop_Amount = PlayerCounter.count;
        }
        /*
        if (Input.GetKey(KeyCode.F))
        {
            Time.timeScale = 2f;
        }
        else if (Input.GetKey(KeyCode.Tab))
        {
            Time.timeScale = 1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Time.timeScale = 1.20f;
        }
        else if (Input.GetKey(KeyCode.UpArrow))
        {
            Time.timeScale = 1.60f;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            Time.timeScale = .40f;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            Time.timeScale = .80f;
        }
        */
    }
    /// <summary>
    /// Handles the Deitus Realm portal activation.
    /// </summary>
    public void PortalCheck()
    {
        //checks to see if the amount of starshards is enough. If it is, then it starts the coroutine
        if (Starshard_Amount == 2)
        {
            StartCoroutine(PortalCreation(.35f, 2f, 9f));
        }
    }
    /// <summary>
    /// Handles the activation of the StarShard when the player collects 8 star slivers.
    /// </summary>
    public void StarSliverCheck()
    {
        //checks to see if the amount of star slivers is enough. If it is, then it starts the coroutine
        if (Starsliver_Amount == 8)
        {
            StartCoroutine(StarSliverStarShardActivation(.5f, 1f, 12f));
        }
    }
    /// <summary>
    /// Handles the activation of the StarShard when the player collects 8 star slivers.
    /// </summary>
    public void UnblockFinalAreaCheck()
    {
        //checks to see if the amount of starshards is enough. If it is, then it starts the coroutine
        if (Starshard_Amount == 1)
        {
            StartCoroutine(UnblockFinalArea(.35f, 2f, 7.2f));
        }
    }

    IEnumerator PortalCreation(float time1, float time2, float time3)
    {
        //VVV fades to black and enables portal when starshards hit a certain amount VVV
        //disable ui
        yield return new WaitForSeconds(time1);
        UIanim.SetBool("CutSceneStart", true);
        yield return new WaitForSeconds(time2);
        Portal.SetActive(true);
        UIanim.SetBool("CutSceneStart", false);
        yield return new WaitForSeconds(time3);
        UIanim.SetBool("CutSceneStart", true);
        yield return new WaitForSeconds(time2);
        UIanim.SetBool("CutSceneStart", false);
        yield return new WaitForSeconds(time1);
        //enable ui
    }
    IEnumerator StarSliverStarShardActivation(float time1, float time2, float time3)
    {
        //VVV turns on the camera and gameobject that controls the starshard spawning VVV
        //disable ui
        yield return new WaitForSeconds(time1);
        UIanim.SetBool("CutSceneStart", true);
        yield return new WaitForSeconds(time2);
        if (StarsliverActivator != null)
        {
            StarsliverActivator.SetActive(true);
        }
        UIanim.SetBool("CutSceneStart", false);
        yield return new WaitForSeconds(time3);
        UIanim.SetBool("CutSceneStart", true);
        yield return new WaitForSeconds(time2);
        UIanim.SetBool("CutSceneStart", false);
        yield return new WaitForSeconds(time1);
        //enable ui
    }
    IEnumerator UnblockFinalArea(float time1, float time2, float time3)
    {
        //VVV fades to black and tells the rock to play its destroy animation when starshards hit a certain amount VVV
        //disable ui
        yield return new WaitForSeconds(time1);
        UIanim.SetBool("CutSceneStart", true);
        yield return new WaitForSeconds(time2);
        UIanim.SetBool("CutSceneStart", false);
        Rock.GetComponent<Animation>().Play();
        //animation plays, rock blocking final area entrance gets destroyed
        yield return new WaitForSeconds(time3);
        UIanim.SetBool("CutSceneStart", true);
        Destroy(Rock);
        yield return new WaitForSeconds(time2);
        UIanim.SetBool("CutSceneStart", false);
        Rock = null;
        yield return new WaitForSeconds(time1);
        //enable ui
    }
}
