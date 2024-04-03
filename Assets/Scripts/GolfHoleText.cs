using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GolfHoleText : MonoBehaviour
{
    public TextMeshPro holeinText;
    public Text golfcounterText;
    public MiniGolf MG;
    public GameObject GolfBall;
    public ParticleSystem HoleParticle;
    public Animator anim;
    public Collider HoleTrigger;
    public int FinalGolfScore;

    // Start is called before the first frame update
    void Start()
    {
        holeinText = GetComponent<TextMeshPro>();
    }
    public void OnEnable()
    {
        HoleTrigger.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("GolfBall") && MG.Gcount <= 7)
        {
            HoleTrigger.enabled = false;
            MG.GetIntoHole();
            FinalGolfScore = MG.Gcount;
            SetHoleText();
            HoleParticle.Play();
            StartCoroutine(StopHoleParticle(3.50f));
            GolfingWin();
            StartCoroutine(MG.RespawnGolfBall(3.21f));
        }
        if (other.CompareTag("GolfBall") && MG.Gcount > 7)
        {
            MG.GetIntoHole();
            MG.RestartGolf();
            StartCoroutine(StopHoleParticle(3.50f));
        }
    }

    public void SetHoleText()
    {
        holeinText.text = "Hole in " + MG.Gcount.ToString() + "!";
    }

    public void GolfingWin()
    {
        MG.UIanim.SetTrigger("GolfNumberWin");
        anim.SetTrigger("GolfHoleComplete");
        MG.GolfWon = true;
    }

    public IEnumerator StopHoleParticle(float time)
    {
        yield return new WaitForSeconds(time);
        HoleParticle.Stop();
        GolfBall.SetActive(true);
    }
}
