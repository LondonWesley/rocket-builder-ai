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
    IEnumerator generate()
    {

        yield return new WaitForSeconds(5);

        for (int i = 0; i < 4; i++)
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
    IEnumerable loadShip(List<float[]> shipData)
    {
        yield return new WaitForSeconds(5);
        Debug.Log("Courinte waited");
        for (int i = 0; i < shipData.Count; i++)
        {
            int targetIndex = (int) shipData[i][1];
            int blockType = (int) shipData[i][0];
            int sidechosen = (int)shipData[i][2];
            Debug.Log("Placing block");
            BasicBlock target = connectedParts[targetIndex].GetComponent<BasicBlock>();
            placeModule(target, blockType, sidechosen);
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
                float nozzleDiam = Random.Range(0.1f, 0.5f);
                shipBlockData[i][3] = nozzleDiam;
                rocket.setNozzleDiameter(nozzleDiam);
                List<FuelBlock> sources = getFuelSources();
                if (!sources.Count.Equals(0))
                {
                    for (int j = 0; j < sources.Count; j++)
                    {

                        rocket.fuelSources = getFuelSources();
                    }
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

    bool placeModule(BasicBlock targetBlockScript, int moduleNum, int side)
    {
          
            GameObject module = Instantiate(placeableModules[moduleNum], new Vector3(0, 100, 0), Quaternion.identity);

            BasicBlock blockScript = module.GetComponent<BasicBlock>();
           
            if (blockScript.attachTo(targetBlockScript, side))
            {
                connectedParts.Add(module);
                checkAdjacentMoudules();
            return true;
            }
        return false;
        }

    // Update is called once per frame
    void Update()
    {
        
    }
}
