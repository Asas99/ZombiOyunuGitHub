using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    [Header("Lights")]
    public Light directionalLight;  // Güneş ışığı
    public Light moonLight;         // Ay ışığı

    [Header("Time Settings")]
    public float dayDuration = 60f; // Tam döngü süresi (saniye)

    private float time; // 0 - 1 arası zaman
    private Gradient sunColor;

    void Start()
    {
        RenderSettings.ambientMode = AmbientMode.Flat;

        // Güneş rengi gradient’i
        sunColor = new Gradient();
        sunColor.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(0.043f, 0.114f, 0.227f), 0f),
                new GradientColorKey(new Color(1f, 0.627f, 0.478f), 0.25f),
                new GradientColorKey(Color.white, 0.5f),
                new GradientColorKey(new Color(1f, 0.627f, 0.478f), 0.75f),
                new GradientColorKey(new Color(0.043f, 0.114f, 0.227f), 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );

        // Directional Light gölge ayarları
        directionalLight.shadows = LightShadows.Soft;
        directionalLight.shadowStrength = 0.8f;
        directionalLight.shadowBias = 0.05f;
        directionalLight.shadowNormalBias = 0.4f;

        // Moon Light gölge kapalı veya isteğe göre ayarla
        moonLight.shadows = LightShadows.None;
    }

    void Update()
    {
        // Zaman ilerlet
        time += Time.deltaTime / dayDuration;
        if (time > 1f) time = 0f;

        // Işık dönüşü
        float sunAngle = time * 360f - 90f;
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);
        moonLight.transform.rotation = Quaternion.Euler(sunAngle + 180f, 170f, 0f);

        // Işık yoğunluğu (0-1 arası)
        float intensity = Mathf.Clamp01(Mathf.Sin(time * Mathf.PI));

        // Gün mü gece mi kontrolü
        bool isDay = intensity > 0.05f;
        directionalLight.enabled = isDay;
        moonLight.enabled = !isDay;

        // Yoğunluk ve ambient ışık ayarları
        if (isDay)
        {
            directionalLight.intensity = intensity * 1.2f;
            RenderSettings.ambientLight = new Color(0.4f, 0.4f, 0.4f); // Kapalı gri gündüz ambiyans
        }
        else
        {
            moonLight.intensity = (1f - intensity) * 0.2f;
            RenderSettings.ambientLight = new Color(0.05f, 0.1f, 0.2f); // Koyu mavi gece ambiyans
        }

        // Güneş ışığı rengi
        directionalLight.color = sunColor.Evaluate(time);
    }
}
