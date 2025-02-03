using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tetris_TetriminoShadow : MonoBehaviour
{
    public GameObject shadowTetrimino;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(shadowTetrimino != null)
        {
            UpdateShadowPosition();
        }
    }

    public void CreateShadow()
    {
        if (shadowTetrimino == null)
        {
            shadowTetrimino = new GameObject("ShadowTetrimino");
            foreach (Transform block in transform)
            {
                GameObject shadowBlock = Instantiate(block.gameObject, shadowTetrimino.transform);
                shadowBlock.GetComponent<Renderer>().material.color = Color.gray;
                Destroy(shadowBlock.GetComponent<Collider>());
            }
        }
        shadowTetrimino.SetActive(true);
        UpdateShadowPosition();
    }

    public void DestroyShadow()
    {
        if (shadowTetrimino != null)
        {
            Destroy(shadowTetrimino);
        }
    }

    public void UpdateShadowPosition()
    {
        if (shadowTetrimino == null) return;

        shadowTetrimino.transform.position = transform.position;
        shadowTetrimino.transform.rotation = transform.rotation;

        int maxDropDepth = Grid3D.height; // 최대 떨어질 거리 설정
        int dropCount = 0;

        while (IsValidShadowPosition() && dropCount < maxDropDepth)
        {
            shadowTetrimino.transform.position += Vector3.down;
            dropCount++;
        }

        // 한 칸 위로 보정 (마지막 이동이 유효하지 않았을 경우)
        shadowTetrimino.transform.position -= Vector3.down;
    }


    bool IsValidShadowPosition()
    {
        foreach (Transform shadowBlock in shadowTetrimino.transform)
        {
            Vector3 pos = Grid3D.Round(shadowBlock.position);
            if (!Grid3D.InsideGrid(pos) || Grid3D.GetTransformAtGridPosition(pos) != null)
            {
                return false;
            }
        }
        return true;
    }
}
