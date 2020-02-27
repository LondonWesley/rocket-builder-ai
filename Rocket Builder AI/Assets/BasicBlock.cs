using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*
 * All modules will inherit this class
 * This class will handle all physics welding
 */
public class BasicBlock : MonoBehaviour
{
    /*
     * Array of Connected Blocks on all 6 sides
     *  1:up 2:down 3:left 4:right 5:forward 6:back  
     */
    public ConfigurableJoint weld;
    public Rigidbody rigidBody;
    public BasicBlock[] sideConnections;
    public Transform transform;

    //TODO: CREATE METAL MATERIAL OPTIONS

    bool full = false;

    public void Init()
    {
        
        sideConnections = new BasicBlock[6];
        rigidBody = GetComponentInParent<Rigidbody>();
        weld = GetComponentInParent<ConfigurableJoint>();
        transform = GetComponentInParent<Transform>();
    }

    public void attachTo(BasicBlock targetBlock, int side)
    {
        switch (side) {
            //attach to topside
            case 1:
                Debug.Log("TOP SIDE!");
                transform.position = targetBlock.transform.position + new Vector3(1,1,1);
                weld.connectedBody = targetBlock.rigidBody;
                break;
            //attach to bottom
            case 2:

                break;
            //attach to left
            case 3:

                break;
            //attach to right
            case 4:

                break;
            //attach to front
            case 5:

                break;
            //attach to back
            case 6:

                break;

        }
        
        weld.connectedBody = targetBlock.rigidBody;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
