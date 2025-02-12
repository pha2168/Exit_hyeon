using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class Manager_Tetris : MonoBehaviour
{
    //public static Manager_Tetris Instance { get; private set; }

    public GameObject tetriminoPrefab;
    public GameObject nextTetriminoPrefab;
    public Transform nextBlockPos;

    public GameObject gameOverUI;

    public bool isGameOver = false;
    public Tetris_Tetrimino currentTetrimino;
    private Tetris_Tetrimino nextTetrimino;

    void Awake()
    {
        //if (Instance == null)
        //{
        //    Instance = this;
        //    DontDestroyOnLoad(gameObject);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}
    }

    void Start()
    {
        nextTetrimino = SpawnNextTetrimino();

        SpawnTetrimino();
    }

    public void OnBlockLanded()
    {
        SpawnTetrimino();
    }

    public void TriggerGameOver()
    {
        isGameOver = true;
        gameOverUI.SetActive(true);
    }

    public void SpawnTetrimino()
    {
        if (isGameOver) return;

        Vector3 spawnPosition = new Vector3(2, 16, 2);

        if (IsPositionOccupied(spawnPosition, nextTetrimino))
        {
            TriggerGameOver();
            return;
        }

        currentTetrimino = nextTetrimino;
        currentTetrimino.transform.position = spawnPosition;
        currentTetrimino.enabled = true;

        Tetris_TetriminoShadow nextShadow = currentTetrimino.GetComponent<Tetris_TetriminoShadow>();
        if (nextShadow != null)
        {
            nextShadow.CreateShadow();
        }

        nextTetrimino = SpawnNextTetrimino();
    }

    private bool IsPositionOccupied(Vector3 spawnPosition, Tetris_Tetrimino tetrimino)
    {
        if (tetrimino == null) return false;

        foreach (Transform child in tetrimino.transform)
        {
            Vector3 pos = Grid3D.Round(spawnPosition + child.localPosition);
            if (!Grid3D.InsideGrid(pos) || Grid3D.GetTransformAtGridPosition(pos) != null)
            {
                return true;
            }
        }
        return false;
    }

    public Tetris_Tetrimino SpawnNextTetrimino()
    {
        Vector3 spawnPosition = nextBlockPos.position;
        GameObject nextTetriminoObject = Instantiate(tetriminoPrefab, spawnPosition, Quaternion.identity);
        Tetris_Tetrimino tetriminoComponent = nextTetriminoObject.GetComponent<Tetris_Tetrimino>();

        Vector3[] shape = GetRandomShape();
        tetriminoComponent.setBlockPos(shape);

        tetriminoComponent.createBlock();

        tetriminoComponent.enabled = false;

        return tetriminoComponent;

    }

    public void SwapWithNextBlock()
    {
        if (currentTetrimino == null || nextTetrimino == null) return;

        // 현재 블록의 그림자 제거
        Tetris_TetriminoShadow currentShadow = currentTetrimino.GetComponent<Tetris_TetriminoShadow>();
        if (currentShadow != null)
        {
            currentShadow.DestroyShadow();
        }

        // 위치 변경
        Vector3 currentPosition = currentTetrimino.transform.position;
        Vector3 nextBlockPosition = nextBlockPos.position;

        currentTetrimino.transform.position = nextBlockPosition;
        nextTetrimino.transform.position = currentPosition;

        // 테트리미노 교체
        Tetris_Tetrimino temp = currentTetrimino;
        currentTetrimino = nextTetrimino;
        nextTetrimino = temp;

        // 현재 블록 활성화 & 다음 블록 비활성화
        currentTetrimino.enabled = true;
        nextTetrimino.enabled = false;

        // 새로운 블록의 그림자 갱신
        Tetris_TetriminoShadow nextShadow = currentTetrimino.GetComponent<Tetris_TetriminoShadow>();
        if (nextShadow != null)
        {
            nextShadow.CreateShadow();
        }
    }

    Vector3[] GetRandomShape()
    {
        int shapeIndex = UnityEngine.Random.Range(0, 7);
        Vector3[] selectedShape;

        switch (shapeIndex)
        {
            case 0: selectedShape = Tetris_TetriminoShapes.IShape; break;
            case 1: selectedShape = Tetris_TetriminoShapes.OShape; break;
            case 2: selectedShape = Tetris_TetriminoShapes.TShape; break;
            case 3: selectedShape = Tetris_TetriminoShapes.LShape; break;
            case 4: selectedShape = Tetris_TetriminoShapes.JShape; break;
            case 5: selectedShape = Tetris_TetriminoShapes.SShape; break;
            case 6: selectedShape = Tetris_TetriminoShapes.ZShape; break;
            default: selectedShape = Tetris_TetriminoShapes.IShape; break;
        }

        return selectedShape;
    }
}
