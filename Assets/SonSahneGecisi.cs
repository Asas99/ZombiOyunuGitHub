using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SonSahneGecisi : MonoBehaviour
{


   
    public TextMesh promptText;    // UI �zerindeki Text nesnesi
    private bool isPlayerInTrigger = false;

    void Start()
    {
        promptText.gameObject.SetActive(false); // Ba�ta yaz� kapal�
    }

    void Update()
    {
        if (isPlayerInTrigger && Input.GetKeyDown(KeyCode.E))
        {
            SceneManager.LoadScene(3);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = true;
            promptText.gameObject.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerInTrigger = false;
            promptText.gameObject.SetActive(false);
        }
    }
}
