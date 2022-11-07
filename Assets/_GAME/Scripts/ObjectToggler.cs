using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds lists of objects to enable or disable when called.
/// </summary>
public class ObjectToggler : MonoBehaviour
{
    //[SerializeField, Tooltip("Cancels a delayed toggle if a call to undo that is received during the coroutine.")]
    //private bool cancelDelayOnOppositeCall = true; 
    [SerializeField]
    private List<GameObject> enableOnCall = new List<GameObject>();

    [SerializeField]
    private List<GameObject> disableOnCall = new List<GameObject>();

    //private Coroutine delayedEnableCoroutine = null;
    //private Coroutine delayedDisableCoroutine = null;

    public void EnableImmediately()
    {
        foreach(GameObject target in enableOnCall)
        {
            target.SetActive(true);
        }
    }

    public void EnableAfterDelay(float delay)
    {
        StartCoroutine(ToggleAfterDelay(delay, enableOnCall, true));
    }

    public void DisableImmediately()
    {
        foreach (GameObject target in disableOnCall)
        {
            target.SetActive(false);
        }
    }

    public void DisableAfterDelay(float delay)
    {
        StartCoroutine(ToggleAfterDelay(delay, disableOnCall, false));
    }
    

    private IEnumerator ToggleAfterDelay(float delay, List<GameObject> targets, bool enable)
    {
        for (float time = 0; time < delay; time += Time.deltaTime)
        {
            yield return null;
        }

        foreach (GameObject target in targets)
        {
            if (target != null)
            {
                target.SetActive(enable);
            }
        }
    }
}
