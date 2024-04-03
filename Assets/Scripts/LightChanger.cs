using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightChanger : MonoBehaviour
{
    public Light worldLight;
    public GameObject gumLight;
    public bool CaveModeLights;
    public bool DRModeLights;

    public void InOverworld()
    {
        worldLight.intensity = 1f;
        gumLight.SetActive(false);
        CaveModeLights = false;
        DRModeLights = false;
    }
    public void InCave()
    {
        worldLight.intensity = 0.1f;
        gumLight.SetActive(true);
        CaveModeLights = true;
        DRModeLights = false;
    }
    public void InDR()
    {
        worldLight.intensity = 0.7f;
        gumLight.SetActive(false);
        CaveModeLights = false;
        DRModeLights = true;
    }
}