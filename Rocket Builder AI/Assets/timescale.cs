using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class timescale : MonoBehaviour
{
    // Start is called before the first frame update
    public float timescaler = 0.1f;
    void Start()
    {
        Time.timeScale = timescaler;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = timescaler;
    }
}
