using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds lists of objects to enable or disable when reacting.
/// </summary>
public class ToggleReactor : TaskReactor
{
    [Header("Properties")]
    [SerializeField]
    private float enableDelay = 0f;
    [SerializeField]
    private float disableDelay = 0f;

    [Space]
    [SerializeField]
    private List<GameObject> enableOnCall = new List<GameObject>();

    [SerializeField]
    private List<GameObject> disableOnCall = new List<GameObject>();

    public override void OnCorrectTaskCompleted(int taskIndex)
    {
        if (taskTarget != taskIndex)
            return;

        EnableAfterDelay(enableDelay);
        DisableAfterDelay(disableDelay);
    }

    public void EnableImmediately()
    {
        foreach(GameObject target in enableOnCall)
        {
            target.SetActive(true);
        }
    }

    public void EnableAfterDelay(float delay)
    {
        if (enableOnCall.Count == 0)
            return;

        if (enableDelay == 0)
        {
            EnableImmediately();
            return;
        }

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
        if (disableOnCall.Count == 0)
            return;

        if (disableDelay == 0)
        {
            DisableImmediately();
            return;
        }

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
