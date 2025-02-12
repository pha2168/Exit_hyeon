using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story Log", menuName = "Scriptable Object/Story Log", order = int.MaxValue)]
public class StoryLog : ScriptableObject
{
    [System.Serializable]
    public class LogEntry
    {
        public string dayName; // "Day1", "Day2", "Ending"
        [TextArea(2, 5)] public string[] logs; // 여러 줄의 로그
    }

    [SerializeField]
    private List<LogEntry> logEntries = new List<LogEntry>();

    public List<LogEntry> LogEntries => logEntries;
}
