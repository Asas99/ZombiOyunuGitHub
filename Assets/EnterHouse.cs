using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterHouse : MonoBehaviour
{
    public GameObject player;
    public GameObject enterButton;
    public GameObject exitButton;
    public GameObject sleepButton;

    public Transform bedPosition;
    public float sleepDistance = 3f;

    private DayNightCycle dayNightCycle;
    private bool isPlayerInsideTrigger = false;

    void Start()
    {
        if (enterButton != null) enterButton.SetActive(false);
        if (exitButton != null) exitButton.SetActive(false);
        if (sleepButton != null) sleepButton.SetActive(false);

        dayNightCycle = Object.FindFirstObjectByType<DayNightCycle>();

        if (enterButton != null)
        {
            enterButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                SaveCarPositionIfExists(); // ✅ Araba konumunu kaydet
                SaveCurrentTime();
                SceneManager.LoadScene(2);
            });
        }

        if (exitButton != null)
        {
            exitButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                SaveCarPositionIfExists(); // ✅ Araba konumunu kaydet
                SaveCurrentTime();
                SceneManager.LoadScene(1);
            });
        }

        if (sleepButton != null)
        {
            sleepButton.GetComponent<Button>().onClick.AddListener(SleepAndExit);
        }
    }

    void SaveCarPositionIfExists()
    {
        GameObject car = GameObject.FindWithTag("Car");
        if (car != null)
        {
            carManager manager = car.GetComponent<carManager>();
            if (manager != null)
            {
                manager.SaveCarPosition();
                Debug.Log("Araba konumu sahne geçişi öncesi kaydedildi.");
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            enterButton.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = false;
            if (enterButton != null) enterButton.SetActive(false);
            if (exitButton != null) exitButton.SetActive(false);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2 && sleepButton != null && dayNightCycle != null && bedPosition != null)
        {
            float distanceToBed = Vector3.Distance(player.transform.position, bedPosition.position);
            float time = dayNightCycle.GetCurrentTime();
            bool isNight = time >= 0.75f && time <= 1.0f;

            sleepButton.SetActive(distanceToBed < sleepDistance && isNight);
        }
    }

    void SleepAndExit()
    {
        SaveCarPositionIfExists(); // ✅ Araba konumunu kaydet
        PlayerPrefs.SetInt("WakeUp", 1);
        PlayerPrefs.SetFloat("SavedTime", 0.2f);
        SceneManager.LoadScene(1);
    }

    void SaveCurrentTime()
    {
        DayNightCycle cycle = Object.FindFirstObjectByType<DayNightCycle>();
        if (cycle != null)
        {
            PlayerPrefs.SetFloat("SavedTime", cycle.GetCurrentTime());
        }
    }
}
