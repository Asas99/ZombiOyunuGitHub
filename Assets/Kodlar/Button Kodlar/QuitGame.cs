using UnityEngine;

public class QuitGame : MonoBehaviour
{
    // Quit butonuna bağlanacak fonksiyon
    public void Quit()
    {
        // Unity Editor'de çalışırken
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        // Derlenmiş oyunda çalışırken
        Application.Quit();
        #endif
    }
}
