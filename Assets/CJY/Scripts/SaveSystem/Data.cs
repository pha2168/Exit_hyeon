using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // 직렬화

public class Data
{
    public int  NowDay = 1;

    public int PublicAuthority_Step = 1;
    public int RevolutionaryArmy_Step = 1;
    public int Cult_Step = 1;
    public int CrimeSyndicate_Step = 1;

    public int Public_s = 0;
    public int Revolution_s = 0;
    public int Crime_s = 0;
    public int Cult_s = 0;


    //저장할 데이터들 값 넣기
}
