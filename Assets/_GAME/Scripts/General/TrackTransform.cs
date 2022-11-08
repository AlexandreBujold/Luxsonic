using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Moves transform of attached object to match target transform, accounting for an initial offset.
/// </summary>
public class TrackTransform : MonoBehaviour
{
    [SerializeField]
    private Transform target = null;

    // Update is called once per frame
    void Update()
    {
        if (target == null)
            return;

        transform.SetPositionAndRotation(target.transform.position, target.rotation);
    }
}
