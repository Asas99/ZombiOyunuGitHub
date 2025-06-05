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
    }

    public void ShowTaskPanel()
    {
        CloseAllPanels();
        taskPanel.SetActive(true);
        PlayUISound();
    }

    public void CloseAllPanels()
    {
        rulesPanel.SetActive(false);
        taskPanel.SetActive(false);
    }

    private void PlayUISound()
    {
        if (uiSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(uiSound);
        }
    }
}
