using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitEffectScript : MonoBehaviour
{
    private Transform target;

    private void Start()
    {
        target = Camera.main.transform;
        //Play Sound
    }
    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        transform.LookAt(target);

    }

    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
