using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMove : MonoBehaviour
{
    public GameObject[] childObjects;
    private BlockCheck[] childScripts;
    bool canMoveDown = true;

    // Start is called before the first frame update
    void Start()
    {
        childScripts = new BlockCheck[childObjects.Length];
        for (int i = 0; i < childObjects.Length; i++)
        {
            if (childObjects[i] != null)
            {
                childScripts[i] = childObjects[i].GetComponent<BlockCheck>();
            }
        }

        InvokeRepeating("BlockMoveDown", 1.0f, 1.0f);
    }

    // Update is called once per frame
    void Update()
    {
        bool canMoveLeft = true;
        bool canMoveRight = true;
        bool canMoveForward = true;
        bool canMoveBack = true;
        

        foreach (var childScript in childScripts)
        {
            if (childScript != null)
            {
                if (childScript.LeftCheck)
                {
                    canMoveLeft = false;
                }
                if (childScript.RightCheck)
                {
                    canMoveRight = false;
                }
                if (childScript.ForwardCheck)
                {
                    canMoveForward = false;
                }
                if (childScript.BackCheck)
                {
                    canMoveBack = false;
                }
                if (childScript.DownCheck)
                {
                    canMoveDown = false;
                }
            }
        }

        if (canMoveLeft == true)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                transform.position += new Vector3(-1, 0, 0);
            }
        }
        if (canMoveRight == true)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                transform.position += new Vector3(1, 0, 0);
            }

        }
        if (canMoveForward == true)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                transform.position += new Vector3(0, 0, 1);
            }
        }
        if (canMoveBack == true)
        {
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                transform.position += new Vector3(0, 0, -1);
            }
        }

    }

    void BlockMoveDown()
    {
        if (canMoveDown == true)
        {
            transform.position += new Vector3(0, -1, 0);
        }
        else
        {            
            GetComponent<BlockMoveStop>().enabled = true;        
        }
    }
}