using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class thrustertest : MonoBehaviour
{
    // Start is called before the first frame update
    public Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.AddForce(transform.up * 2000f);
    }
}
