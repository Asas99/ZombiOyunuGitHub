using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

public class DayNightCycle : MonoBehaviour
{
    [Header("Lights")]
    public Light directionalLight;
    public Light moonLight;

    [Header("Time Settings")]
    public float dayDuration = 60f;
    [SerializeField]
    private float time;
    private Gradient sunColor;
    private Gradient ambientGradient;
    private Gradient fogGradient;

    void Start()
    {
        RenderSettings.ambientMode = AmbientMode.Flat;
        

        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "EmreAlexEv")
        {
            RenderSettings.fog = false;
        }
        else
        {
            RenderSettings.fog = true;
        }

        // Öncelik: Uykudan uyanma kontrolü
        if (PlayerPrefs.GetInt("WakeUp", 0) == 1)
        {
            time = 0.2f; // Sabah
            PlayerPrefs.SetInt("WakeUp", 0);
        }
        // Eğer uyanma değilse, kayıtlı zamanı yükle
        else if (PlayerPrefs.HasKey("SavedTime"))
        {
            time = PlayerPrefs.GetFloat("SavedTime");
        }

        // GÜNEŞ IŞIĞI RENGİ GEÇİŞLERİ
        sunColor = new Gradient();
        sunColor.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(0.1f, 0.1f, 0.2f), 0f),
                new GradientColorKey(new Color(1f, 0.5f, 0.3f), 0.25f),
                new GradientColorKey(Color.white, 0.5f),
                new GradientColorKey(new Color(1f, 0.5f, 0.3f), 0.75f),
                new GradientColorKey(new Color(0.1f, 0.1f, 0.2f), 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );

        // AMBIENT LIGHT GEÇİŞİ
        ambientGradient = new Gradient();
        ambientGradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(0.03f, 0.04f, 0.06f), 0f),
                new GradientColorKey(new Color(0.25f, 0.2f, 0.2f), 0.2f),
                new GradientColorKey(new Color(0.55f, 0.55f, 0.5f), 0.5f),
                new GradientColorKey(new Color(0.25f, 0.2f, 0.2f), 0.8f),
                new GradientColorKey(new Color(0.03f, 0.04f, 0.06f), 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );

        // FOG RENGİ
        fogGradient = new Gradient();
        fogGradient.SetKeys(
            new GradientColorKey[] {
                new GradientColorKey(new Color(0.05f, 0.06f, 0.08f), 0f),
                new GradientColorKey(new Color(0.35f, 0.3f, 0.25f), 0.25f),
                new GradientColorKey(new Color(0.8f, 0.8f, 0.75f), 0.5f),
                new GradientColorKey(new Color(0.35f, 0.3f, 0.25f), 0.75f),
                new GradientColorKey(new Color(0.05f, 0.06f, 0.08f), 1f)
            },
            new GradientAlphaKey[] {
                new GradientAlphaKey(1f, 0f),
                new GradientAlphaKey(1f, 1f)
            }
        );
    }

    void Update()
    {
        time += Time.deltaTime / dayDuration;
        if (time > 1f) time = 0f;

        // Zamanı kaydet
        PlayerPrefs.SetFloat("SavedTime", time);

        float sunAngle = time * 360f - 90f;
        directionalLight.transform.rotation = Quaternion.Euler(sunAngle, 170f, 0f);
        moonLight.transform.rotation = Quaternion.Euler(sunAngle + 180f, 170f, 0f);

        float intensity = Mathf.Clamp01(Mathf.Sin(time * Mathf.PI));
        bool isDay = intensity > 0.05f;

        directionalLight.enabled = isDay;
        moonLight.enabled = !isDay;

        directionalLight.intensity = isDay ? intensity * 1.2f : 0f;
        moonLight.intensity = isDay ? 0f : (1f - intensity) * 0.3f;

        directionalLight.color = sunColor.Evaluate(time);
        RenderSettings.ambientLight = ambientGradient.Evaluate(time);
        RenderSettings.fogColor = fogGradient.Evaluate(time);
    }

    public float GetCurrentTime()
    {
        return time;
    }

    public void SetTime(float newTime)
    {
        time = Mathf.Clamp01(newTime);
    }
}
