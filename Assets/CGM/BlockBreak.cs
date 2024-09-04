using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBreak : MonoBehaviour
{
    public GameObject[] childObjects;
    private BlockInCheck[] childScripts;
    private bool blocksReadyToBreak = false;

    // Start is called before the first frame update
    void Start()
    {
        childScripts = new BlockInCheck[childObjects.Length];
        for (int i = 0; i < childObjects.Length; i++)
        {
            if (childObjects[i] != null)
            {
                childScripts[i] = childObjects[i].GetComponent<BlockInCheck>();
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        blocksReadyToBreak = AllBlocksAreReadyToBreak();
    }

    void OnTriggerStay(Collider other)
    {
        if (blocksReadyToBreak && other.CompareTag("BLOCK"))
        {
            Destroy(other.gameObject);
        }
    }

    bool AllBlocksAreReadyToBreak()
    {
        foreach (var childScript in childScripts)
        {
            if (childScript != null)
            {
                if (!childScript.BlockBreakBool)
                {
                    return false;
                }
            }
        }
        return true;
    }
}