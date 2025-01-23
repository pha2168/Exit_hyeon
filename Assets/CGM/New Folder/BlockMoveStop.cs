using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockMoveStop : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        BlockMove blockMove = GetComponent<BlockMove>();
        if (blockMove != null)
            Destroy(blockMove);

        foreach (Transform child in transform)
        {
            child.tag = "BLOCK";
        }

        Spawner spawner = FindObjectOfType<Spawner>();
        if (spawner != null)
        {
            spawner.SpawnBool = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
