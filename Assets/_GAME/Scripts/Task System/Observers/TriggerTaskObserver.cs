using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens for trigger events to observe a task related event.
/// </summary>
public class TriggerTaskObserver : PhysicalTaskObserver
{
    protected override void OnEnter(Collider collider)
    {
        if (observeOnEnter == false)
            return;

        if (IsExpectedAny(collider))
        {
            //Debug.Log(observationType.ToString() + " OnEnter!");
            lastCollider = collider;
            TaskObserved();
        }
    }

    protected override void OnExit(Collider collider)
    {
        if (observeOnExit == false)
            return;

        if (IsExpectedAny(collider))
        {
            //Debug.Log(observationType.ToString() + " OnExit!");
            lastCollider = collider;
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
