using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockInCheck : MonoBehaviour
{
    public bool BlockBreakBool;


    // Start is called before the first frame update
    void Start()
    {

    }

    void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("BLOCK"))
        {
            BlockBreakBool = true;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("BLOCK"))
        {
            BlockBreakBool = false;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

}
