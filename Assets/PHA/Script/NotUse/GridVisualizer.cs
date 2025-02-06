
using UnityEngine;

public class GridVisualizer : MonoBehaviour
{
    public int gridWidth = 10;
    public int gridHeight = 20;
    public int gridDepth = 10;
    public GameObject gridCubePrefab;  // 미리 생성한 큐브 프리팹

    void Start()
    {
        CreateGrid();
    }

    void CreateGrid()
    {
        for (int x = 0; x < gridWidth; x++)
        {
            for (int y = 0; y < gridHeight; y++)
            {
                for (int z = 0; z < gridDepth; z++)
                {
                    Vector3 position = new Vector3(x, y, z);
                    GameObject gridCube = Instantiate(gridCubePrefab, position, Quaternion.identity);
                    gridCube.transform.parent = this.transform;  // 부모 오브젝트 설정
                }
            }
        }
    }
}
