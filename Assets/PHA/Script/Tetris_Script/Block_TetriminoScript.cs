using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block_TetriminoScript : MonoBehaviour
{
    public string GetTag()
    {
        return this.tag;
    }

    Block_TetriminoScript() 
    {
        // 스테이더스 값 증가 로직
    }

    ~Block_TetriminoScript() 
    {
        //스테이더스 값 감소 로직
    }
}
