using UnityEngine;
using System.Collections.Generic;
using System.IO;
using System;

[System.Serializable]
public class TelemetryData
{
    public string sessionID;
    public string timestamp;
    public string platform;
    public string osVersion;
    public string graphicsDevice;
    public string processorType;
    public int systemMemorySize;
    public float averageFPS;
    public List<float> fpsSamples = new List<float>();
    public UserFeedback userFeedback;
}

[System.Serializable]
public class UserFeedback
{
    public string identity; // Name and Student ID
    public int rating; // 1-5
    public string comments;
    public string bugReport;
    public string suggestions;
    public bool consentGiven;
}

public class TelemetryManager : MonoBehaviour
{
    public static TelemetryManager Instance;

    private TelemetryData currentSessionData;
    private float fpsAccumulator = 0f;
    private int fpsCounter = 0;
    private float fpsTimer = 0f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSession();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSession()
    {
        currentSessionData = new TelemetryData();
        currentSessionData.sessionID = System.Guid.NewGuid().ToString();
        currentSessionData.timestamp = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        currentSessionData.platform = Application.platform.ToString();
        currentSessionData.osVersion = SystemInfo.operatingSystem;
        currentSessionData.graphicsDevice = SystemInfo.graphicsDeviceName;
        currentSessionData.processorType = SystemInfo.processorType;
        currentSessionData.systemMemorySize = SystemInfo.systemMemorySize;
        currentSessionData.userFeedback = new UserFeedback();
    }

    private void Update()
    {
        // FPS Calculation
        fpsAccumulator += Time.unscaledDeltaTime;
        fpsCounter++;
        fpsTimer += Time.unscaledDeltaTime;

        if (fpsTimer >= 1.0f)
        {
            float fps = fpsCounter / fpsAccumulator;
            currentSessionData.fpsSamples.Add(fps);
            
            fpsAccumulator = 0f;
            fpsCounter = 0;
            fpsTimer = 0f;
        }
    }

    public void SubmitFeedback(string identity, int rating, string comments, string bugReport, string suggestions, bool consent)
    {
        currentSessionData.userFeedback.identity = identity;
        currentSessionData.userFeedback.rating = rating;
        currentSessionData.userFeedback.comments = comments;
        currentSessionData.userFeedback.bugReport = bugReport;
        currentSessionData.userFeedback.suggestions = suggestions;
        currentSessionData.userFeedback.consentGiven = consent;
        
        SaveTelemetry();
    }

    private void OnApplicationQuit()
    {
        SaveTelemetry();
    }

    private void SaveTelemetry()
    {
        if (currentSessionData.fpsSamples.Count > 0)
        {
            float sum = 0;
            foreach (float f in currentSessionData.fpsSamples) sum += f;
            currentSessionData.averageFPS = sum / currentSessionData.fpsSamples.Count;
        }

        string json = JsonUtility.ToJson(currentSessionData, true);
        
        // Save to persistent data path
        // Determine filename based on identity or session ID
        string identifier = !string.IsNullOrEmpty(currentSessionData.userFeedback.identity) 
            ? currentSessionData.userFeedback.identity 
            : currentSessionData.sessionID;
            
        string filename = $"Telemetry_{SanitizeFilename(identifier)}.json";
        string path = Path.Combine(Application.persistentDataPath, filename);
        
        // Also save to a more accessible location for the user to find easily during development
        #if UNITY_EDITOR
        path = Path.Combine(Application.dataPath, "../Logs/Telemetry Logs", filename);
        #endif

        // Ensure directory exists
        string directory = Path.GetDirectoryName(path);
        if (!Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        File.WriteAllText(path, json);
        Debug.Log($"Telemetry saved to: {path}");
    }

    private string SanitizeFilename(string name)
    {
        string invalidChars = new string(Path.GetInvalidFileNameChars());
        string escapedInvalidChars = System.Text.RegularExpressions.Regex.Escape(invalidChars);
        string invalidRegStr = string.Format(@"([{0}]*\.+$)|([{0}]+)", escapedInvalidChars);

        return System.Text.RegularExpressions.Regex.Replace(name, invalidRegStr, "_");
    }
}
