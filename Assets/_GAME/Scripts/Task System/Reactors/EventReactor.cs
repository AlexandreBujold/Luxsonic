using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EventReactor : TaskReactor
{
    [field: SerializeField]
    public UnityEvent OnCorrectAttempted { get; private set; } = new UnityEvent();
    [field: SerializeField]
    public UnityEvent OnIncorrectAttempted { get; private set; } = new UnityEvent();
    [field: SerializeField]
    public UnityEvent OnCorrectCompleted { get; private set; } = new UnityEvent();
    [field: SerializeField]
    public UnityEvent OnIncorrectCompleted { get; private set; } = new UnityEvent();

    public override void OnCorrectTaskAttempted(int taskIndex)
    {
        OnCorrectAttempted?.Invoke();
    }

    public override void OnIncorrectTaskAttempted(int taskIndex)
    {
        OnIncorrectAttempted?.Invoke();
    }

    public override void OnCorrectTaskCompleted(int taskIndex)
    {
        OnCorrectCompleted?.Invoke();
    }

    public override void OnIncorrectTaskCompleted(int taskIndex)
    {
        OnIncorrectCompleted?.Invoke();
    }
}
