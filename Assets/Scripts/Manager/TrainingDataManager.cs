using System;
using System.IO;
using UnityEngine;

public class TrainingDataManager : MonoBehaviour

{
    private static string logPath;
    private static float sessionStartTime;

    public static TrainingDataManager Instance;

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(this);
            return;
        }

        Instance = this;
    }

    public void StartSession()
    {
        sessionStartTime = Time.time;

        string folder = Path.Combine(Application.streamingAssetsPath, "Logs");
        Directory.CreateDirectory(folder);

        string fileName = $"TrainingLog_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
        logPath = Path.Combine(folder, fileName);

        File.WriteAllText(logPath, $"Forklift Training Session - {DateTime.Now}\n");
        File.AppendAllText(logPath, "--------------------------------------\n");
    }

    public void LogAction(string action)
    {
        if (string.IsNullOrEmpty(logPath))
        {
            Debug.LogWarning("No active training session. Call StartSession() first.");
            return;
        }

        float elapsed = Time.time - sessionStartTime;
        string entry = $"[{elapsed:F2}s] ACTION: {action}\n";
        File.AppendAllText(logPath, entry);
        Debug.Log(entry);
    }

    public void LogError(string error)
    {
        if (string.IsNullOrEmpty(logPath))
        {
            Debug.LogWarning("No active training session. Call StartSession() first.");
            return;
        }

        float elapsed = Time.time - sessionStartTime;
        string entry = $"[{elapsed:F2}s] ERROR: {error}\n";
        File.AppendAllText(logPath, entry);
        Debug.LogWarning(entry);
    }

    public void EndSession()
    {
        File.AppendAllText(logPath, "\nTraining Completed.\n");
        Debug.Log($"Log saved to: {logPath}");
        logPath = null;
    }
}
