using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // 직렬화

public class Data
{
    public int  NowDay;

    // 새로 추가된 데이터: 볼륨 조절값 (0.0f ~ 1.0f)
    public float BGMVolume = 1.0f; // 배경음 볼륨
    public float SFXVolume = 1.0f; // 효과음 볼륨

    //저장할 데이터들 값 넣기
}
