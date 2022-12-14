using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Listens for collision events to observe a task related event.
/// </summary>
public class CollisionTaskObserver : PhysicalTaskObserver
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

    private void OnCollisionEnter(Collision collision)
    {
        OnEnter(collision.collider);
    }

    private void OnCollisionExit(Collision collision)
    {
        OnExit(collision.collider);
    }
}
