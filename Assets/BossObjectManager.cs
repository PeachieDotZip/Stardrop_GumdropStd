using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossObjectManager : MonoBehaviour
{
    #region Singleton
    public static BossObjectManager instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    public GameObject[] bossHitboxes;
    //Boss Hitbox List:
    // 0 - Sciz Left Collider
    // 1 - Sciz Right Collider
    // 2 - Bottom Collider
    // 3 - Sciz Left Collider (Right Arm Version / Final Phase)
    // 4 - Sciz Right Collider (Right Arm Version / Final Phase)
    // 5 - Final Phase Spinny Collider

}
