using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject[] TetrisBlocks; // 다양한 테트리스 블록 프리팹을 여기에 설정

    public GameObject SpawnBlock()
    {
        int blockIndex = Random.Range(0, TetrisBlocks.Length);
        return Instantiate(TetrisBlocks[blockIndex], transform.position, Quaternion.identity);
    }
}
