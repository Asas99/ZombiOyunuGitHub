using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class AutoFPSOptimizer : MonoBehaviour
{
    [Header("URP Asset")]
    public UniversalRenderPipelineAsset urpAsset;

    void Start()
    {
        OptimizeQualitySettings();
        OptimizeURPSettings();
        OptimizeCamera();
    }

    void OptimizeQualitySettings()
    {
        QualitySettings.shadowDistance = 30f;
        QualitySettings.antiAliasing = 0;
        QualitySettings.vSyncCount = 0;
        QualitySettings.lodBias = 2.5f;

        QualitySettings.shadows = UnityEngine.ShadowQuality.Disable;

    }

    void OptimizeURPSettings()
    {
        if (urpAsset == null)
        {
            Debug.LogWarning("URP Asset atanmadý!");
            return;
        }

        urpAsset.renderScale = 0.75f;
        urpAsset.shadowDistance = 30f;
        urpAsset.shadowCascadeCount = 1;
        urpAsset.msaaSampleCount = 1;
    }

    void OptimizeCamera()
    {
        Camera cam = Camera.main;
        if (cam != null)
        {
            float[] distances = new float[32];
            for (int i = 0; i < distances.Length; i++)
                distances[i] = 250f;
            cam.layerCullDistances = distances;
        }
    }
}
