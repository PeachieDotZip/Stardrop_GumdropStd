using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeSkybox : MonoBehaviour
{
    public int SkyboxChanger_ID;
    public LightChanger LC;

    public Material[] skyboxes;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            Debug.Log("Change Skybox");
            if (SkyboxChanger_ID == 0)
            {
                ChangeToSkybox_Overworld();
            }
            if (SkyboxChanger_ID == 1)
            {
                ChangeToSkybox_Cave();
            }
            if (SkyboxChanger_ID == 2)
            {
                ChangeToSkybox_DR();
            }
        }
    }


    void ChangeToSkybox_Random()
    {
        int x = Random.Range(0, skyboxes.Length - 1);
        RenderSettings.skybox = skyboxes[x];
    }
    void ChangeToSkybox_Overworld()
    {
        int x = 0;
        RenderSettings.skybox = skyboxes[x];
        LC.InOverworld();
        LC.worldLight.transform.eulerAngles = new Vector3(70, -30, 0);
    }
    void ChangeToSkybox_Cave()
    {
        int x = 1;
        RenderSettings.skybox = skyboxes[x];
        LC.InCave();
        LC.worldLight.transform.eulerAngles = new Vector3(90, -30, 0);
    }
    void ChangeToSkybox_DR()
    {
        int x = 2;
        RenderSettings.skybox = skyboxes[x];
        LC.InDR();
        LC.worldLight.transform.eulerAngles = new Vector3(62, -235, 0);
    }
}
