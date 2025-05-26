using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [Header("Görev Sistemi")]
    public TextMeshProUGUI taskText;

    [Header("Diyalog")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public AudioSource radioDialogueAudio;

    [Header("Radyoya Yaklaþma")]
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

        // SADECE sahne 0'dan sahne 2'ye geçildiyse çalýþsýn
        if (!(previousScene == 0 && currentScene == 2))
        {
            Debug.Log("[TASKMANAGER] Koþul saðlanmadý, tüm görev objeleri pasif yapýlýyor.");

            // UI'larý da görünmez yap
            if (taskText != null) taskText.gameObject.SetActive(false);
            if (dialogueBox != null) dialogueBox.SetActive(false);
            this.enabled = false;
            return;
        }

        // Görev baþladý
        if (taskText != null) taskText.text = "Görev: Radyoyu bul";
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
        dialogueText.text = "Doktor Brooks: Hey, oradaki! Yalnýz deðilsin.";
        yield return new WaitForSeconds(2.5f);
        dialogueText.text += "\nBeni bul, yardýma ihtiyacým var.";
        yield return new WaitForSeconds(2f);
        dialogueText.text += "\nYer altý üssünü bulmak zorundasýn...";
        yield return new WaitForSeconds(2.5f);
        dialogueText.text += "\nBu iþi sonlandýrabiliriz... yer altý üssü... hasta...";
        yield return new WaitForSeconds(3f);

        dialogueBox.SetActive(false);
        taskText.gameObject.SetActive(true);
        taskText.text = "Görev: Dýþarý çýk ve sinyalin devamý için yakýnlarda yeni bir radyo bul";
    }
}
