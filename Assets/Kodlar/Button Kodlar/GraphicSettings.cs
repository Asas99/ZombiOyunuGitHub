using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class GraphicSettings : MonoBehaviour
{
    public Dropdown screenModeDropdown;
    public Dropdown vSyncDropdown;
    public Dropdown textureDropdown;
    public Dropdown shadowDropdown;
    public Dropdown shadowDistanceDropdown;
    public Dropdown antiAliasingDropdown;
    public Dropdown anisotropicFilteringDropdown;

    void Start()
    {
        // O anki sistem ayarlarını dropdown'lara yansıt
        ApplyCurrentSettings();

        // Her bir dropdown'a listener ekle
        screenModeDropdown.onValueChanged.AddListener(delegate { ApplyScreenMode(); });
        vSyncDropdown.onValueChanged.AddListener(delegate { ApplyVSync(); });
        textureDropdown.onValueChanged.AddListener(delegate { ApplyTextureQuality(); });
        shadowDropdown.onValueChanged.AddListener(delegate { ApplyShadowQuality(); });
        shadowDistanceDropdown.onValueChanged.AddListener(delegate { ApplyShadowDistance(); });
        antiAliasingDropdown.onValueChanged.AddListener(delegate { ApplyAntiAliasing(); });
        anisotropicFilteringDropdown.onValueChanged.AddListener(delegate { ApplyAnisotropicFiltering(); });
    }

    void ApplyCurrentSettings()
    {
        // Ekran modu
        switch (Screen.fullScreenMode)
        {
            case FullScreenMode.ExclusiveFullScreen:
                screenModeDropdown.value = 0;
                break;
            case FullScreenMode.Windowed:
                screenModeDropdown.value = 1;
                break;
            case FullScreenMode.FullScreenWindow:
                screenModeDropdown.value = 2;
                break;
        }

        // VSync
        vSyncDropdown.value = QualitySettings.vSyncCount;

        // Texture
        textureDropdown.value = QualitySettings.globalTextureMipmapLimit == 0 ? 1 : 0;

        // Shadow
        shadowDropdown.value = (int)QualitySettings.shadows;

        // Shadow Distance
        float dist = QualitySettings.shadowDistance;
        if (dist <= 30f)
            shadowDistanceDropdown.value = 0;
        else if (dist <= 60f)
            shadowDistanceDropdown.value = 1;
        else
            shadowDistanceDropdown.value = 2;

        // Anti-aliasing
        int aa = QualitySettings.antiAliasing;
        antiAliasingDropdown.value = aa == 0 ? 0 : (int)Mathf.Log(aa, 2);

        // Anisotropic
        anisotropicFilteringDropdown.value = (int)QualitySettings.anisotropicFiltering;
    }

    void ApplyScreenMode()
    {
        print("ApplyScreenMode");
        switch (screenModeDropdown.value)
        {
            case 0: Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; break;
            case 1: Screen.fullScreenMode = FullScreenMode.Windowed; break;
            case 2: Screen.fullScreenMode = FullScreenMode.FullScreenWindow; break;
        }
    }

    void ApplyVSync()
    {
        print("ApplyVSync");
        QualitySettings.vSyncCount = vSyncDropdown.value;
    }

    void ApplyTextureQuality()
    {
        print("ApplyTextureQuality");
        // 0: Low → Limit 1, 1: High → Limit 0
        QualitySettings.globalTextureMipmapLimit = textureDropdown.value == 0 ? 1 : 0;
    }

    void ApplyShadowQuality()
    {
        print("ApplyShadowMode");
        QualitySettings.shadows = (ShadowQuality)shadowDropdown.value;
    }

    void ApplyShadowDistance()
    {
        print("ApplyScreenDistance");
        switch (shadowDistanceDropdown.value)
        {
            case 0: QualitySettings.shadowDistance = 30f; break;
            case 1: QualitySettings.shadowDistance = 60f; break;
            case 2: QualitySettings.shadowDistance = 100f; break;
        }
    }

    void ApplyAntiAliasing()
    {
        print("ApplyAntiAliasing");
        // 0: Disabled, 1: 2x, 2: 4x, 3: 8x → değerler 2 üzeri n
        int val = antiAliasingDropdown.value;
        QualitySettings.antiAliasing = val == 0 ? 0 : (int)Mathf.Pow(2, val);
    }

    void ApplyAnisotropicFiltering()
    {
        print("ApplyAntisotropicFiltering");
        QualitySettings.anisotropicFiltering = (AnisotropicFiltering)anisotropicFilteringDropdown.value;
    }
}

