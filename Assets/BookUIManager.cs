using UnityEngine;

public class BookUIManager : MonoBehaviour
{
    public GameObject infoPanel;
    public AudioClip toggleSound;
    public AudioSource audioSource;

    public void ShowInfo()
    {
        infoPanel.SetActive(true);
        audioSource.Play();
    }

    public void HideInfo()
    {
        infoPanel.SetActive(false);
        audioSource.Play();
    }
}
