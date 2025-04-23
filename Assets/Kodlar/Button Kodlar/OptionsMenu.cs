using UnityEngine;

public class OptionsMenu : MonoBehaviour
{
    public GameObject optionsPanel; // Options Panel GameObject
    public GameObject mainMenuPanel; // Main Menu Panel GameObject

    // Options butonuna bağlanacak fonksiyon
    public void ToggleOptions()
    {
        if (optionsPanel != null && mainMenuPanel != null)
        {
            bool isOptionsActive = optionsPanel.activeSelf;

            // Options Panel'i aç/kapat
            optionsPanel.SetActive(!isOptionsActive);

            // Main Menu Panel'i tam tersi duruma getir
            mainMenuPanel.SetActive(isOptionsActive);        
        }
    }
}


