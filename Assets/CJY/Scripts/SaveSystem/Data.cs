using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable] // 직렬화

public class Data
{
    public int  NowDay; //현재 일차

    public int PublicAuthority_Step; //공권력 퀘스트 단계
    public int RevolutionaryArmy_Step; // 혁명군 퀘스트 단계
    public int Cult_Step; //사이비 퀘스트 단계
    public int CrimeSyndicate_Step; //범죄 퀘스트 단계

    //저장할 데이터들 값 넣기
}
