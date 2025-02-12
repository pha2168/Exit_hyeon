using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraChanage : MonoBehaviour
{
    public Camera camera1;
    public Camera camera2;
    private bool isCamera1Active = true;

    void Start()
    {
        // 초기 설정: camera1 활성화, camera2 비활성화
        camera1.enabled = true;
        camera2.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0)) // 0번 키 입력
        {
            isCamera1Active = !isCamera1Active;
            camera1.enabled = isCamera1Active;
            camera2.enabled = !isCamera1Active;
        }
    }
}
