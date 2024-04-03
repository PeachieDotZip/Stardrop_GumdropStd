using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerCounter : MonoBehaviour
{

    public Text countText;
    public Text winText;
    public int count;
    public GameObject StarShard100;
    public GameObject poop;

    void Start()
    {
        count = 0;
        SetCountText();
        winText.text = "";
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Pick Up"))
        {
            SetCountText();
        }
    }

    void SetCountText()
    {
        countText.text = "X  " + count.ToString();

        if (count == 100)
        {
            StarShard100.SetActive(true);
        }

        if (count >= 120)
        {
            winText.text = "WOW! WHY?";
            poop.SetActive(true);
        }
    }
    public void Collect()
    {
        count += 1;
    }
}