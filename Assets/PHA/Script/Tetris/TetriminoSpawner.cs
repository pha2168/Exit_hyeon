using UnityEngine;

public class TetriminoSpawner : MonoBehaviour
{
    public GameObject tetriminoPrefab;

    void Start()
    {
        SpawnTetrimino();
    }

    public void SpawnTetrimino()
    {
        Vector3 spawnPosition = new Vector3(0, 19, 0);
        GameObject tetrimino = Instantiate(tetriminoPrefab, spawnPosition, Quaternion.identity);
        Tetrimino tetriminoComponent = tetrimino.GetComponent<Tetrimino>();

        Vector3[] shape = GetRandomShape();
        tetriminoComponent.blockPositions = shape;
    }

    Vector3[] GetRandomShape()
    {
        int shapeIndex = Random.Range(0, 7);
        switch (shapeIndex)
        {
            case 0: return TetriminoShapes.IShape;
            case 1: return TetriminoShapes.OShape;
            case 2: return TetriminoShapes.TShape;
            case 3: return TetriminoShapes.LShape;
            case 4: return TetriminoShapes.JShape;
            case 5: return TetriminoShapes.SShape;
            case 6: return TetriminoShapes.ZShape;
            default: return TetriminoShapes.IShape;
        }
    }
}
