using UnityEngine;
using UnityEngine.Rendering;

public class DayNightCycle : MonoBehaviour
{
    [Header("Lights")]
    public Light directionalLight;
    public Light moonLight;

    [Header("Time Settings")]
    public float dayDuration = 60f;

    private float time;
    private Gradient sunColor;

    void Start()
    {
        RenderSettings.ambientMode = AmbientMode.Flat;
        RenderSettings.fog = true;

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

        directionalLight.shadows = LightShadows.Soft;
        directionalLight.shadowStrength = 0.8f;
        directionalLight.shadowBias = 0.05f;
        directionalLight.shadowNormalBias = 0.4f;

        moonLight.shadows = LightShadows.None;
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

        Color currentSunColor = sunColor.Evaluate(time);

        if (isDay)
        {
            directionalLight.intensity = intensity * 1.2f;
            RenderSettings.ambientLight = new Color(0.4f, 0.4f, 0.4f);
        }
        else
        {
            moonLight.intensity = (1f - intensity) * 0.2f;
            RenderSettings.ambientLight = new Color(0.05f, 0.1f, 0.2f);
        }

        directionalLight.color = currentSunColor;

        // Dinamik fog rengi: Gündüz güneş rengi, gece yumuşak koyu mavi
        Color nightFog = new Color(0.08f, 0.1f, 0.15f);
        RenderSettings.fogColor = Color.Lerp(nightFog, currentSunColor, intensity);
    }
}
