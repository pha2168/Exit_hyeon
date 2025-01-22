using UnityEngine;

[CreateAssetMenu(fileName = "Day Data", menuName = "Scriptable Object/Day Data", order = int.MaxValue)]
public class DayData : ScriptableObject
{
    [SerializeField]
    private int dayNum;
    public int DayNum { get { return dayNum; } }
    [SerializeField]
    private float dropSpeed;
    public float DropSpeed { get { return dropSpeed; } }
    [SerializeField]
    private int scoreMax;
    public int ScoreMax { get { return scoreMax; } }

}