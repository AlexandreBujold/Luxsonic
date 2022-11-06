using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Contains evaluation metrics of a task that were recorded during a scenario.
/// </summary>
[System.Serializable]
public class TaskMetrics
{
    [Tooltip("Attempts of this task.")]
    public int Attempts = 0;
    [Tooltip("Attempts of another task when this was the task.")]
    public int IncorrectAttempts = 0;
    [Tooltip("Time from end of previous task to completion of this task")]
    public float TimeToComplete = 0;
}
