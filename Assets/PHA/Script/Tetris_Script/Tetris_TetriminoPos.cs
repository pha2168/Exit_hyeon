using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using UnityEngine;

public class Tetris_TetriminoPos : MonoBehaviour
{
    public Vector3[] blockPositions;

    public GameObject StoreBlock;
    public GameObject HospitalBlock;
    public GameObject CrimeBlock;
    public GameObject CleanHouseBlock;
    public GameObject TrashHouseBlock;
    public GameObject WeaponStoreBlock;

    private float previousTime;
    public float lastMoveTime;
    private float previousYPosition;

    public Tetris_Tetrimino tetrinimo;

    // Start is called before the first frame update
    void Start()
    {
        previousTime = Time.time;
        lastMoveTime = Time.time;
        previousYPosition = transform.position.y;

        //tetrinimo = GetComponent<Tetris_Tetrimino>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setBlockPositions(Vector3[] pos)
    {
        blockPositions = pos;
    }

    public void CreateBlocks()
    {
        // 자식 블록이 이미 생성되어 있으면 추가 생성 방지
        if (transform.childCount > 0) return;


        GameObject blockPrefab = SelectBlock();
        // 각 블록의 위치에 큐브를 생성하여 모양을 구성
        foreach (Vector3 pos in blockPositions)
        {
            GameObject block = Instantiate(blockPrefab, transform.position + pos, Quaternion.identity);
            block.transform.parent = this.transform;

        }
    }

    GameObject SelectBlock()
    {
        int randomBlock = Random.Range(0, 6);
        switch (randomBlock)
        {
            case 0:
                return StoreBlock;
            case 1:
                return HospitalBlock;
            case 2:
                return CrimeBlock;
            case 3:
                return CleanHouseBlock;
            case 4:
                return TrashHouseBlock;
            case 5:
                return WeaponStoreBlock;
            default:
                return null;
        }
    }


    public bool IsValidPosition()
    {
        foreach (Transform child in transform)
        {
            Vector3 pos = Grid3D.Round(child.position);
            if (!Grid3D.InsideGrid(pos) || Grid3D.GetTransformAtGridPosition(pos) != null)
            {
                return false;
            }
        }
        return true;
    }

    public void MoveToValidPosition()
    {
        Vector3 adjustment = Vector3.zero;

        foreach (Transform child in transform)
        {
            Vector3 pos = Grid3D.Round(child.position);

            if (pos.x < 0) adjustment.x = Mathf.Max(adjustment.x, -pos.x);
            if (pos.x >= Grid3D.width) adjustment.x = Mathf.Min(adjustment.x, Grid3D.width - 1 - pos.x);
            if (pos.y < 0) adjustment.y = Mathf.Max(adjustment.y, -pos.y);
            if (pos.y >= Grid3D.height) adjustment.y = Mathf.Min(adjustment.y, Grid3D.height - 1 - pos.y);
            if (pos.z < 0) adjustment.z = Mathf.Max(adjustment.z, -pos.z);
            if (pos.z >= Grid3D.depth) adjustment.z = Mathf.Min(adjustment.z, Grid3D.depth - 1 - pos.z);
        }

        transform.position += adjustment;
    }

    public void TetriminoDown()
    {
        if (Time.time - previousTime >= tetrinimo.fallTime)
        {
            tetrinimo.Move(Vector3.down);

            if (Mathf.Abs(transform.position.y - previousYPosition) > Mathf.Epsilon)
            {
                lastMoveTime = Time.time;
                previousYPosition = transform.position.y;
            }

            if (Time.time - lastMoveTime >= tetrinimo.lockDelay)
            {
                tetrinimo.LockTetrimino();
            }

            previousTime = Time.time;
        }
    }
}
