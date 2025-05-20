using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneInteractionManager : MonoBehaviour
{
    public GameObject player;
    public GameObject enterButton;
    public GameObject exitButton;
    public GameObject sleepButton;

    public Transform doorPosition;
    public Transform bedPosition;
    public float interactionDistance = 3f;

    private DayNightCycle dayNightCycle;

    void Start()
    {
        if (enterButton != null) enterButton.SetActive(false);
        if (exitButton != null) exitButton.SetActive(false);
        if (sleepButton != null) sleepButton.SetActive(false);

        if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            dayNightCycle = FindObjectOfType<DayNightCycle>();
        }

        if (enterButton != null)
            enterButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(1));

        if (exitButton != null)
            exitButton.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(0));

        if (sleepButton != null)
            sleepButton.GetComponent<Button>().onClick.AddListener(SleepAndExit);
    }

    void Update()
    {
        float distToDoor = Vector3.Distance(player.transform.position, doorPosition.position);

        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            enterButton?.SetActive(distToDoor < interactionDistance);
        }
        else if (SceneManager.GetActiveScene().buildIndex == 1)
        {
            exitButton?.SetActive(distToDoor < interactionDistance);

            float distToBed = Vector3.Distance(player.transform.position, bedPosition.position);
            bool isNight = dayNightCycle != null && (dayNightCycle.GetCurrentTime() < 0.25f || dayNightCycle.GetCurrentTime() > 0.75f);

            sleepButton?.SetActive(distToBed < interactionDistance && isNight);
        }
    }

    void SleepAndExit()
    {
        PlayerPrefs.SetInt("WakeUp", 1);
        SceneManager.LoadScene(0);
    }
}
