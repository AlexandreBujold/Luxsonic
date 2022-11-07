using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// Toggles objects depending on what has been grabbed.
/// </summary>
public class GrabbedItemGate : MonoBehaviour
{
    [SerializeField]
    private GrabbedItemDetector detector = null;
    [SerializeField, Tooltip("The amount of time in seconds before the objects are toggled off after the item is lost.")]
    private float disableDelayAfterItemLost = 2f;
    [SerializeField]
    private List<int> targetItemIDs = new List<int>();

    [Space]
    public UnityEvent OnItemDetected = new UnityEvent();
    public UnityEvent OnItemLost = new UnityEvent();

    private float lastTimeLost = 0;
    private bool itemDetected = false;
    private Item currentItem = null;

    private Coroutine DisableCoroutine = null;

    private void Awake()
    {
        detector = detector == null ? GetComponent<GrabbedItemDetector>() : detector;
    }

    private void OnEnable()
    {
        if (detector != null)
        {
            detector.OnTargetDetected.AddListener(OnDetect);
            detector.OnTargetLost.AddListener(OnLost);
        }
        else
        {
            Debug.LogWarning("No Grabbed Item Detector found!", gameObject);
        }
    }

    private void OnDisable()
    {
        if (detector != null)
        {
            detector.OnTargetDetected.RemoveListener(OnDetect);
            detector.OnTargetLost.RemoveListener(OnLost);
        }
    }

    private void OnDetect(Item target)
    {
        if (targetItemIDs.Contains(target.ID))
        {
            currentItem = target;
            itemDetected = true;
            OnItemDetected?.Invoke();
        }
    }

    private void OnLost(Item target)
    {
        if (target == currentItem)
        {
            itemDetected = false;
            lastTimeLost = Time.time;

            if (DisableCoroutine != null)
            {
                StopCoroutine(DisableCoroutine);
            }
            DisableCoroutine = StartCoroutine(DelayedDisable());
        }
    }

    private IEnumerator DelayedDisable()
    {
        for (float time = 0; time < disableDelayAfterItemLost; time += Time.deltaTime)
        {
            yield return null;
        }

        if (itemDetected == false)
        {
            OnItemLost?.Invoke();
        }

        DisableCoroutine = null;
    }
}
