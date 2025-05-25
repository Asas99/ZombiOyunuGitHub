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

        // Sadece ev sahnesindeyken DayNightCycle bul
        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            dayNightCycle = Object.FindFirstObjectByType<DayNightCycle>();

        }

        // Eve gir butonu
        if (enterButton != null)
        {
            enterButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                // Zamaný kaydet
                SaveCurrentTime();
                SceneManager.LoadScene(2);
            });
        }

        // Evden çýk butonu
        if (exitButton != null)
        {
            exitButton.GetComponent<Button>().onClick.AddListener(() =>
            {
                SaveCurrentTime();
                SceneManager.LoadScene(1);
            });
        }

        // Uyu butonu
        if (sleepButton != null)
        {
            sleepButton.GetComponent<Button>().onClick.AddListener(SleepAndExit);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player eve girdi");

            isPlayerInsideTrigger = true;

            if (enterButton != null)
            {
                enterButton.SetActive(true);
                enterButton.transform.position = new Vector3(Screen.width / 2, Screen.height / 2, 0);
            }
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInsideTrigger = false;

            if (enterButton != null) enterButton.SetActive(false);
            if (exitButton != null) exitButton.SetActive(false);
            if (sleepButton != null) sleepButton.SetActive(false);
        }
    }

    void Update()
    {
        // Ev sahnesindeyken yataða yakýnlýk ve gece kontrolü
        if (SceneManager.GetActiveScene().buildIndex == 1 && sleepButton != null && dayNightCycle != null && bedPosition != null)
        {
            float distanceToBed = Vector3.Distance(player.transform.position, bedPosition.position);
            float time = dayNightCycle.GetCurrentTime();
            bool isNight = time < 0.25f || time > 0.75f;

            sleepButton.SetActive(distanceToBed < sleepDistance && isNight);
        }
    }

    void SleepAndExit()
    {
        PlayerPrefs.SetInt("WakeUp", 1); // Sabah baþlat
        PlayerPrefs.SetFloat("SavedTime", 0.2f); // Sabah zamaný
        SceneManager.LoadScene(0);
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
