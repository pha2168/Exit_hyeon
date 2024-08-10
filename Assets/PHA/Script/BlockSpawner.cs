using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockSpawner : MonoBehaviour
{
    public GameObject TetriseBlock1;
    public GameObject TetriseBlock2;
    public GameObject TetriseBlock3;
    public GameObject TetriseBlock4;
    public GameObject TetriseBlock5;
    public GameObject TetriseBlock6;
    public GameObject TetriseBlock7;
    public GameObject TetriseBlock8;

    private GameObject nextBlock;

    void Start()
    {
        nextBlock = SpawnNextBlock(); // 첫 블록을 미리 생성
    }

    // 스폰 가능한 블록 프리팹 목록에서 랜덤으로 블록을 선택하여 스폰
    public GameObject SpawnBlock()
    {
        GameObject currentBlock = nextBlock;
        nextBlock = SpawnNextBlock(); // 다음 블록을 미리 준비
        return currentBlock;
    }

    // 다음에 나올 블록을 스폰하여 리턴
    private GameObject SpawnNextBlock()
    {
        int blockIndex = Random.Range(1, 9); // 1부터 8까지 랜덤 값
        GameObject blockPrefab = null;

        switch (blockIndex)
        {
            case 1:
                blockPrefab = TetriseBlock1;
                break;
            case 2:
                blockPrefab = TetriseBlock2;
                break;
            case 3:
                blockPrefab = TetriseBlock3;
                break;
            case 4:
                blockPrefab = TetriseBlock4;
                break;
            case 5:
                blockPrefab = TetriseBlock5;
                break;
            case 6:
                blockPrefab = TetriseBlock6;
                break;
            case 7:
                blockPrefab = TetriseBlock7;
                break;
            case 8:
                blockPrefab = TetriseBlock8;
                break;
        }

        GameObject newBlock = Instantiate(blockPrefab, transform.position, Quaternion.identity);

        // 블록 데이터 값을 동일한 랜덤한 값으로 설정
        TetrisBlock tetrisBlock = newBlock.GetComponent<TetrisBlock>();
        if (tetrisBlock != null)
        {
            char randomValue = (char)('0' + Random.Range(1, 7)); // '1'부터 '6' 사이의 랜덤한 문자
            for (int x = 0; x < tetrisBlock.Width; x++)
            {
                for (int y = 0; y < tetrisBlock.Height; y++)
                {
                    for (int z = 0; z < tetrisBlock.Depth; z++)
                    {
                        if (tetrisBlock.GetBlockData()[x, y, z] != '0') // '0'이 아닌 경우만 변경
                        {
                            tetrisBlock.SetBlockData(x, y, z, randomValue);
                        }
                    }
                }
            }
        }

        return newBlock;
    }

    // NextBlock을 반환하는 메서드
    public GameObject GetNextBlock()
    {
        return nextBlock;
    }
}
