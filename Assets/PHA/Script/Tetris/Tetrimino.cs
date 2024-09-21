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

    void Start()
    {
        CreateBlocks();
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

    void Move(Vector3 direction)
    {
        transform.position += direction;

        if (!IsValidPosition())
        {
            transform.position -= direction;
        }
    }

    void Rotate(Vector3 axis)
    {
        transform.Rotate(axis * 90);

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
        enabled = false;
    }

    void OnDestroy()
    {
        if (isLocked)
        {
            GameManager.Instance.UpdateStatus(statusChange.Inverse());
        }
    }

    void CreateBlocks()
    {
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
