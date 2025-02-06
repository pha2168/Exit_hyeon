using UnityEngine;

public class StatusChange
{
    public int statusChangeA;
    public int statusChangeB;
    public int statusChangeC;

    public StatusChange(int a, int b, int c)
    {
        statusChangeA = a;
        statusChangeB = b;
        statusChangeC = c;
    }

    // Inverse 메서드 추가: 값들을 반대로 바꿉니다.
    public StatusChange Inverse()
    {
        return new StatusChange(-statusChangeA, -statusChangeB, -statusChangeC);
    }
}