using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class PostProcessingScript : MonoBehaviour
{
    private PostProcessVolume postProcessVolume;
    public ChromaticAberration chromaticAb;
    public DepthOfField depthF;
    public float currentAbIntensity = 0;
    public float currentdepthIntensity = 0;

    void Start()
    {
        postProcessVolume = GetComponent<PostProcessVolume>();
        postProcessVolume.profile.TryGetSettings(out chromaticAb);
        postProcessVolume.profile.TryGetSettings(out depthF);
        depthF.active = GameManager.blurFX;
    }
    void Update()
    {
        if (GameManager.blurFX == true)
        {
            chromaticAb.intensity.value = currentAbIntensity;
            depthF.focusDistance.value = currentdepthIntensity;
        }
        if (GameManager.isPaused)
        {
            depthF.active = GameManager.blurFX;
        }
    }
}
