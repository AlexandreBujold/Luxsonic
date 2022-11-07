using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

/// <summary>
/// Detects specified items in hand and invokes events. 
/// </summary>
public class GrabbedItemDetector : MonoBehaviour
{
    [SerializeField]
    private List<int> targetItemIDs = new List<int>();

    [field: SerializeField]
    public UnityEvent<Item> OnTargetDetected { get; private set; } = new UnityEvent<Item>();
    [field: SerializeField]
    public UnityEvent<Item> OnTargetLost { get; private set; } = new UnityEvent<Item>();

    [SerializeField, Tooltip("The interactors to listen to for select events")]
    private List<XRBaseInteractor> interactors = new List<XRBaseInteractor>();

    private void OnEnable()
    {
        foreach (XRBaseInteractor interactor in interactors)
        {
            if (interactor != null)
            {
                interactor.selectEntered.AddListener(OnSelectEnter);
                interactor.selectExited.AddListener(OnSelectExit);
            }
        }
    }

    private void OnDisable()
    {
        foreach (XRBaseInteractor interactor in interactors)
        {
            if (interactor != null)
            {
                interactor.selectEntered.RemoveListener(OnSelectEnter);
                interactor.selectExited.RemoveListener(OnSelectExit);
            }
        }
    }

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (args.interactableObject != null)
        {
            if (args.interactableObject.transform.TryGetComponent<Item>(out Item target)) //Item Detected
            {
                if (targetItemIDs.Contains(target.ID))
                {
                    OnTargetDetected?.Invoke(target);
                }
            }
        }
    }

    public void OnSelectExit(SelectExitEventArgs args)
    {
        if (args.interactableObject != null)
        {
            OnTargetLost?.Invoke(args.interactableObject.transform.GetComponent<Item>());
        }
    }
}
