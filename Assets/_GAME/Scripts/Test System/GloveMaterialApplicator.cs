using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GloveMaterialApplicator : TaskReactor
{
    [SerializeField]
    private List<SkinnedMeshRenderer> renderersToApply = new List<SkinnedMeshRenderer>();

    [SerializeField]
    private Material gloveMaterial = null;

    public override void OnCorrectTaskCompleted(int taskIndex)
    {
        if (taskIndex != taskTarget)
            return;

        foreach (SkinnedMeshRenderer renderer in renderersToApply)
        {
            renderer.material = gloveMaterial;
        }
    }
}
