using UnityEngine;

public class TetriminoShapes : MonoBehaviour
{
    public UIManager uiManager;

    void Start()
    {
        // 랜덤 StatusChange 생성 후 UI 업데이트
        StatusChange randomChange = GetRandomStatusChange();
        uiManager.ApplyStatusChange(randomChange);
    }

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

    public static StatusChange GetRandomStatusChange()
    {
        int randomStatus = Random.Range(0, 3); // 0: A, 1: B, 2: C
        int changeValue = Random.Range(0, 2) == 0 ? -1 : 1; // -1 또는 +1

        switch (randomStatus)
        {
            case 0:
                return new StatusChange(changeValue, 0, 0); // A에 영향
            case 1:
                return new StatusChange(0, changeValue, 0); // B에 영향
            case 2:
                return new StatusChange(0, 0, changeValue); // C에 영향
            default:
                return new StatusChange(0, 0, 0); // 이 기본값은 절대 선택되지 않음
        }
    }
}