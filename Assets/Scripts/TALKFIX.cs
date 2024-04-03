using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TALKFIX : MonoBehaviour
{
    public TRADCONTROL GUMDROP;
    public void TalkFix()
    {
        GUMDROP.AllowedToLook = false;
        GUMDROP.direction.x = 0;
        GUMDROP.direction.z = 0;
        GUMDROP._moveSpeed = 0f;
        GUMDROP.currentDash = 0f;
        GUMDROP.gameObject.tag = "PlayerTalking";
    }
}
