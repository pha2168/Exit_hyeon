using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tetris_Tetrimino : MonoBehaviour
{
    public float fallTime = 1.0f;
    public float lockDelay = 1.0f;

    private bool isLocked = false;

    private Tetris_TetriminoPos tetrimino_pos;
    private Tetris_TetriminoShadow tetrimino_sha;

    // Start is called before the first frame update
    void Start()
    {
        tetrimino_pos = GetComponent<Tetris_TetriminoPos>();
        tetrimino_sha = GetComponent<Tetris_TetriminoShadow>();

        tetrimino_pos.CreateBlocks();
        tetrimino_sha.CreateShadow();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLocked) return;

        tetrimino_pos.TetriminoDown();
        tetrimino_sha.UpdateShadowPosition();
    }

    public void Move(Vector3 direction)
    {
        transform.position += direction;
        tetrimino_pos.MoveToValidPosition();

        if (!tetrimino_pos.IsValidPosition())
        {
            transform.position -= direction;
        }
    }

    public void Rotate(Vector3 axis)
    {
        transform.Rotate(axis * 90);
        tetrimino_pos.MoveToValidPosition();

        if (!tetrimino_pos.IsValidPosition())
        {
            transform.Rotate(-axis * 90);
        }
    }

    public void HardDrop()
    {
        while (tetrimino_pos.IsValidPosition())
        {
            transform.position += Vector3.down;
        }
        transform.position -= Vector3.down;
        LockTetrimino();
    }

    public void LockTetrimino()
    {
        isLocked = true;
        Grid3D.AddBlockToGrid(transform);
        Grid3D.DeleteFullLines();
        FindObjectOfType<GameManager>().OnBlockLanded();
        tetrimino_sha.DestroyShadow();
        enabled = false;
    }

    public void DestroyIfNoChildren()
    {
        // 자식이 하나도 없으면 자신을 삭제
        if (transform.childCount == 0)
        {
            Destroy(gameObject);
        }
    }

    public void setLastMoveTime()
    {
        tetrimino_pos.lastMoveTime = Time.time;
    }

    public void setBlockPos(Vector3[] pos)
    {
        tetrimino_pos.blockPositions = pos;
    }

    public void createBlock()
    {
        tetrimino_pos.CreateBlocks();
    }
}
