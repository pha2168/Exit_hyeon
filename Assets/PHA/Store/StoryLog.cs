#if UNITY_EDITOR
using UnityEditor;
#endif
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Story Log", menuName = "Scriptable Object/Story Log", order = int.MaxValue)]
public class StoryLog : ScriptableObject
{
    [System.Serializable]
    public class LogEntry
    {
        public string dayName;
        [TextArea(2, 10)] public string[] logs;
    }

    [SerializeField]
    private List<LogEntry> logEntries = new List<LogEntry>();

    public List<LogEntry> LogEntries => logEntries;

#if UNITY_EDITOR
    private void OnValidate()
    {
        EditorUtility.SetDirty(this); // UI 업데이트 최소화
    }
#endif
}
