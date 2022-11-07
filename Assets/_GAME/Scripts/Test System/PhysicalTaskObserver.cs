using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Any task observer that deals with collisions, triggers, rays, etc.
/// </summary>
public abstract class PhysicalTaskObserver : TaskObserver
{
    [Header("Observation Configuration")]
    [SerializeField]
    protected TaskActionType observationType = TaskActionType.Attempt;

    [SerializeField]
    protected bool observeOnEnter = true;
    [SerializeField]
    protected bool observeOnExit = true;

    [Header("References")]
    [SerializeField]
    protected List<GameObject> expectedGameObjects = new List<GameObject>();
    [SerializeField]
    protected List<Collider> expectedColliders = new List<Collider>();
    [SerializeField]
    protected List<int> expectedItemIDs = new List<int>();
    [SerializeField]
    protected List<GameObject> disableOnCompletionSuccess = new List<GameObject>();

    protected abstract void OnEnter(Collider collider);
    protected abstract void OnExit(Collider collider);

    protected virtual bool IsExpectedObject(GameObject target)
    {
        return expectedGameObjects.Contains(target);
    }

    protected virtual bool IsExpectedCollider(Collider collider)
    {
        return expectedColliders.Contains(collider);
    }

    protected virtual bool IsExpectedItem(Item item)
    {
        if (item == null)
            return false;

        return expectedItemIDs.Contains(item.ID);
    }

    protected virtual bool IsExpectedAny(Collider collider)
    {
        return IsExpectedCollider(collider) || IsExpectedObject(collider.gameObject) || IsExpectedItem(collider.GetComponent<Item>());
    }

    protected virtual void TaskObserved()
    {
        TaskObserved(observationType);
    }

    protected virtual void TaskObserved(TaskActionType type)
    {
        switch (type)
        {
            case TaskActionType.Attempt:
                TaskAttempted();
                break;
            case TaskActionType.Completion:
                TaskCompleted();
                break;
        }
    }

    public override bool TaskCompleted()
    {
        bool success = base.TaskCompleted();
        if (success)
        {
            foreach (GameObject target in disableOnCompletionSuccess)
            {
                target.SetActive(false);
            }
        }
        return success;
    }
}
