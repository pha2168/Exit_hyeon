using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris : MonoBehaviour
{
    public int Width = 4;
    public int Height = 20;
    public int Depth = 4;
    public float dropSpeed = 1.0f;
    public GameObject[] TetrisBlocks; // 블록 프리팹 배열

    private GameObject currentBlock;
    private float lastFallTime;

    private char[,,] grid; // 블록이 쌓이는 3D 공간 표현

    void Start()
    {
        grid = new char[Width, Height, Depth];
        InitializeGrid();
        SpawnBlock();
    }

    void Update()
    {
        HandleInput();
        if (Time.time - lastFallTime > dropSpeed)
        {
            FallBlock();
            lastFallTime = Time.time;
        }
    }

    void InitializeGrid()
    {
        for (int x = 0; x < Width; x++)
        {
            for (int y = 0; y < Height; y++)
            {
                for (int z = 0; z < Depth; z++)
                {
                    grid[x, y, z] = '0';
                }
            }
        }
    }

    void SpawnBlock()
    {
        int randomIndex = Random.Range(0, TetrisBlocks.Length);
        currentBlock = Instantiate(TetrisBlocks[randomIndex], new Vector3(Width / 2, Height - 1, Depth / 2), Quaternion.identity);
    }

    void FallBlock()
    {
        if (currentBlock != null)
        {
            currentBlock.transform.position += Vector3.down;
            if (IsColliding())
            {
                currentBlock.transform.position += Vector3.up; // 충돌이 일어나면 블록을 제자리로
                AddBlockToGrid();
                ClearFullLayers();
                SpawnBlock();
            }
        }
    }

    bool IsColliding()
    {
        foreach (Transform block in currentBlock.transform)
        {
            Vector3 pos = block.position;
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            int z = Mathf.RoundToInt(pos.z);

            if (x < 0 || x >= Width || y < 0 || z < 0 || z >= Depth || grid[x, y, z] != '0')
            {
                return true;
            }
        }
        return false;
    }

    void AddBlockToGrid()
    {
        foreach (Transform block in currentBlock.transform)
        {
            Vector3 pos = block.position;
            int x = Mathf.RoundToInt(pos.x);
            int y = Mathf.RoundToInt(pos.y);
            int z = Mathf.RoundToInt(pos.z);

            grid[x, y, z] = '1';
        }

        Destroy(currentBlock);
    }

    void ClearFullLayers()
    {
        for (int y = 0; y < Height; y++)
        {
            if (IsLayerFull(y))
            {
                ClearLayer(y);
                DropLayersAbove(y);
            }
        }
    }

    bool IsLayerFull(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Depth; z++)
            {
                if (grid[x, y, z] == '0')
                {
                    return false;
                }
            }
        }
        return true;
    }

    void ClearLayer(int y)
    {
        for (int x = 0; x < Width; x++)
        {
            for (int z = 0; z < Depth; z++)
            {
                grid[x, y, z] = '0';

                // 블록 파괴
                RaycastHit hit;
                Vector3 pos = new Vector3(x, y, z);
                if (Physics.Raycast(pos + Vector3.up * 0.5f, Vector3.down, out hit, 1f))
                {
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }

    void DropLayersAbove(int clearedY)
    {
        for (int y = clearedY + 1; y < Height; y++)
        {
            for (int x = 0; x < Width; x++)
            {
                for (int z = 0; z < Depth; z++)
                {
                    if (grid[x, y, z] != '0')
                    {
                        grid[x, y - 1, z] = grid[x, y, z];
                        grid[x, y, z] = '0';

                        RaycastHit hit;
                        Vector3 pos = new Vector3(x, y, z);
                        if (Physics.Raycast(pos + Vector3.up * 0.5f, Vector3.down, out hit, 1f))
                        {
                            hit.collider.gameObject.transform.position += Vector3.down;
                        }
                    }
                }
            }
        }
    }

    void HandleInput()
    {
        if (currentBlock != null)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                currentBlock.transform.position += Vector3.left;
                if (IsColliding()) currentBlock.transform.position += Vector3.right;
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                currentBlock.transform.position += Vector3.right;
                if (IsColliding()) currentBlock.transform.position += Vector3.left;
            }
            else if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                currentBlock.transform.position += Vector3.forward;
                if (IsColliding()) currentBlock.transform.position += Vector3.back;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                currentBlock.transform.position += Vector3.back;
                if (IsColliding()) currentBlock.transform.position += Vector3.forward;
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                currentBlock.transform.Rotate(0, 90, 0);
                if (IsColliding()) currentBlock.transform.Rotate(0, -90, 0);
            }
        }
    }
}
