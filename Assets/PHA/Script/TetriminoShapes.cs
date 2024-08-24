using UnityEngine;

public class TetriminoShapes : MonoBehaviour
{
    // 각 테트리미노의 모양을 정의
    public static Vector3[] IShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(2, 0, 0), new Vector3(3, 0, 0)
    };

    public static Vector3[] OShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)
    };

    public static Vector3[] TShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0)
    };

    public static Vector3[] LShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(2, 0, 0), new Vector3(0, 1, 0)
    };

    public static Vector3[] JShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(-2, 0, 0), new Vector3(0, 1, 0)
    };

    public static Vector3[] SShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(1, 0, 0), new Vector3(0, 1, 0), new Vector3(-1, 1, 0)
    };

    public static Vector3[] ZShape = new Vector3[] {
        new Vector3(0, 0, 0), new Vector3(-1, 0, 0), new Vector3(0, 1, 0), new Vector3(1, 1, 0)
    };
}
