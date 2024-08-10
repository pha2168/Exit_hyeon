using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisBlock : MonoBehaviour
{
    public int Width;   // 블록의 가로 크기
    public int Height;  // 블록의 높이
    public int Depth;   // 블록의 깊이

    [SerializeField]
    private char[,,] blockData;  // 블록의 3D 데이터
    public bool hasInteracted = false; // 상호작용 여부를 나타내는 플래그

    void Start()
    {
        // BlockData 배열이 인스펙터에서 초기화된 경우 길이를 맞춤
        if (blockData == null || blockData.GetLength(0) != Width || blockData.GetLength(1) != Height || blockData.GetLength(2) != Depth)
        {
            blockData = new char[Width, Height, Depth];
            for (int x = 0; x < Width; x++)
            {
                for (int y = 0; y < Height; y++)
                {
                    for (int z = 0; z < Depth; z++)
                    {
                        blockData[x, y, z] = '0'; // 기본값 설정
                    }
                }
            }
        }
    }

    // BlockData를 반환
    public char[,,] GetBlockData()
    {
        return blockData;
    }

    // 특정 위치의 BlockData 설정
    public void SetBlockData(int x, int y, int z, char value)
    {
        if (x >= 0 && x < Width && y >= 0 && y < Height && z >= 0 && z < Depth)
        {
            blockData[x, y, z] = value;
        }
        else
        {
            Debug.LogError("Invalid block data position.");
        }
    }

    // 전체 BlockData 설정
    public void SetBlockData(char[,,] data)
    {
        if (data.GetLength(0) == Width && data.GetLength(1) == Height && data.GetLength(2) == Depth)
        {
            blockData = data;
        }
        else
        {
            Debug.LogError("Invalid block data size.");
        }
    }
}
