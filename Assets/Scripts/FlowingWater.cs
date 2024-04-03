using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowingWater : MonoBehaviour
{
    public float speedY = 0.1f;
    private float curY;
    private Renderer thisRenderer;

    void Start()
    {
        thisRenderer = GetComponent<Renderer>();
        if (thisRenderer != null)
        {
            curY = thisRenderer.material.mainTextureOffset.y;
        }
    }

    void FixedUpdate()
    {
        curY += Time.deltaTime * speedY;
        thisRenderer.material.SetTextureOffset("_MainTex", new Vector2(thisRenderer.material.mainTextureOffset.x, curY));
    }
}
