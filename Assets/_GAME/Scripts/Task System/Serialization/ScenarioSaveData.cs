using System.Collections.Generic;

[System.Serializable]
public class ScenarioSaveData
{
    public string ScenarioName = string.Empty;

    public float TotalDuration = 0;
    public int TotalCorrectAttempts = 0;
    public int TotalIncorrectAttempts = 0;

    public List<TaskMetricPair> TaskMetrics = null;

    public ScenarioSaveData(List<TaskMetricPair> taskMetrics, string scenarioName)
    {
        TaskMetrics = taskMetrics;
        ScenarioName = scenarioName;

        //Calculate Total Duration
        float totalTime = 0;
        foreach (TaskMetricPair pair in taskMetrics)
        {
            totalTime += pair.Metrics.TimeToComplete;
            TotalCorrectAttempts += pair.Metrics.Attempts;
            TotalIncorrectAttempts += pair.Metrics.IncorrectAttempts;
        }

        TotalDuration = totalTime;
    }
}
