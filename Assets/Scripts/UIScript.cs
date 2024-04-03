using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class UIScript : MonoBehaviour
{
    private Animator UIanim;
    public TRADCONTROL GUMDROP;
    public Image FullH;
    public Image TwoH;
    public Image OneH;
    public Image ZeroH;
    public Image OneHb;
    public Transform player;
    public GameObject TP;
    public GameObject DR_PreviewCamera;
    public MiniGolf MG;
    private CameraRotation camRot;
    public GameObject pauseMenu;
    private PostProcessingScript postProcessingScript;

    // Start is called before the first frame update
    void Start()
    {
        UIanim = GetComponent<Animator>();
        DR_PreviewCamera.SetActive(false);
        camRot = FindObjectOfType<CameraRotation>();
        postProcessingScript = FindObjectOfType<PostProcessingScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            TeleportGMDRP();
        }
        //debug code

        if (GUMDROP.currentHealth == 3)
        {
            FullH.enabled = true;
            TwoH.enabled = false;
            OneH.enabled = false;
            ZeroH.enabled = false;
            OneHb.enabled = false;
        }
        if (GUMDROP.currentHealth == 2)
        {
            FullH.enabled = false;
            TwoH.enabled = true;
            OneH.enabled = false;
            ZeroH.enabled = false;
            OneHb.enabled = false;
        }
        if (GUMDROP.currentHealth == 1)
        {
            FullH.enabled = false;
            TwoH.enabled = false;
            OneH.enabled = true;
            ZeroH.enabled = false;
            // VVV Triggers "tired" Gumdrop anim
            GUMDROP.anim.SetBool("isTired", true);
        }
        if (GUMDROP.currentHealth <= 0)
        {
            FullH.enabled = false;
            TwoH.enabled = false;
            OneH.enabled = false;
            ZeroH.enabled = true;
            OneHb.enabled = false;
            // VVV Un-triggers tired Gumdrop anim
            GUMDROP.anim.SetBool("isTired", false);
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameManager.isPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        postProcessingScript.currentdepthIntensity = 9.6f;
        GameManager.isPaused = false;
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Pause()
    {
        pauseMenu.SetActive(true);
        Time.timeScale = 0f;
        postProcessingScript.currentdepthIntensity = 0f;
        GameManager.isPaused = true;
        Cursor.lockState = CursorLockMode.None;
    }
    public void ExitToMainMenu()
    {
        //play animation
        SceneManager.LoadScene("Stage0");
    }
    public void LoadMainMenu()
    {
        //use this function in the "exit to main menu" animation
    }
    private void HideUI()
    {
        //include a list of GROUPED objects of main ui elements to disable when cutscenes play
    }
    public void DoorTeleport()
    {
        player.transform.position = GUMDROP.Door.doortelePoint.transform.position;
        StartCoroutine(DoorTeleportAnimEndDelay(.5f));
        camRot._LocalRotation = new Vector3(-90, 0, 0);
    }

    public void FlashReset()
    {
        UIanim.ResetTrigger("Flash");
    }
    public void DR_Preview()
    {
        DR_PreviewCamera.SetActive(true);

    }
    public void DR_Preview_End()
    {
        Destroy(DR_PreviewCamera);
        Destroy(GameObject.Find("Bungo_Snooper"));

    }
    public void UnGolfNumbSpin()
    {
        UIanim.ResetTrigger("GolfNumberSpin");
    }
    public void UnGolf()
    {
        UIanim.ResetTrigger("GolfNumberFail");
        UIanim.ResetTrigger("GolfNumberWin");
        TP = GameObject.Find("PostGolfTelePoint");
    }
    public void PostGolfTeleport()
    {
        player.transform.position = TP.transform.position;
        MG.Gcount = 0;
        MG.isGolfing = false;
        MG.golfcountText.enabled = false;
    }
    public void TeleportGMDRP()
    {
        player.transform.position = TP.transform.position;
    }
    public void TeleportGMDRP_ToPoint(Transform point)
    {
        Debug.Log("Teleported Gumdrop To " + point.name);
        player.transform.position = point.position;
    }
    public void TeleportGMDRP_AfterDR()
    {
        player.transform.position = new Vector3(2144.27f, 255.45f, -2168.16f);
        camRot._LocalRotation = new Vector3(-200, 0, 0);
    }

    public void BudshroomGolfEnable()
    {
        if (MG.BudshroomGolf != null) { MG.BudshroomGolf.transform.position = new Vector3(1962.8f, 259.652f, -2258.2f); }
        if (MG.BudshroomGolf_Lose != null) { MG.BudshroomGolf_Lose.transform.position = new Vector3(1962.8f, 209.652f, -2258.2f); }
        MG.GolfWon = true;
        StartCoroutine(GOLF_Unsetallgameobjects(.21f));
    }
    public void BudshroomGolf_LoseEnable()
    {
        MG.BudshroomGolf.transform.position = new Vector3(1962.8f, 209.652f, -2258.2f);
        MG.BudshroomGolf_Lose.transform.position = new Vector3(1962.8f, 259.652f, -2258.2f);
    }

        IEnumerator DoorTeleportAnimEndDelay(float time)
    {
        yield return new WaitForSeconds(time);
        UIanim.SetBool("Enter", false);
    }
    private IEnumerator GOLF_Unsetallgameobjects(float time)
    {
        yield return new WaitForSeconds(time);
        MG.BudshroomGolf = null;
        Destroy(MG.BudshroomGolf_Lose);
    }
}