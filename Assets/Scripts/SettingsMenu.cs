using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer audioMixer;
    public Animator menuAnim;
    public Animator cameraAnim;

    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    private void Start()
    {
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width &&
                resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetVolume(float Volume)
    {
        Debug.Log(Volume);
        audioMixer.SetFloat("Volume_Master", Volume);
    }
    public void SetSensitivity(float Sensitivity)
    {
        Debug.Log(Sensitivity);
        GameManager.sensitivity = Sensitivity;
    }
    public void SetFov(float Fov)
    {
        Debug.Log(Fov);
        GameManager.FOV = Fov;
    }

    public void DefaultSensitivity()
    {
        Debug.Log("Set Mouse Sensitivity to default setting. (3)");
        GameObject.Find("SensitivitySlider").GetComponent<Slider>().value = 3f;
        GameManager.sensitivity = 3f;
    }
    public void DefaultFOV()
    {
        Debug.Log("Set FOV to default setting. (70)");
        GameObject.Find("FOVSlider").GetComponent<Slider>().value = 70f;
        GameManager.FOV = 70f;
    }

    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void BlurEffectsToggle(bool blurEnabled)
    {
        GameManager.blurFX = blurEnabled;
    }

    public void ExitOptionsMenu()
    {
        menuAnim.SetTrigger("back");
        cameraAnim.SetTrigger("back");
    }

}
