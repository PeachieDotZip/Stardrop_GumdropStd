using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public GameObject player;

    public Vector3 offset;

    void Start()
    {
        //offset = transform.position - player.transform.position;
        offset = transform.position = new Vector3(0f, 6.21f, 0f);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = player.transform.position + offset;
    }
}
