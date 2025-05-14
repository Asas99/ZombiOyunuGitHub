using UnityEngine;
using UnityEngine.UI;

public class ForceMinimapUI : MonoBehaviour
{
    public RawImage mapImage;
    public RenderTexture minimapTexture;

    void Start()
    {
        if (mapImage != null && minimapTexture != null)
        {
            mapImage.texture = minimapTexture;
        }
    }
}
