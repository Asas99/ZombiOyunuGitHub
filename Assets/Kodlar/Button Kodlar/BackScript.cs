using UnityEngine;

public class BackScript : MonoBehaviour
{
    public GameObject optionsPanel; // Options Panel GameObject
    public GameObject mainMenuPanel; // Main Menu Panel GameObject

    // Main Menu butonuna bağlanacak fonksiyon
    public void ToggleMainMenu()
    {
        if (optionsPanel != null && mainMenuPanel != null)
        {
            bool isMainMenuActive = mainMenuPanel.activeSelf;

            // Main Menu Panel'i aç/kapat
            mainMenuPanel.SetActive(!isMainMenuActive);

            // Options Panel'i tam tersi duruma getir
            optionsPanel.SetActive(isMainMenuActive);
        }
    }
}
