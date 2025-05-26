using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [Header("G�rev Sistemi")]
    public TextMeshProUGUI taskText;

    [Header("Diyalog")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public AudioSource radioDialogueAudio;

    [Header("Radyoya Yakla�ma")]
    public Transform player;
    public Transform radio;
    public float detectDistance = 4f;

    private bool task1Completed = false;
    private bool dialoguePlayed = false;

    void Start()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        int previousScene = SceneTracker.previousSceneIndex;

        Debug.Log($"[TASKMANAGER] CurrentScene: {currentScene}, PreviousScene: {previousScene}");

        // SADECE sahne 0'dan sahne 2'ye ge�ildiyse �al��s�n
        if (!(previousScene == 0 && currentScene == 2))
        {
            Debug.Log("[TASKMANAGER] Ko�ul sa�lanmad�, t�m g�rev objeleri pasif yap�l�yor.");

            // UI'lar� da g�r�nmez yap
            if (taskText != null) taskText.gameObject.SetActive(false);
            if (dialogueBox != null) dialogueBox.SetActive(false);
            this.enabled = false;
            return;
        }

        // G�rev ba�lad�
        if (taskText != null) taskText.text = "G�rev: Radyoyu bul";
        if (dialogueBox != null) dialogueBox.SetActive(false);
    }

    void Update()
    {
        if (!task1Completed && Vector3.Distance(player.position, radio.position) <= detectDistance)
        {
            task1Completed = true;
            StartCoroutine(PlayDialogue());
        }
    }

    IEnumerator PlayDialogue()
    {
        dialoguePlayed = true;
        taskText.text = "";
        dialogueBox.SetActive(true);
        dialogueText.text = "";
        radioDialogueAudio.Play();

        yield return new WaitForSeconds(1f);
        dialogueText.text = "Doktor Brooks: Hey, oradaki! Yaln�z de�ilsin.";
        yield return new WaitForSeconds(2.5f);
        dialogueText.text += "\nBeni bul, yard�ma ihtiyac�m var.";
        yield return new WaitForSeconds(2f);
        dialogueText.text += "\nYer alt� �ss�n� bulmak zorundas�n...";
        yield return new WaitForSeconds(2.5f);
        dialogueText.text += "\nBu i�i sonland�rabiliriz... yer alt� �ss�... hasta...";
        yield return new WaitForSeconds(3f);

        dialogueBox.SetActive(false);
        taskText.gameObject.SetActive(true);
        taskText.text = "G�rev: D��ar� ��k ve sinyalin devam� i�in yak�nlarda yeni bir radyo bul";
    }
}
