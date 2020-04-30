using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelBlock : BasicBlock
{
    public float gallons;
    public float capacity;
    public int fuelType;
    public GameObject liquid;
    public bool levelChanged;
    // Start is called before the first frame update
    void Start()
    {
        capacity = 5;
        gallons = 5;
        base.Init();
    }

    // Update is called once per frame
    void Update()
    {
        updateLiquidFuelLevel();
        
    }

    public bool burn(float fuelConsumptionRate)
    {
        if (gallons > 0f)
        {
            gallons -= fuelConsumptionRate;
            return true;
        }
        return false;
    }
    void updateLiquidFuelLevel()
    {
        if (!levelChanged)
        {
            liquid.transform.localScale = new Vector3(1, gallons/capacity, 1);
        }
    }
}
