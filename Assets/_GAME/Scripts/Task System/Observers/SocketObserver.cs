using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

/// <summary>
/// Listens for a target item or object to enter a socket.
/// </summary>
public class SocketObserver : TaskObserver
{
    [SerializeField]
    private XRSocketInteractor socket = null;

    [SerializeField]
    private List<int> targetItems = new List<int>();
    [SerializeField]
    private List<GameObject> targetObjects = new List<GameObject>();

    [Space]
    [SerializeField]
    private TaskActionType actionType = TaskActionType.Attempt;
    [SerializeField]
    private bool triggerOnEnter = false;
    [SerializeField]
    private bool triggerOnExit = false;

    private void OnEnable()
    {
        if (socket != null)
        {
            socket.selectEntered.AddListener(OnSelectEnter);
            socket.selectExited.AddListener(OnSelectExit);
        }
        else
        {
            Debug.LogWarning("There is no socket set to observe!", gameObject);
        }
    }

    private void OnDisable()
    {
        if (socket != null)
        {
            socket.selectEntered.RemoveListener(OnSelectEnter);
            socket.selectExited.RemoveListener(OnSelectExit);
        }
    }

    private void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (triggerOnEnter)
            return;

        if (args.interactableObject != null)
        {
            ProcessInteractable(args.interactableObject.transform);
        }
    }

    private void OnSelectExit(SelectExitEventArgs args)
    {
        if (triggerOnExit)
            return;

        if (args.interactableObject != null)
        {
            ProcessInteractable(args.interactableObject.transform);
        }
    }

    private void ProcessInteractable(Transform target)
    {
        if (target == null)
            return;

        if (IsExpectedObject(target.gameObject))
        {
            TaskObserved(actionType);
        }
        else if (target.TryGetComponent<Item>(out Item itemTarget))
        {
            if (IsExpectedItem(itemTarget))
                TaskObserved(actionType);
        }
    }

    private void TaskObserved(TaskActionType type)
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

    private bool IsExpectedObject(GameObject target)
    {
        return targetObjects.Contains(target);
    }

    private bool IsExpectedItem(Item item)
    {
        return targetItems.Contains(item.ID);
    }
}
