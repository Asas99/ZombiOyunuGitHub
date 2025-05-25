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

    private int currentDay = 0;
    private bool hasTriggeredNewDay = false;

    // Yeni gün başladığında tetiklenecek event
    public delegate void NewDayDelegate(int dayCount);
    public static event NewDayDelegate OnNewDay;

    void Start()
    {

        RenderSettings.ambientMode = AmbientMode.Flat;

        string sceneName = SceneManager.GetActiveScene().name;
        RenderSettings.fog = sceneName != "EmreAlexEv";

        // Uyanma kontrolü
        if (PlayerPrefs.GetInt("WakeUp", 0) == 1)
        {
            time = 0.2f; // Sabah
            PlayerPrefs.SetInt("WakeUp", 0);
        }
        else if (PlayerPrefs.HasKey("SavedTime"))
        {
            time = PlayerPrefs.GetFloat("SavedTime");
        }

        SetupGradients();
    }

    void Update()
    {
        time += Time.deltaTime / dayDuration;
        if (time > 1f)
        {
            time = 0f;
            hasTriggeredNewDay = false;
        }

        // Yeni gün başlangıcı kontrolü
        if (time >= 0.01f && time <= 0.02f && !hasTriggeredNewDay)
        {
            currentDay++;
            hasTriggeredNewDay = true;
            OnNewDay?.Invoke(currentDay);
        }

        PlayerPrefs.SetFloat("SavedTime", time);

        UpdateLighting();
    }

    void SetupGradients()
    {
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

    void UpdateLighting()
    {
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

    public float GetCurrentTime() => time;
    public void SetTime(float newTime) => time = Mathf.Clamp01(newTime);
    public int GetCurrentDay() => currentDay;
}
