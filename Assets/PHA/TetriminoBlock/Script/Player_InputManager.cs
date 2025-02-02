using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InputManager : MonoBehaviour
{
    public static Player_InputManager Instance { get; private set; }
    public Tetris_Tetrimino tetriminoObjects;

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
        // GameManager에서 소환될 때 이동시킬 테트리미노 블럭을 전달
        // TetriminoLock 될 때 다음 블럭으로 전달
    }

    // Update is called once per frame
    void Update()
    {
        bool inputReceived = false;

        if (Input.GetKeyDown(KeyCode.UpArrow)) 
        {
            tetriminoObjects.Move(Vector3.forward);
            inputReceived = true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            tetriminoObjects.Move(Vector3.back);
            inputReceived = true;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            tetriminoObjects.Move(Vector3.right);
            inputReceived = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            tetriminoObjects.Move(Vector3.left);
            inputReceived = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            tetriminoObjects.HardDrop();
            inputReceived = true;
        }
        if (Input.GetKeyDown(KeyCode.A))
        {
            tetriminoObjects.Rotate(Vector3.right);
            inputReceived = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            tetriminoObjects.Move(Vector3.down);
            inputReceived = true;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            tetriminoObjects.Rotate(Vector3.forward);
            inputReceived = true;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //다음 블럭과 교체 로직
        }

        if (inputReceived)
        {
            tetriminoObjects.setLastMoveTime();
        }
    }
}
