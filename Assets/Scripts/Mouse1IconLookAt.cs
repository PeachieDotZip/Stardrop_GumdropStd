using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mouse1IconLookAt : MonoBehaviour
{
    public Transform Camera1;
    public bool isFacingMainCam;
    private Camera MainCam;

    // Start is called before the first frame update
    void Start()
    {
        MainCam = Camera.main;
        transform.LookAt(Camera1, Vector3.up);
    }
    void Update()
    {
        if (isFacingMainCam == true)
        {
            transform.LookAt(MainCam.transform);

            transform.LookAt(MainCam.transform, Vector3.up);
        }
        else
        {
            transform.LookAt(Camera1);

            transform.LookAt(Camera1, Vector3.up);
        }

    }

}
