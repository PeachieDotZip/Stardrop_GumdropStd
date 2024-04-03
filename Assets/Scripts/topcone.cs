/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class topcone : MonoBehaviour
{
    GameObject tc;
    public TowerSnipper twrsnp;
    void Start()
    {
        tc = GameObject.Find("TopCone");
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject == twrsnp.target && (twrsnp.GUMDROP._isDashing == true))
        {
            tc.transform.SetParent(transform);
        }
    }

}
*/
