using UnityEngine;

public class CameraZoom : MonoBehaviour
{
    public Camera targetCamera;
    public Canvas targetCanvas; // Canvas referansý
    public float startFOV = 90f;
    public float endFOV = 20f;
    public float zoomDuration = 5f;

    private float elapsedTime = 0f;
    private bool isZooming = true;

    void Start()
    {
        if (targetCamera != null)
        {
            targetCamera.fieldOfView = startFOV;
        }

        if (targetCanvas != null)
        {
            targetCanvas.enabled = false; // Baþta görünmesin
        }
    }

    void Update()
    {
        if (isZooming && targetCamera != null)
        {
            elapsedTime += Time.deltaTime;
            float t = Mathf.Clamp01(elapsedTime / zoomDuration);
            targetCamera.fieldOfView = Mathf.Lerp(startFOV, endFOV, t);

            if (t >= 1f)
            {
                isZooming = false;

                if (targetCanvas != null)
                {
                    targetCanvas.enabled = true; // Zoom tamamlanýnca göster
                }
            }
        }
    }
}
