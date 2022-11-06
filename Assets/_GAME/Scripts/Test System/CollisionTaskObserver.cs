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

    private void OnCollisionEnter(Collision collision)
    {
        OnEnter(collision.collider);
    }

    private void OnCollisionExit(Collision collision)
    {
        OnExit(collision.collider);
    }
}
