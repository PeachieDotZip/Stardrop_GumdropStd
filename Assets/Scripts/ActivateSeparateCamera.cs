using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateSeparateCamera : MonoBehaviour
{
    public GameObject separateCamera;
    public bool isActive = false;

    // Update is called once per frame
    void Update()
    {
        separateCamera.SetActive(isActive);
    }
}
