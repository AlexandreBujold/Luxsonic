using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Observes if a task has been attempted and sends a signal when it does.
/// </summary>
public abstract class TaskObserver : MonoBehaviour
{
    [SerializeField, Range(0, 10)]
    protected int taskIndex = 0;

    public UnityEvent OnAttempt = new UnityEvent();
    public UnityEvent OnCompletionSuccess = new UnityEvent();

    /// <summary>
    /// Tells the manager the task was completed.
    /// </summary>
    /// <returns>True if the task was completed correctly, false if it failed.</returns>
    public virtual bool TaskCompleted()
    {
        bool success = TaskManager.SubmitTaskCompleted(taskIndex);
        if (success)
            OnCompletionSuccess?.Invoke();
        return success;
    }

    /// <summary>
    /// Tells the manager the task was attempted.
    /// </summary>
    public virtual void TaskAttempted()
    {
        TaskManager.SubmitTaskAttempted(taskIndex);
        OnAttempt?.Invoke();
    }
}
