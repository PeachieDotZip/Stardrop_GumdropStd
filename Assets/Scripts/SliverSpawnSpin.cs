using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliverSpawnSpin : MonoBehaviour
{
    public bool Part1;
    public bool Part2;
    //public bool Collected;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
        if (Part1 == true)
        {
            transform.Rotate(new Vector3(80, 0, 0) * Time.deltaTime);
        }
        if (Part2 == true)
        {
            transform.Rotate(new Vector3(-80, 0, 0) * Time.deltaTime);
        }
    }
}
