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
    private Gradient ambientGradient;
    private Gradient fogGradient;

    void Start()
    {
        RenderSettings.ambientMode = AmbientMode.Flat;
        RenderSettings.fog = true;

        // GÜNEŞ IŞIĞI RENGİ GEÇİŞLERİ
        sunColor = new Gradient();
        sunColor.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(0.1f, 0.1f, 0.2f), 0f),       // Gece
                new GradientColorKey(new Color(1f, 0.5f, 0.3f), 0.25f),      // Gün doğumu
                new GradientColorKey(Color.white, 0.5f),                    // Gündüz
                new GradientColorKey(new Color(1f, 0.5f, 0.3f), 0.75f),      // Gün batımı
                new GradientColorKey(new Color(0.1f, 0.1f, 0.2f), 1f)        // Gece
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );

        // AMBIENT LIGHT GEÇİŞİ (gerçekçi, yumuşak)
        ambientGradient = new Gradient();
        ambientGradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(0.03f, 0.04f, 0.06f), 0f),      // Gece
                new GradientColorKey(new Color(0.25f, 0.2f, 0.2f), 0.2f),      // Gün doğumu
                new GradientColorKey(new Color(0.55f, 0.55f, 0.5f), 0.5f),     // Gündüz
                new GradientColorKey(new Color(0.25f, 0.2f, 0.2f), 0.8f),      // Gün batımı
                new GradientColorKey(new Color(0.03f, 0.04f, 0.06f), 1f)       // Gece
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );

        // FOG RENGİ: Ambient ile uyumlu şekilde geçiş
        fogGradient = new Gradient();
        fogGradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(0.05f, 0.06f, 0.08f), 0f),       // Gece
                new GradientColorKey(new Color(0.35f, 0.3f, 0.25f), 0.25f),     // Gün doğumu
                new GradientColorKey(new Color(0.8f, 0.8f, 0.75f), 0.5f),       // Gündüz
                new GradientColorKey(new Color(0.35f, 0.3f, 0.25f), 0.75f),     // Gün batımı
                new GradientColorKey(new Color(0.05f, 0.06f, 0.08f), 1f)        // Gece
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );

        // Gölge ve ışık detayları
        directionalLight.shadows = LightShadows.Soft;
        directionalLight.shadowStrength = 0.9f;
        directionalLight.shadowBias = 0.05f;

        moonLight.color = new Color(0.3f, 0.35f, 0.45f); // Soğuk gece ışığı
        moonLight.shadows = LightShadows.Soft;
        moonLight.shadowStrength = 0.5f;
    }

    void Update()
    {
        time += Time.deltaTime / dayDuration;
        if (time > 1f) time = 0f;

        float sunAngle = time * 360f - 90f;
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);
        moonLight.transform.rotation = Quaternion.Euler(sunAngle + 180f, 170f, 0f);

        float intensity = Mathf.Clamp01(Mathf.Sin(time * Mathf.PI));
        bool isDay = intensity > 0.05f;

        directionalLight.enabled = isDay;
        moonLight.enabled = !isDay;

        // Işık yoğunluğu
        directionalLight.intensity = isDay ? intensity * 1.2f : 0f;
        moonLight.intensity = isDay ? 0f : (1f - intensity) * 0.3f;

        // Renk ve ortam geçişleri
        directionalLight.color = sunColor.Evaluate(time);
        RenderSettings.ambientLight = ambientGradient.Evaluate(time);
        RenderSettings.fogColor = fogGradient.Evaluate(time);
    }
}
