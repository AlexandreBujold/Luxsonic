using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class EditorUtilities
{
#if UNITY_EDITOR
    [UnityEditor.MenuItem("TOOLS/Recompile")]
    public static void Recompile()
    {
        Debug.Log("Forcing Recompile... " +
            "\n You can verify recompile by checking the bottom right corner of the editor for a loading circle.");
        UnityEditor.Compilation.CompilationPipeline.RequestScriptCompilation();
    }

    [UnityEditor.MenuItem("TOOLS/Refresh Asset Database")]
    public static void RefreshAssetDatabase()
    {
        UnityEditor.AssetDatabase.Refresh(UnityEditor.ImportAssetOptions.ForceUpdate);
    }
#endif
}