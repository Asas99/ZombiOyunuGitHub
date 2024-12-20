using UnityEngine;

public class CreditsMenu : MonoBehaviour
{
    public GameObject creditsPanel; // Options Panel GameObject
    public GameObject mainMenuPanel; // Main Menu Panel GameObject

    // Options butonuna bağlanacak fonksiyon
    public void ToggleCredits()
    {
        if (creditsPanel != null && mainMenuPanel != null)
        {
            bool isCreditsActive = creditsPanel.activeSelf;

            // Options Panel'i aç/kapat
            creditsPanel.SetActive(!isCreditsActive);

            // Main Menu Panel'i tam tersi duruma getir
            mainMenuPanel.SetActive(isCreditsActive);
        }
    }
}



