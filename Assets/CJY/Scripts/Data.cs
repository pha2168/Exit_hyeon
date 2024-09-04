using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // 직렬화

public class Data
{
    // 예시
    // 각 챕터의 잠금여부를 저장할 배열
    public bool[] isUnlock = new bool[5];

    //저장할 데이터들 값 넣기
    //일차수는 인트 값으로 저장 일차에 따라 레벨 조정
}
