using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class TaskReactor : MonoBehaviour
{
    [SerializeField, Range(0,10)]
    protected int taskTarget = 0;
    public virtual void OnCorrectTaskAttempted(int taskIndex)
    {
        return;
    }

    public virtual void OnIncorrectTaskAttempted(int taskIndex)
    {
        return;
    }

    public virtual void OnCorrectTaskCompleted(int taskIndex)
    {
        return;
    }

    public virtual void OnIncorrectTaskCompleted(int taskIndex)
    {
        return;
    }
}
