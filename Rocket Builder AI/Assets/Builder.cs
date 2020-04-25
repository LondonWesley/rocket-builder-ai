using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder :BasicBlock
{
    // Start is called before the first frame update

    public List<GameObject> placeableModules;
    public List<GameObject> connectedParts;
    //saves block building sequence actions for loading later
    public List<double[]> shipBlockData;
    void Start()
    {
        base.Init();
        StartCoroutine(generateShip());
        connectedParts.Add(this.gameObject);
      
    }

    IEnumerator generateShip()
    {

        yield return new WaitForSeconds(5);

        for (int i = 0; i < 10; i++)
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
            }
            else
            {
                i--;
            }
            
        }
        /*BasicBlock cockpit = connectedParts[0].GetComponent<BasicBlock>();
        placeModule(cockpit, 0, 1);
        GetComponentInParent<Rigidbody>().useGravity = true;*/
      

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
