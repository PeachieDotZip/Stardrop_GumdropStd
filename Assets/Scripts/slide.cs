using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slide : MonoBehaviour
{
    public Vector3 slidingDirection;
    public bool sliding;
    public CharacterController _controller;
    public TRADCONTROL GUMDROP;

    void Update()
    {

    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (Vector3.Angle(transform.up, hit.normal) > _controller.slopeLimit && hit.gameObject.CompareTag("Slope"))
        {
            GUMDROP.direction.y -= (GUMDROP._gravity * Time.deltaTime) * 3f;
            GUMDROP.direction += slidingDirection;
            _controller.Move(GUMDROP.direction * Time.deltaTime);
            sliding = true;

            Vector3 normal = hit.normal;
            Vector3 c = Vector3.Cross(Vector3.up, normal);
            Vector3 u = Vector3.Cross(c, normal);
            slidingDirection = u * 4f;

        }
        else
        {
            sliding = false;
            slidingDirection = Vector3.zero;
        }
    }
}
 