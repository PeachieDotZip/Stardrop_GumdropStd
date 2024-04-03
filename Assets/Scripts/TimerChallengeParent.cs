// This script is primarily used for the animation event(s) for the timer challenge parent.
// Reason the timer challenge parent even exists is for the local location/rotaion/scale for animations.
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerChallengeParent : MonoBehaviour
{
    public GameObject platformChallenge;
    public Collider sliverCollider;
    public bool hasWon = false;
    [SerializeField] private ParticleSystem platformParticleWin;
    [SerializeField] private ParticleSystem platformParticleFail;
    public GameObject introCamera;
    private TimerChallengeScript childScript;

    private void Start()
    {
        childScript = GetComponentInChildren<TimerChallengeScript>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (sliverCollider != null)
        {
            if (sliverCollider.enabled == false)
            {
                hasWon = true;
            }
        }
        else
        {
            hasWon = true;
        }
    }
    public void ActivatePlatforms()
    {
        platformChallenge.SetActive(true);
    }
    public void DeactivatePlatforms()
    {
        if (hasWon == true)
        {
            StartCoroutine(ChallengeWin());
        }
        else
        {
            StartCoroutine(ChallengeFail());
        }
    }

    public void Ready()
    {
        childScript.timerTrigger.enabled = true;
        childScript.anim.ResetTrigger("Triggered");
        childScript.anim.ResetTrigger("Fail");
    }

    public void TurnOnCamera()
    {
        introCamera.SetActive(true);
    }
    public void TurnOffCamera()
    {
        introCamera.SetActive(false);
    }

    public IEnumerator ChallengeWin()
    {
        childScript.anim.SetTrigger("Win");
        platformParticleWin.Play();
        yield return new WaitForSeconds(.5f);
        Destroy(introCamera);
        Destroy(platformChallenge);
    }
    public IEnumerator ChallengeFail()
    {
        childScript.anim.SetTrigger("Fail");
        platformParticleFail.Play();
        yield return new WaitForSeconds(.5f);
        platformChallenge.SetActive(false);
    }

    public void DestroySelf()
    {
        Debug.Log("Deleted: " + gameObject.name);
        Destroy(gameObject);
    }
}
