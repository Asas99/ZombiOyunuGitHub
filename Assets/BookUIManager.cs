using UnityEngine;

public class BookUIManager : MonoBehaviour
{
    public GameObject rulesPanel;
    public GameObject taskPanel;

    public AudioClip uiSound;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        CloseAllPanels();
    }

    public void ShowRulesPanel()
    {
        CloseAllPanels();
        rulesPanel.SetActive(true);
        PlayUISound();
        PauseGame();
    }

    public void ShowTaskPanel()
    {
        CloseAllPanels();
        taskPanel.SetActive(true);
        PlayUISound();
        PauseGame();
    }

    public void CloseAllPanels()
    {
        rulesPanel.SetActive(false);
        taskPanel.SetActive(false);
        ResumeGame();
    }

    private void PlayUISound()
    {
        if (uiSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(uiSound);
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
    }
}
