using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.Events;

/// <summary>
/// Detects specified objects in hand and invokes events. 
/// </summary>
public class GrabbedItemDetector : MonoBehaviour
{
    [SerializeField]
    private List<Transform> detectTargets = new List<Transform>();

    [field: SerializeField]
    public UnityEvent<Transform> OnTargetDetected { get; private set; } = new UnityEvent<Transform>();
    [field: SerializeField]
    public UnityEvent<Transform> OnTargetLost { get; private set; } = new UnityEvent<Transform>();

    private Transform trackedItem = null;

    // Start is called before the first frame update
    void Start()
    {

    }

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        if (args.interactableObject != null)
        {
            if (detectTargets.Contains(args.interactableObject.transform)) //Item Detected
            {
                trackedItem = args.interactableObject.transform;
                OnTargetDetected?.Invoke(trackedItem);
            }
        }
    }

    public void OnSelectExit(SelectExitEventArgs args)
    {
        if (args.interactableObject != null)
        {
            if (args.interactableObject.transform == trackedItem) //Item Lost
            {
                OnTargetLost?.Invoke(trackedItem);
                trackedItem = null;
            }
        }
    }

    [ContextMenu("Falsify")]
    private void Falsify()
    {
        OnTargetDetected?.Invoke(trackedItem);
    }
}
