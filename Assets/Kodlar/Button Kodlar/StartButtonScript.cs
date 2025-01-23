using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButtonScript : MonoBehaviour
{
    // Bu fonksiyon buton tıklandığında çağrılır
    public void OnStartButtonClick()
    {
        Debug.Log("Start Butonuna Tıklandı!");
        
        // Yeni bir sahne yüklemek için (örneğin: "GameScene")
        SceneManager.LoadScene("zombi 1");
    }
}
