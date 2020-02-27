using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Builder :BasicBlock
{
    // Start is called before the first frame update

    public List<GameObject> modules;
    void Start()
    {
        base.Init();
        StartCoroutine(buildTest());
        
      
    }

    IEnumerator buildTest()
    {

        yield return new WaitForSeconds(5);
        Debug.Log("Welding a new rocket module!");
        GameObject rocket = Instantiate(modules[0], new Vector3(0, 0, 0), Quaternion.identity);
        BasicBlock rocketScript = rocket.GetComponent<RocketBlock>();
        rocketScript.attachTo(this,1);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
