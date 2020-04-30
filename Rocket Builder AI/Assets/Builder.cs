using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder :BasicBlock
{
    // Start is called before the first frame update

    public List<GameObject> placeableModules;
    public List<GameObject> connectedParts;
    //saves block building sequence actions for loading later
    public List<float[]> shipBlockData;
    public int parts = 4;
    public bool replay;
    void Start()
    {
        base.Init();
        shipBlockData = new List<float[]>();
        connectedParts.Add(this.gameObject);
      
    }
    
    public void destroyShip()
    {
        for (int i = 0; i < connectedParts.Count; i++)
            Destroy(connectedParts[i]);
    }

    public void generateShip()
    {
        StartCoroutine("generate");
    }

    public void mutate(int numMutations, List<float[]> ship)
    {
        for(int i = 0; i < numMutations; i++)
        {
            Debug.Log("mutating!");
            int actionChoice = Random.Range(1, 3);
            Debug.Log(actionChoice);
            if(actionChoice == 2)
            {
                for(int j = 0; j < 1; j++)
                {
                    int blockType = Random.Range(0, placeableModules.Count);
                    int attachTo = Random.Range(0, connectedParts.Count);
                    int sideChosen = Random.Range(0, 6);
                    int blockIndex = Random.Range(0, connectedParts.Count-1);
                    //List<float[]> newData = new List<float[]>();

                    if(!placeModule(connectedParts[blockIndex].GetComponent<BasicBlock>(), blockType, sideChosen, 5, 0))
                    {
                        j--;
                        Debug.Log("retrying block placement");
                    } else
                    {
                        Debug.Log("block placed");
                        float[] data = {blockType, blockIndex, sideChosen, 0, 0 };
                        ship.Add(data);

                    }
                    
                }
            }
            if(actionChoice == 3)
            {

            }

        }
    }
    IEnumerator generate()
    {

        yield return new WaitForSeconds(5);

        for (int i = 0; i < parts; i++)
        {
            Debug.Log("Welding a new rocket module!");

            int blockType = Random.Range(0, placeableModules.Count);
            int attachTo = Random.Range(0, connectedParts.Count);
            int sideChosen = Random.Range(0, 6);

            GameObject module = Instantiate(placeableModules[blockType], new Vector3(0, 100, 0), Quaternion.identity);

            BasicBlock blockScript = module.GetComponent<BasicBlock>();
            BasicBlock targetBlockScript = connectedParts[attachTo].GetComponent<BasicBlock>();

            if (blockScript.attachTo(targetBlockScript, sideChosen))
            {
                connectedParts.Add(module);
                checkAdjacentMoudules();
                float[] data = {blockType, attachTo, sideChosen, 0, 0 };
                shipBlockData.Add(data);
                displayConnections();
                blockScript.displayConnections();
            }
            else
            {
                i--;
            }
            
        }
        randomizeModuleSettings();
        /*BasicBlock cockpit = connectedParts[0].GetComponent<BasicBlock>();
        placeModule(cockpit, 0, 1);
        GetComponentInParent<Rigidbody>().useGravity = true;*/
        
    }
    // randomly changes all settings of blocks in the system

    public void loadShipData(List<float[]> shipData)
    {
        Debug.Log("Attempting load");
        StartCoroutine("loadShip", shipData);
    }
    IEnumerator loadShip(List<float[]> shipData)
    {
        Debug.Log("Courinte waiteing?");
        yield return new WaitForSeconds(5);
        shipBlockData = new List<float[]>(shipData);
        Debug.Log("Courinte waited");
        for (int i = 0; i < shipData.Count; i++)
        {
            int targetIndex = (int) shipData[i][1];
            int blockType = (int) shipData[i][0];
            int sidechosen = (int)shipData[i][2];
            float nozzleDiam = shipData[i][3];
            float consumeRate = shipData[i][3];
            Debug.Log("Placing block");
            BasicBlock target = connectedParts[targetIndex].GetComponent<BasicBlock>();
            Debug.Log("nozzle Diameter loaded is " + nozzleDiam);
            placeModule(target, blockType, sidechosen, nozzleDiam, consumeRate);
        }
        if (replay != true)
            mutate(3, shipBlockData);
        connectEngines();

    }

    public List<float[]> getShipBlockData()
    {
        return this.shipBlockData;
    }

    void connectEngines() {

        for (int i = 0; i < connectedParts.Count; i++)
        {
            BasicBlock currentBlock = connectedParts[i].GetComponent<BasicBlock>();
            RocketBlock rocket = currentBlock as RocketBlock;

            if (rocket != null)
            {
                List<FuelBlock> sources = getFuelSources();
                if (!sources.Count.Equals(0))
                {
                    rocket.fuelSources = sources;
                }

            }

        }
    }
    void randomizeModuleSettings()
    {
        for(int i = 0; i < connectedParts.Count; i++)
        {
            BasicBlock currentBlock = connectedParts[i].GetComponent<BasicBlock>();
            RocketBlock rocket = currentBlock as RocketBlock;

            if(rocket != null)
            {
                float nozzleDiam = Random.Range(1f, 5f);
       
                shipBlockData[i][3] = nozzleDiam;
                
                rocket.setNozzleDiameter(nozzleDiam);
                List<FuelBlock> sources = getFuelSources();
                if (!sources.Count.Equals(0))
                {
                        rocket.fuelSources = sources;
                }

            }
            FuelBlock fuel = currentBlock as FuelBlock;
            if(fuel != null)
            {
               //todosettings here
            }

        }
    }

    List<FuelBlock> getFuelSources()
    {
        List<FuelBlock> sources = new List<FuelBlock>();
        for (int i = 0; i < connectedParts.Count; i++)
        {
            FuelBlock fuel = connectedParts[i].GetComponent<BasicBlock>() as FuelBlock;
            if (fuel != null)
            {
                sources.Add(fuel);
            }
        }
        return sources;
    }

    bool placeModule(BasicBlock targetBlockScript, int moduleNum, int side , float nozzleDiam, float consumeRate)
    {

        GameObject module = Instantiate(placeableModules[moduleNum], new Vector3(0, 100, 0), Quaternion.identity);

        BasicBlock blockScript = module.GetComponent<BasicBlock>();

        RocketBlock rocket = blockScript as RocketBlock;
        if(rocket!= null)
        {
            rocket.setNozzleDiameter(nozzleDiam);
            rocket.setFuelConsumeRate(consumeRate);
        }
           if (blockScript.attachTo(targetBlockScript, side))
            {
                connectedParts.Add(module);
                blockScript.checkAdjacentMoudules();
            return true;
            }
        return false;
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
