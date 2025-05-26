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

        // Sahne 2'deyiz ve ya sahne 0'dan geldik ya da test için direkt sahne 2'den baþlatýldýysa çalýþtýr
        if (!((previousScene == 0 && currentScene == 2) || previousScene == -1))
        {
            this.enabled = false;
            return;
        }

        taskText.text = "Görev: Radyoyu bul";
        dialogueBox.SetActive(false);
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
        taskText.text = "Görev: Dýþarý çýk ve yer altý üssünü bul";
    }
}
