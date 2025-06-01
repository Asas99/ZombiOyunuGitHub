using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class TaskManager : MonoBehaviour
{
    [Header("Task System")]
    public TextMeshProUGUI taskText;

    [Header("Dialogue")]
    public GameObject dialogueBox;
    public TextMeshProUGUI dialogueText;
    public AudioSource radioDialogueAudio;

    [Header("Radio Proximity")]
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

        // ONLY continue if transitioned from scene 0 to scene 2
        if (!(previousScene == 0 && currentScene == 2))
        {
            Debug.Log("[TASKMANAGER] Condition not met, deactivating all task objects.");

            if (taskText != null) taskText.gameObject.SetActive(false);
            if (dialogueBox != null) dialogueBox.SetActive(false);
            this.enabled = false;
            return;
        }

        // Task started
        if (taskText != null) taskText.text = "Task: Find the radio";
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
        dialogueText.text = "Dr. Brooks: Hey, you there! You're not alone.";
        yield return new WaitForSeconds(2.5f);
        dialogueText.text += "\nFind me, I need help.";
        yield return new WaitForSeconds(2f);
        dialogueText.text += "\nYou have to find the underground base...";
        yield return new WaitForSeconds(2.5f);
        dialogueText.text += "\nWe can end this... the underground base... patient...";
        yield return new WaitForSeconds(3f);

        dialogueBox.SetActive(false);
        taskText.gameObject.SetActive(true);
        taskText.text = "Task: Get outside and find another radio nearby to follow the signal";
    }
}
