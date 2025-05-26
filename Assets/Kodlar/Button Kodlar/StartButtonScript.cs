using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    public void OnStartButtonClick()
    {
        Debug.Log("Start Butonuna Tıklandı!");

        // Şu anki aktif sahnenin indeksini kaydet (örneğin sahne 0)
        PlayerPrefs.SetInt("PreviousSceneIndex", SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.SetInt("PlayWakeUpCutscene", 1); // İstersen sinematik için kullanabilirsin

        // Sahne 2'yi yükle
        SceneManager.LoadScene(2);
    }
}
