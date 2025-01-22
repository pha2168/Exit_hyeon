using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public GameObject tetriminoPrefab;
    public GameObject nextTetriminoPrefab;  // NextBlock 미리보기를 위한 프리팹
    public Transform nextBlockDisplayPosition;  // NextBlock UI 위치

    public Slider statusASlider;
    public Slider statusBSlider;
    public Slider statusCSlider;

    //public Slider status1Slider;
    //public Slider status2Slider;
    //public Slider status3Slider;

    public int statusA;
    public int statusB;
    public int statusC;

    public GameObject gameOverUI;

    private bool isGameOver = false;
    private Tetrimino currentTetrimino;  // 현재 블록
    private Tetrimino nextTetrimino;     // 다음 블록

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

    void Start()
    {
        // NextBlock 초기화 및 첫 블록 스폰
        nextTetrimino = SpawnNextTetrimino();
        SpawnTetrimino();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            SwapWithNextBlock();  // C키 입력 시 블록 교체
        }

        //// TriggerGameOver 호출 조건
        //if (status1Slider.value <= 45 || status2Slider.value <= 45 || status3Slider.value <= 45)
        //{
        //    TriggerGameOver();
        //}
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

        if (IsPositionOccupied(spawnPosition, nextTetrimino))
        {
            TriggerGameOver();
            return;
        }

        currentTetrimino = nextTetrimino;
        currentTetrimino.transform.position = spawnPosition;
        currentTetrimino.enabled = true;

        // 새로 소환된 블럭에 대한 새로운 그림자 생성
        currentTetrimino.CreateShadow();
        currentTetrimino.UpdateShadowPosition();

        nextTetrimino = SpawnNextTetrimino();
    }


    private bool IsPositionOccupied(Vector3 position, Tetrimino tetrimino)
    {
        // tetrimino가 null이 아닐 경우에만 자식 위치를 확인
        if (tetrimino == null) return false;

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


    public Tetrimino SpawnNextTetrimino()
    {
        // NextBlock 위치에 생성
        Vector3 spawnPosition = nextBlockDisplayPosition.position;
        GameObject nextTetriminoObject = Instantiate(tetriminoPrefab, spawnPosition, Quaternion.identity);
        Tetrimino tetriminoComponent = nextTetriminoObject.GetComponent<Tetrimino>();

        // 블럭 모양을 무작위로 선택
        Vector3[] shape = GetRandomShape(out StatusChange statusChange);
        tetriminoComponent.blockPositions = shape;
        tetriminoComponent.statusChange = statusChange;

        // 블록을 구성하는 CreateBlocks() 호출하여 모양을 그리기
        tetriminoComponent.CreateBlocks();

        tetriminoComponent.ApplyMaterial();  // 추가된 부분: 머터리얼을 적용

        // NextBlock은 움직이지 않도록 비활성화
        tetriminoComponent.enabled = false;

        return tetriminoComponent;
    }
    // GameManager.cs


    public void SwapWithNextBlock()
    {
        if (currentTetrimino == null || nextTetrimino == null) return;

        currentTetrimino.DestroyShadow(); // 현재 블럭의 그림자 비활성화

        Vector3 currentPosition = currentTetrimino.transform.position;
        Vector3 nextBlockPosition = nextBlockDisplayPosition.position;

        currentTetrimino.transform.position = nextBlockPosition;
        nextTetrimino.transform.position = currentPosition;

        Tetrimino temp = currentTetrimino;
        currentTetrimino = nextTetrimino;
        nextTetrimino = temp;

        // 새로운 currentTetrimino에 대해 그림자 재생성/활성화
        if (currentTetrimino.shadowTetrimino == null)
        {
            currentTetrimino.CreateShadow();
        }
        else
        {
            currentTetrimino.shadowTetrimino.SetActive(true);
        }

        currentTetrimino.UpdateShadowPosition();
        currentTetrimino.enabled = true;
        nextTetrimino.enabled = false;
    }


    Vector3[] GetRandomShape(out StatusChange statusChange)
    {
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

    public void OnBlockLanded()
    {
        SpawnTetrimino();
    }


    public void UpdateStatus(StatusChange statusChange)
    {
        // 스테이터스 변경 로직
        statusA += statusChange.statusChangeA;
        statusB += statusChange.statusChangeB;
        statusC += statusChange.statusChangeC;

        // UI 슬라이더 업데이트
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
}
