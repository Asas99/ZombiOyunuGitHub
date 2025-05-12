using UnityEngine;
using UnityEditor;
using UnityEngine.Rendering.Universal;

public class URPMaterialConverter : EditorWindow
{
    [MenuItem("Tools/Convert Selected Materials to URP")]
    public static void ConvertSelectedMaterialsToURP()
    {
        foreach (Object obj in Selection.objects)
        {
            if (obj is Material mat)
            {
                Shader urpShader = Shader.Find("Universal Render Pipeline/Lit");
                if (urpShader != null && mat.shader.name.Contains("Standard"))
                {
                    mat.shader = urpShader;

                    var baseMap = mat.GetTexture("_MainTex");
                    if (baseMap != null)
                        mat.SetTexture("_BaseMap", baseMap);

                    var normalMap = mat.GetTexture("_BumpMap");
                    if (normalMap != null)
                    {
                        mat.EnableKeyword("_NORMALMAP");
                        mat.SetTexture("_BumpMap", normalMap);
                    }

                    Debug.Log($"[URP Convert] {mat.name} baþarýyla dönüþtürüldü.");
                }
            }
        }

        AssetDatabase.SaveAssets();
        AssetDatabase.Refresh();
    }
}
