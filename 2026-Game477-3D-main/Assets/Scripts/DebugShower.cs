using UnityEngine;
using System.Collections.Generic;

public class ScreenDebug : MonoBehaviour
{
    private List<string> messages = new List<string>();

    void OnEnable() { Application.logMessageReceived += HandleLog; }
    void OnDisable() { Application.logMessageReceived -= HandleLog; }

    void HandleLog(string message, string stackTrace, LogType type)
    {
        messages.Add(message);
        if (messages.Count > 10) messages.RemoveAt(0);
    }

    void OnGUI()
    {
        for (int i = 0; i < messages.Count; i++)
            GUI.Label(new Rect(10, 10 + i * 20, 800, 20), messages[i]);
    }
}