using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction;
using UnityEngine.XR.Interaction.Toolkit;

public class HandController : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> toggleWhenItemGrabbed = new List<GameObject>();

    public void OnSelectEnter(SelectEnterEventArgs args)
    {
        foreach (GameObject target in toggleWhenItemGrabbed)
        {
            target.SetActive(false);
        }
    }

    public void OnSelectExit(SelectExitEventArgs args)
    {
        foreach (GameObject target in toggleWhenItemGrabbed)
        {
            target.SetActive(true);
        }
    }
}
