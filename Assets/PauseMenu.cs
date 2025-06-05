using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseMenu : MonoBehaviour
{
    public GameObject pausePanel;
    public GameObject optionsPanel;
    public Slider brightnessSlider;
    public Slider volumeSlider;

    public Light mainLight; // Directional Light
    public AudioSource globalAudioSource; // Ana ses kaynaðý

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);

        // Slider dinleyicileri
        brightnessSlider.onValueChanged.AddListener(SetBrightness);
        volumeSlider.onValueChanged.AddListener(SetVolume);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanel.activeSelf)
            {
                CloseOptions();
            }
            else
            {
                if (isPaused)
                    ResumeGame();
                else
                    PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeGame()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(false);
        Time.timeScale = 1f;
        isPaused = false;
    }

    public void OpenOptions()
    {
        pausePanel.SetActive(false);
        optionsPanel.SetActive(true);
    }

    public void CloseOptions()
    {
        optionsPanel.SetActive(false);
        pausePanel.SetActive(true);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f; // Sahne deðiþtirmeden önce zamaný düzelt
        SceneManager.LoadScene(0); // 0 numaralý sahne
    }

    public void QuitGame()
    {
        Debug.Log("Oyundan çýkýlýyor...");
        Application.Quit(); // Build alýnca çalýþýr
    }

    public void SetBrightness(float value)
    {
        if (mainLight != null)
            mainLight.intensity = value;
    }

    public void SetVolume(float value)
    {
        if (globalAudioSource != null)
            globalAudioSource.volume = value;
    }
}
