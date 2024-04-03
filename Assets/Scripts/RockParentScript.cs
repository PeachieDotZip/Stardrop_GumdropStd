using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This script is just pretty much used to contian public functions that can be used during animations via animation events.
public class RockParentScript : MonoBehaviour
{
    public ParticleSystem[] dustParticles;

    private void Awake()
    {
        //Sets the rock to be on the correct y level.
        transform.position = new Vector3(transform.position.x, 355.77f, transform.position.z);
    }
    public void ParticleOn(int which)
    {
        dustParticles[which].Play();
    }
    public void ParticleOff(int which)
    {
        dustParticles[which].Stop();
    }
}
