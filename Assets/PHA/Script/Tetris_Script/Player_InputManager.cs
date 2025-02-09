using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player_InputManager : MonoBehaviour
{
    public static Player_InputManager Instance { get; private set; }
    private Tetris_Tetrimino tetriminoObjects;
    public Manager_Tetris tetrisManager;
    public UI_Score uI_Score;

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
        tetriminoObjects = tetrisManager.currentTetrimino;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            uI_Score.AddScore(10);
        }

        if (!tetrisManager.isGameOver)
        {
            bool inputReceived = false;
            tetriminoObjects = tetrisManager.currentTetrimino;

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
                tetrisManager.SwapWithNextBlock();
            }

            if (inputReceived)
            {
                tetriminoObjects.setLastMoveTime();
            }
        }
    }
}
