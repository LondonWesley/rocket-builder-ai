using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AISolution : MonoBehaviour
{
    public float fitnessScore = 0f;
    public GameObject Cockpit;
    public Builder builder;
    public bool running = true;
    public List<float[]> shipBlockData;

    void Start()
    {
        shipBlockData = new List<float[]>();
    }

    

    // Update is called once per frame
    void Update()
    {
        if(Cockpit != null)
        {
            float height = Cockpit.transform.position.y;
            if (height > fitnessScore)
            {
                fitnessScore = height;
            } else
            {
                if (height < fitnessScore - 30)
                {
                    shipBlockData = builder.shipBlockData;
                    builder.destroyShip();
                    running = false;

                }
            }

        }
    }
}
