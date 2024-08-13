using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrisTower : MonoBehaviour
{
    public int Width = 4;
    public int Height = 20;
    public int Depth = 4;  // Z 축의 깊이 추가
    public float DropSpeed = 1.0f;
    public float MoveInterval = 1.0f;  // 이동 시 한 번에 이동할 거리 (블록 크기)

    private GameObject currentBlock;
    private BlockSpawner blockSpawner;

    void Start()
    {
        blockSpawner = FindObjectOfType<BlockSpawner>();
        SpawnNewBlock();
    }

    void Update()
    {
        HandleMovementInput();
        HandleRotationInput();

        if (currentBlock != null)
        {
            currentBlock.transform.position += Vector3.down * DropSpeed * Time.deltaTime;

            if (CheckCollision())
            {
                currentBlock.transform.position -= Vector3.down * DropSpeed * Time.deltaTime; // 충돌 시 위치 보정
                currentBlock = null; // 현재 블록을 고정하고 다음 블록 준비
                SpawnNewBlock();
            }
        }
    }

    void HandleMovementInput()
    {
        if (currentBlock == null) return;

        Vector3 moveDirection = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            moveDirection = Vector3.left * MoveInterval;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            moveDirection = Vector3.right * MoveInterval;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            moveDirection = Vector3.forward * MoveInterval;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            moveDirection = Vector3.back * MoveInterval;
        }

        Vector3 newPosition = currentBlock.transform.position + moveDirection;

        if (IsValidMove(newPosition))
        {
            currentBlock.transform.position = newPosition;
        }
    }

    void HandleRotationInput()
    {
        if (currentBlock == null) return;

        Vector3 rotationAxis = Vector3.zero;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            rotationAxis = Vector3.up;
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            rotationAxis = Vector3.down;
        }
        else if (Input.GetKeyDown(KeyCode.E))
        {
            rotationAxis = Vector3.left;
        }
        else if (Input.GetKeyDown(KeyCode.R))
        {
            rotationAxis = Vector3.right;
        }
        else if (Input.GetKeyDown(KeyCode.T))
        {
            rotationAxis = Vector3.forward;
        }
        else if (Input.GetKeyDown(KeyCode.Y))
        {
            rotationAxis = Vector3.back;
        }

        if (rotationAxis != Vector3.zero)
        {
            currentBlock.transform.Rotate(rotationAxis, 90, Space.World);

            if (!IsValidMove(currentBlock.transform.position))
            {
                currentBlock.transform.Rotate(rotationAxis, -90, Space.World); // 회전 되돌리기
            }
        }
    }

    void SpawnNewBlock()
    {
        if (blockSpawner != null)
        {
            currentBlock = blockSpawner.SpawnBlock();
            currentBlock.transform.position = new Vector3(Width / 2, Height, Depth / 2); // 타워의 중심에서 소환
        }
    }

    bool IsValidMove(Vector3 position)
    {
        if (position.x < 0 || position.x >= Width ||
            position.z < 0 || position.z >= Depth || // Z 축의 경계 추가
            position.y < 0)
        {
            return false;
        }

        // 이 부분에 다른 블록과의 충돌 검사를 추가할 수 있습니다.

        return true;
    }

    bool CheckCollision()
    {
        if (currentBlock.transform.position.y <= 0)
        {
            return true;
        }

        // 다른 블록과의 충돌 여부는 여기서 처리할 수 있습니다.

        return false;
    }
}
