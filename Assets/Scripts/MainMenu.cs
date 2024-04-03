using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Animator menuAnim;
    public Animator cameraAnim;

    public void PlayGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void QuitGame()
    {
        Debug.Log("Bye lol");
        Application.Quit();
    }
    public void EnterOptionsMenu()
    {
        menuAnim.SetTrigger("options");
        cameraAnim.SetTrigger("options");
    }
}
