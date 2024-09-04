using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject tetriminoPrefab;

    // UI 슬라이더를 통해 스테이더스를 보여주기 위한 필드
    public Slider statusASlider;
    public Slider statusBSlider;
    public Slider statusCSlider;

    // 스테이더스 값 저장
    public int statusA;
    public int statusB;
    public int statusC;

    void Awake()
    {
        // 싱글톤 패턴 구현
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

    void Start()
    {
        // 게임 시작 시 첫 테트리미노 소환
        SpawnTetrimino();
    }

    public void SpawnTetrimino()
    {
        Vector3 spawnPosition = new Vector3(2, 10, 2);
        GameObject tetrimino = Instantiate(tetriminoPrefab, spawnPosition, Quaternion.identity);
        Tetrimino tetriminoComponent = tetrimino.GetComponent<Tetrimino>();

        // 블럭 모양을 무작위로 선택하고, 스테이더스 변화를 무작위로 설정
        Vector3[] shape = GetRandomShape(out StatusChange statusChange);
        tetriminoComponent.blockPositions = shape;
        tetriminoComponent.statusChange = statusChange;

        // 블럭에 머터리얼 적용
        tetriminoComponent.ApplyMaterial();
    }

    Vector3[] GetRandomShape(out StatusChange statusChange)
    {
        // 무작위로 모양 선택
        int shapeIndex = Random.Range(0, 7);
        switch (shapeIndex)
        {
            case 0: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.IShape;
            case 1: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.OShape;
            case 2: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.TShape;
            case 3: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.LShape;
            case 4: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.JShape;
            case 5: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.SShape;
            case 6: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.ZShape;
            default: statusChange = TetriminoShapes.GetRandomStatusChange(); return TetriminoShapes.IShape;
        }
    }

    public void UpdateStatus(StatusChange statusChange)
    {
        statusA += statusChange.statusChangeA;
        statusB += statusChange.statusChangeB;
        statusC += statusChange.statusChangeC;

        // 스테이더스가 변경되었을 때 UI 업데이트
        UpdateStatusUI();
    }

    void UpdateStatusUI()
    {
        if (statusASlider != null)
        {
            statusASlider.value = statusA;
        }
        if (statusBSlider != null)
        {
            statusBSlider.value = statusB;
        }
        if (statusCSlider != null)
        {
            statusCSlider.value = statusC;
        }
    }

    public void OnBlockLanded()
    {
        // 블럭이 착지했을 때 호출되어 새로운 블럭을 소환합니다.
        SpawnTetrimino();
    }
}
