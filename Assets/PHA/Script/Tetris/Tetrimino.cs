using UnityEngine;

public class Tetrimino : MonoBehaviour
{
    public Vector3[] blockPositions;
    public StatusChange statusChange;

    // 머터리얼을 위한 필드 추가
    public Material statusAIncreaseMaterial;
    public Material statusADecreaseMaterial;
    public Material statusBIncreaseMaterial;
    public Material statusBDecreaseMaterial;
    public Material statusCIncreaseMaterial;
    public Material statusCDecreaseMaterial;

    private float fallTime = 1.0f;
    private float lockDelay = 1.0f;
    private float previousTime;
    private float lastMoveTime;
    private float previousYPosition;
    private bool isLocked = false;

    // Tetrimino.cs

    public GameObject shadowTetrimino;

    void Start()
    {
        CreateBlocks();
        CreateShadow();
        UpdateShadowPosition();

        previousTime = Time.time;
        lastMoveTime = Time.time;
        previousYPosition = transform.position.y;

        // 머터리얼 적용
        ApplyMaterial();
    }

    void Update()
    {
        if (isLocked) return;

        HandleInput();

        // 블럭 이동 및 회전 후 그림자 위치 갱신
        UpdateShadowPosition();

        // 아래쪽으로 자동으로 떨어지는 동작 처리
        if (Time.time - previousTime >= fallTime)
        {
            Move(Vector3.down);

            if (Mathf.Abs(transform.position.y - previousYPosition) > Mathf.Epsilon)
            {
                lastMoveTime = Time.time;
                previousYPosition = transform.position.y;
            }

            if (Time.time - lastMoveTime >= lockDelay)
            {
                LockTetrimino();
            }

            previousTime = Time.time;
        }
    }


    // Tetrimino.cs

    public void CreateShadow()
    {
        if (shadowTetrimino == null)
        {
            shadowTetrimino = new GameObject("ShadowTetrimino");
            foreach (Transform block in transform)
            {
                GameObject shadowBlock = Instantiate(block.gameObject, shadowTetrimino.transform);
                shadowBlock.GetComponent<Renderer>().material.color = Color.gray; // 그림자 색상 설정
                Destroy(shadowBlock.GetComponent<Collider>()); // 충돌 무시
            }
        }
        shadowTetrimino.SetActive(true); // 새 블럭 활성화 시 그림자 활성화
        UpdateShadowPosition();
    }

    public void DestroyShadow()
    {
        if (shadowTetrimino != null)
        {
            shadowTetrimino.SetActive(false); // 그림자를 삭제하지 않고 비활성화
        }
    }

    public void UpdateShadowPosition()
    {
        if (shadowTetrimino == null) return;

        shadowTetrimino.transform.position = transform.position;
        shadowTetrimino.transform.rotation = transform.rotation;

        while (IsValidShadowPosition())
        {
            shadowTetrimino.transform.position += Vector3.down;
        }
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



    void HandleInput()
    {
        bool inputReceived = false;

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Move(Vector3.left);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Move(Vector3.right);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Move(Vector3.forward);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Move(Vector3.back);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.S))
        {
            Move(Vector3.down);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.A))
        {
            Rotate(Vector3.right);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            Rotate(Vector3.forward);
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();
            inputReceived = true;
        }
        else if (Input.GetKeyDown(KeyCode.Space))
        {
            HardDrop();  // 블럭을 바로 바닥으로 떨어뜨림
            inputReceived = true;
        }

        if (inputReceived)
        {
            lastMoveTime = Time.time;
        }
    }

    // Tetrimino.cs

    public void MoveToValidPosition()
    {
        Vector3 adjustment = Vector3.zero;

        foreach (Transform child in transform)
        {
            Vector3 pos = Grid3D.Round(child.position);

            if (pos.x < 0) adjustment.x = Mathf.Max(adjustment.x, -pos.x);
            if (pos.x >= Grid3D.width) adjustment.x = Mathf.Min(adjustment.x, Grid3D.width - 1 - pos.x);
            if (pos.y < 0) adjustment.y = Mathf.Max(adjustment.y, -pos.y);
            if (pos.y >= Grid3D.height) adjustment.y = Mathf.Min(adjustment.y, Grid3D.height - 1 - pos.y);
            if (pos.z < 0) adjustment.z = Mathf.Max(adjustment.z, -pos.z);
            if (pos.z >= Grid3D.depth) adjustment.z = Mathf.Min(adjustment.z, Grid3D.depth - 1 - pos.z);
        }

        transform.position += adjustment;
    }

    void Move(Vector3 direction)
    {
        transform.position += direction;
        MoveToValidPosition();

        if (!IsValidPosition())
        {
            transform.position -= direction;
        }
    }

    void Rotate(Vector3 axis)
    {
        transform.Rotate(axis * 90);
        MoveToValidPosition();

        if (!IsValidPosition())
        {
            transform.Rotate(-axis * 90);
        }
    }

    void HardDrop()
    {
        while (IsValidPosition())
        {
            transform.position += Vector3.down;
        }
        transform.position -= Vector3.down;
        LockTetrimino();
    }

    bool IsValidPosition()
    {
        foreach (Transform child in transform)
        {
            Vector3 pos = Grid3D.Round(child.position);
            if (!Grid3D.InsideGrid(pos) || Grid3D.GetTransformAtGridPosition(pos) != null)
            {
                return false;
            }
        }
        return true;
    }

    void LockTetrimino()
    {
        isLocked = true;
        Grid3D.AddBlockToGrid(transform);
        GameManager.Instance.UpdateStatus(statusChange);
        Grid3D.DeleteFullLines();
        FindObjectOfType<GameManager>().OnBlockLanded();
        DestroyShadow();  // 블럭이 고정되면 그림자 제거
        enabled = false;
    }



    void OnDestroy()
    {
        if (isLocked)
        {
            GameManager.Instance.UpdateStatus(statusChange.Inverse());
        }
    }

    public void CreateBlocks()
    {
        // 각 블록의 위치에 큐브를 생성하여 모양을 구성
        foreach (Vector3 pos in blockPositions)
        {
            GameObject block = GameObject.CreatePrimitive(PrimitiveType.Cube);
            block.transform.position = transform.position + pos;
            block.transform.parent = this.transform;
        }
    }


    public void ApplyMaterial()
    {
        Material selectedMaterial = null;

        if (statusChange.statusChangeA > 0)
        {
            selectedMaterial = statusAIncreaseMaterial;
        }
        else if (statusChange.statusChangeA < 0)
        {
            selectedMaterial = statusADecreaseMaterial;
        }
        else if (statusChange.statusChangeB > 0)
        {
            selectedMaterial = statusBIncreaseMaterial;
        }
        else if (statusChange.statusChangeB < 0)
        {
            selectedMaterial = statusBDecreaseMaterial;
        }
        else if (statusChange.statusChangeC > 0)
        {
            selectedMaterial = statusCIncreaseMaterial;
        }
        else if (statusChange.statusChangeC < 0)
        {
            selectedMaterial = statusCDecreaseMaterial;
        }

        if (selectedMaterial != null)
        {
            foreach (Transform child in transform)
            {
                Renderer renderer = child.GetComponent<Renderer>();
                if (renderer != null)
                {
                    renderer.material = selectedMaterial;
                }
            }
        }
    }
}
