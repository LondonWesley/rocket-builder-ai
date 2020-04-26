using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketBlock : BasicBlock
{
    // Start is called before the first frame update
    float nozzleDiameter = 1f;
    float fuelConsumeRate = 0.1f;
    public List<FuelBlock> fuelSources;
    public GameObject flame;
    public GameObject nozzle;

    void Start()
    {
         base.Init();
    }

    // Update is called once per frame
    public void setNozzleDiameter( float diameter)
    {
        nozzleDiameter = diameter;
        nozzle.transform.localScale = new Vector3(diameter, 0.5f, diameter);
    }
    public void setFuelConsumeRate(float rate)
    {
        fuelConsumeRate = rate;
    }
    public void addSource(FuelBlock source)
    {
        fuelSources.Add(source);
    }
    void fireEngine()
    {
        if (!fuelSources.Count.Equals(0))
        {
            Debug.Log("FUEL SOURCE FOUND!");

            if (fuelSources[fuelSources.Count - 1].burn(fuelConsumeRate))
            {
                Debug.Log("BURNING");
                rigidBody.AddForce(transform.up * 200000f * Time.fixedDeltaTime);
                flame.SetActive(true);
            }

            else
            {
                fuelSources.Remove(fuelSources[fuelSources.Count - 1]);
                flame.SetActive(false);
                Debug.Log("No fuel");
            }
        }
    }
    void FixedUpdate()
    {
        fireEngine();
    }
}
