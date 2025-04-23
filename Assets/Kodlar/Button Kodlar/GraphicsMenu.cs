using UnityEngine;

public class GraphicsMenu : MonoBehaviour
{
    public GameObject graphicsPanel; // Graphics Panel GameObject
    public GameObject optionsPanel; // Options Panel GameObject    

    // Graphics butonuna bağlanacak fonksiyon
    public void ToggleOptions()
    {
        if (graphicsPanel != null && optionsPanel != null)
        {
            bool isGraphicsActive = graphicsPanel.activeSelf;

            // Graphics Panel'i aç/kapat
            graphicsPanel.SetActive(!isGraphicsActive);

            // Options Panel'i tam tersi duruma getir
            optionsPanel.SetActive(isGraphicsActive);                      
        }
    }
}