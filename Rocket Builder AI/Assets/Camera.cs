using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera : MonoBehaviour
{
    // Start is called before the first frame update
    public Camera show;
    void Start()
    {
        show = this.GetComponent<Camera>();   
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate( new Vector3(0,0,0) ,Space.World);
    }
}
