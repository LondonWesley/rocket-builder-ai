using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBlock : BasicBlock
{
    // Start is called before the first frame update
    float nozzleDiameter = 1f;
    float fuelConsumeRate = 0.1f;
    List<FuelBlock> fuelSources;
    public GameObject flame;
    public GameObject nozzle;

    void Start()
    {
         base.Init();
    }

    // Update is called once per frame

    void fireEngine()
    {
        if (fuelSources[fuelSources.Count - 1].burn(fuelConsumeRate) && fuelSources.Count>0)
        {
            rigidBody.AddForce(transform.up * 200f * Time.fixedDeltaTime);
            flame.SetActive(true);
        }
        else
        {
            fuelSources.Remove(fuelSources[fuelSources.Count - 1]);
            flame.SetActive(false);
            Debug.Log("No fuel");
        }
    }
    void FixedUpdate()
    {
        fireEngine();
    }
}
