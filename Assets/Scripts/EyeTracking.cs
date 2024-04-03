using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EyeTracking : MonoBehaviour
{
    public GameObject Eye;

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.rotation = Eye.transform.rotation;
    }
}
