using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens for collision events to observe a task related event.
/// </summary>
public class TriggerTaskObserver : PhysicalTaskObserver
{
    protected override void OnEnter(Collider collider)
    {
        if (observeOnEnter == false)
            return;

        if (IsExpectedCollider(collider) || IsExpectedObject(collider.gameObject))
        {
            Debug.Log(observationType.ToString() + " OnEnter!");
            TaskObserved();
        }
    }

    protected override void OnExit(Collider collider)
    {
        if (observeOnExit == false)
            return;

        if (IsExpectedCollider(collider) || IsExpectedObject(collider.gameObject))
        {
            Debug.Log(observationType.ToString() + " OnExit!");
            TaskObserved();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        OnEnter(other);
    }

    private void OnTriggerExit(Collider other)
    {
        OnExit(other);
    }
}
