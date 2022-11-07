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
    private XRDirectInteractor interactor = null;

    [SerializeField]
    private List<Collider> detectTargets = new List<Collider>();

    public UnityEvent OnTargetDetected { get; private set; } = new UnityEvent();

    // Start is called before the first frame update
    void Start()
    {
        interactor = interactor == null ? GetComponentInParent<XRDirectInteractor>() : interactor;
    }
    

    void FixedUpdate()
    {
        if (interactor == null)
            return;

        if (interactor.hasSelection)
        {
            Debug.Log("HAND HAS INTERACTOR: " + interactor.firstInteractableSelected.transform.name);
            //List<IXRSelectInteractable> interactables = interactor.interactablesSelected;
            
            ////Only check first as there should only ever be 1 per hand
            //if (interactables.Count != 0)
            //{
            //    Debug.Log("Interactables Count = " + interactables.Count);
            //    foreach (Collider collider in interactables[0].colliders)
            //    {
            //        if (detectTargets.Contains(collider))
            //        {
            //            OnTargetDetected?.Invoke();

            //            break;
            //        }
            //    }
            //}
        }
    }
}
