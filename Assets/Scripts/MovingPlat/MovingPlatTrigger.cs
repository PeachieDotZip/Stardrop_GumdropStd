using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatTrigger : MonoBehaviour
{
    private GameObject place;
    public MovingPlat platform;
    private void OnTriggerEnter(Collider other)
    {
        platform.NextPlatform();
        Renderer rend = platform.GetComponent<Renderer>();
        rend.material = Resources.Load<Material>("Player");
    }
    private void OnTriggerStay(Collider other)
    {
        Renderer rend = platform.GetComponent<Renderer>();
        rend.material = Resources.Load<Material>("Player");
    }
        private void OnTriggerExit(Collider other)
    {
        Renderer rend = platform.GetComponent<Renderer>();
        rend.material = Resources.Load<Material>("KOKUJIN");
        platform.NextPlatform();
    }
}