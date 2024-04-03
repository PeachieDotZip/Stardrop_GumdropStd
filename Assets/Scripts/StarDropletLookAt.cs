using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarDropletLookAt : MonoBehaviour
{
    public Transform cameraT;
    private Camera maincam;

    // Start is called before the first frame update
    void Start()
    {
        maincam = Camera.main;
        cameraT = maincam.transform;
        transform.LookAt(cameraT, Vector3.up);
    }
    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(cameraT);

        // Same as above, but setting the worldUp parameter to Vector3.up
        transform.LookAt(cameraT, Vector3.up);
    }

}
