using UnityEngine;

public class Grid3D : MonoBehaviour
{
    public static int width = 5;
    public static int height = 20;
    public static int depth = 5;

    public static Transform[,,] grid = new Transform[width, height, depth];

    public static Vector3 Round(Vector3 pos)
    {
        return new Vector3(Mathf.Round(pos.x), Mathf.Round(pos.y), Mathf.Round(pos.z));
    }

    public static bool InsideGrid(Vector3 pos)
    {
        return ((int)pos.x >= 0 && (int)pos.x < width &&
                (int)pos.y >= 0 && (int)pos.y < height &&
                (int)pos.z >= 0 && (int)pos.z < depth);
    }


    public static Transform GetTransformAtGridPosition(Vector3 pos)
    {
        if (pos.y >= height)
            return null;

        return grid[(int)pos.x, (int)pos.y, (int)pos.z];
    }

    public static void AddBlockToGrid(Transform block)
    {
        foreach (Transform child in block)
        {
            Vector3 pos = Round(child.position);
            grid[(int)pos.x, (int)pos.y, (int)pos.z] = child;
        }
    }

    public static bool IsLineFull(int y)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                if (grid[x, y, z] == null)
                    return false;
            }
        }
        return true;
    }

    public static void DeleteLine(int y)
    {
        UI_Score scoreManager = GameObject.FindObjectOfType<UI_Score>(); // UI_Score 인스턴스 찾기

        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                Transform block = grid[x, y, z];
                if (block != null)
                {
                    // 블록 개별 삭제
                    Destroy(block.gameObject);
                    grid[x, y, z] = null;

                    // 부모 Tetrimino 확인 후 삭제 처리
                    Tetrimino parentTetrimino = block.GetComponentInParent<Tetrimino>();
                    if (parentTetrimino != null)
                    {
                        parentTetrimino.DestroyIfNoChildren();
                    }
                }
            }
        }

        // 점수 추가
        if (scoreManager != null)
        {
            scoreManager.AddScore(20); // 예제 점수 (100점 증가)
        }
    }


    public static void MoveAllBlocksDown(int y)
    {
        for (int i = y; i < height - 1; i++)
        {
            MoveLineDown(i);
        }
    }

    public static void MoveLineDown(int y)
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < depth; z++)
            {
                if (grid[x, y, z] != null)
                {
                    grid[x, y - 1, z] = grid[x, y, z];
                    grid[x, y, z] = null;
                    grid[x, y - 1, z].position += Vector3.down;
                }
            }
        }
    }

    public static void DeleteFullLines()
    {
        for (int y = 0; y < height; y++)
        {
            if (IsLineFull(y))
            {
                DeleteLine(y);
                MoveAllBlocksDown(y);
                y--;
            }
        }
    }

    public static int GridHeightLine()
    {
        int HightLine = 0;
        
        foreach (Transform H_grid in grid)
        {
            if(H_grid != null)
            {
                if (H_grid.position.y >= HightLine)
                    HightLine = (int)H_grid.position.y;
            }
        }
        return HightLine;
    }


}
