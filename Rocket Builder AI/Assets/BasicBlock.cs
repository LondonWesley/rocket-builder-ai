﻿using System.Collections;
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
     *  0:up 1:down 2:left 3:right 4:forward 5:back  
     */
    public ConfigurableJoint constraint;
    public Rigidbody rigidBody;
    public BasicBlock[] sideConnections;
    public Transform transform;

    //TODO: CREATE METAL MATERIAL OPTIONS

    bool full = false;

    public void Init()
    {
        
        sideConnections = new BasicBlock[6];
        rigidBody = GetComponentInParent<Rigidbody>();
        constraint = GetComponentInParent<ConfigurableJoint>();
        transform = GetComponent<Transform>();
    }

    public bool attachTo(BasicBlock targetBlock, int side)
    {
        //checks if object is attached. if it's not at the end of the method it deletes itself
        bool attached = false;
        switch (side) {
            //attach to topside
            case 0:
              
                if (targetBlock.sideConnections[0] == null) {
                    Debug.Log("TOP SIDE!");
                    transform.position = targetBlock.transform.position + new Vector3(0, 2, 0);
                    sideConnections[1] = targetBlock;
                    targetBlock.sideConnections[0] = this;
                    attached = true;
                } 
                break;
            //attach to bottom
            case 1:
                if (targetBlock.sideConnections[1] == null)
                {

                    Debug.Log("BOTTOM SIDE!");
                    transform.position = targetBlock.transform.position + new Vector3(0, -2, 0);
                    sideConnections[0] = targetBlock;
                    targetBlock.sideConnections[1] = this;
                    attached = true;
                }
                //Time.timeScale = 0;
                
                break;
            //attach to left
            case 2:
                if (targetBlock.sideConnections[2] == null)
                {
                    Debug.Log("LEFT SIDE!");
                    transform.position = targetBlock.transform.position + new Vector3(-2, 0, 0);
                    sideConnections[3] = targetBlock;
                    targetBlock.sideConnections[2] = this;
                    attached = true;
                }
                break;

            //attach to right
            case 3:
                if (targetBlock.sideConnections[3] == null)
                {
                    Debug.Log("RIGHT SIDE!");
                    transform.position = targetBlock.transform.position + new Vector3(2, 0, 0);
                    sideConnections[2] = targetBlock;
                    targetBlock.sideConnections[3] = this;
                    attached = true;
                }
                break;
            //attach to front
            case 4:
                if (targetBlock.sideConnections[4] == null)
                {
                    Debug.Log("FRONT SIDE!");
                    transform.position = targetBlock.transform.position + new Vector3(0, 0, -2);
                    sideConnections[5] = targetBlock;
                    targetBlock.sideConnections[4] = this;
                    attached = true;
                }
                break;
            //attach to back
            case 5:
                if (targetBlock.sideConnections[5] == null)
                {
                    Debug.Log("BACK SIDE!");
                    transform.position = targetBlock.transform.position + new Vector3(0, 0, 2);
                    sideConnections[4] = targetBlock;
                    targetBlock.sideConnections[5] = this;
                    attached = true;
                }
                break;

        }
        if (attached)
        {
            weld(targetBlock);
            checkAdjacentMoudules();
            return true;
        }
        else
        {
            Destroy(gameObject);
            return false;
        }
    }
    private void weld(BasicBlock targetBlock)
    {
        constraint.connectedBody = targetBlock.rigidBody;
        constraint.xMotion = ConfigurableJointMotion.Locked;
        constraint.yMotion = ConfigurableJointMotion.Locked;
        constraint.zMotion = ConfigurableJointMotion.Locked;
        constraint.angularXMotion = ConfigurableJointMotion.Locked;
        constraint.angularYMotion = ConfigurableJointMotion.Locked;
        constraint.angularZMotion = ConfigurableJointMotion.Locked;
    }
    public void checkAdjacentMoudules()
    {
        RaycastHit checker;

        if (Physics.Raycast(transform.position, Vector3.up, out checker, 2f))
        {
            if (checker.collider.gameObject.GetComponent<BasicBlock>() != null)
            {
                Debug.DrawRay(transform.position, Vector3.up * checker.distance, Color.red);
                BasicBlock adjacentBlock = checker.collider.gameObject.GetComponent<BasicBlock>();
                sideConnections[0] = adjacentBlock;
                adjacentBlock.sideConnections[1] = this;
            }
        }
        if (Physics.Raycast(transform.position, -Vector3.up, out checker, 2f))
        {
            if (checker.collider.gameObject.GetComponent<BasicBlock>() != null)
            {
                Debug.DrawRay(transform.position, -Vector3.up * checker.distance, Color.green);
                BasicBlock adjacentBlock = checker.collider.gameObject.GetComponent<BasicBlock>();
                sideConnections[1] = adjacentBlock;
                adjacentBlock.sideConnections[0] = this;
            }
        }
        if (Physics.Raycast(transform.position, -Vector3.right, out checker, 2f))
        {
            
            if (checker.collider.gameObject.GetComponent<BasicBlock>() != null)
            {
                Debug.DrawRay(transform.position, -Vector3.right * checker.distance, Color.blue);
                BasicBlock adjacentBlock = checker.rigidbody.gameObject.GetComponent<BasicBlock>();
                sideConnections[2] = adjacentBlock;
                adjacentBlock.sideConnections[3] = this;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.right, out checker, 2f))
        {
            if (checker.collider.gameObject.GetComponent<BasicBlock>() != null)
            {
                Debug.DrawRay(transform.position, Vector3.right * checker.distance, Color.yellow);
                BasicBlock adjacentBlock = checker.collider.gameObject.GetComponent<BasicBlock>();
                sideConnections[3] = adjacentBlock;
                adjacentBlock.sideConnections[2] = this;
            }
        }
        if (Physics.Raycast(transform.position, Vector3.forward, out checker, 2f))
        {
            if (checker.collider.gameObject.GetComponent<BasicBlock>() != null)
            {
                Debug.DrawRay(transform.position, Vector3.forward * checker.distance, Color.cyan);
                BasicBlock adjacentBlock = checker.collider.gameObject.GetComponent<BasicBlock>();
                sideConnections[5] = adjacentBlock;
                adjacentBlock.sideConnections[4] = this;
            }
        }
        if (Physics.Raycast(transform.position, -Vector3.forward, out checker, 2f))
        {
            if (checker.collider.gameObject.GetComponent<BasicBlock>() != null)
            {
                Debug.DrawRay(transform.position, -Vector3.forward * checker.distance, Color.white);
                BasicBlock adjacentBlock = checker.collider.gameObject.GetComponent<BasicBlock>();
                sideConnections[4] = adjacentBlock;
                adjacentBlock.sideConnections[5] = this;
            }
        }
    }
    // Update is called once per frame
    void Update()
    {
    
    }
}