using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialReactor : TaskReactor
{
    [SerializeField]
    private List<RendererMaterialSwapConfig> renderersToApply = new List<RendererMaterialSwapConfig>();

    public override void OnCorrectTaskCompleted(int taskIndex)
    {
        if (taskIndex != taskTarget)
            return;

        foreach (RendererMaterialSwapConfig swapConfig in renderersToApply)
        {
            swapConfig.Apply();
        }
    }
}

[System.Serializable]
public class RendererMaterialSwapConfig
{
    public Renderer renderer = null;
    public List<MaterialIndexMap> materialIndexMaps = new List<MaterialIndexMap>();

    [System.Serializable]
    public struct MaterialIndexMap
    {
        public Material material;
        public int index;

        public void Apply(Renderer renderer)
        {
            if (renderer == null || material == null)
            {
                Debug.LogWarning("Missing Renderer or Material for MaterialIndexMap.");
                return;
            }

            if (index < renderer.materials.Length)
            {
                Material[] newMaterials = renderer.materials;
                newMaterials[index] = material;
                renderer.materials = newMaterials;
            }
        }
    }

    /// <summary>
    /// Applies all configured materials to the renderer
    /// </summary>
    public void Apply()
    {
        if (renderer == null)
            return;

        foreach (MaterialIndexMap map in materialIndexMaps)
        {
            map.Apply(renderer);
        }
    }
}
