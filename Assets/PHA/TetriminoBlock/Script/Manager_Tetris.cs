using System;
using System.Collections.Specialized;
using System.Security.Cryptography;
using UnityEngine;

public class Manager_Tetris : MonoBehaviour
{
    public static Manager_Tetris Instance { get; private set; }

    public GameObject tetriminoPrefab;
    public GameObject nextTetriminoPrefab;
    public Transform nextBlockPos;

    public GameObject gameOverUI;

    private bool isGameOver = false;
    public Tetris_Tetrimino currentTetrimino;
    private Tetris_Tetrimino nextTetrimino;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nextTetrimino = SpawnNextTetrimino();

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

        Vector3 spawnPosition = new Vector3(2, 18, 2);

        if(IsPositionOccupied(nextTetrimino))
        {
            TriggerGameOver();
            return;
        }

        currentTetrimino = nextTetrimino;
        currentTetrimino.transform.position = spawnPosition;
        currentTetrimino.enabled = true;

        nextTetrimino = SpawnNextTetrimino();
    }

    private bool IsPositionOccupied(Tetris_Tetrimino tetrimino)
    {
        if (tetrimino == null) return false;

        Vector3 position = new Vector3(2, 20, 2);

        foreach (Transform child in tetrimino.transform)
        {
            Vector3 pos = Grid3D.Round(position + child.localPosition);
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

        Vector3 currentPosition = currentTetrimino.transform.position;
        Vector3 nextBlockPosition = nextBlockPos.position;

        currentTetrimino.transform.position = nextBlockPosition;
        nextTetrimino.transform.position = currentPosition;

        Tetris_Tetrimino temp = currentTetrimino;
        currentTetrimino = nextTetrimino;
        nextTetrimino = temp;

        currentTetrimino.enabled = true;
        nextTetrimino.enabled = false;
    }

    Vector3[] GetRandomShape()
    {
        int shapeIndex = UnityEngine.Random.Range(0, 7);
        switch (shapeIndex)
        {
            case 0: return Tetris_TetriminoShapes.IShape;
            case 1: return Tetris_TetriminoShapes.OShape;
            case 2: return Tetris_TetriminoShapes.TShape;
            case 3: return Tetris_TetriminoShapes.LShape;
            case 4: return Tetris_TetriminoShapes.JShape;
            case 5: return Tetris_TetriminoShapes.SShape;
            case 6: return Tetris_TetriminoShapes.ZShape;
            default: return Tetris_TetriminoShapes.IShape;
        }
    }
}
