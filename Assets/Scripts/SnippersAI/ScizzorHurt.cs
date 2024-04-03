using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScizzorHurt : MonoBehaviour
{
    void TurnHurt()
    {
        gameObject.tag = "Hurt";
    }
    void TurnGround()
    {
        gameObject.tag = "Ground";
    }
}
