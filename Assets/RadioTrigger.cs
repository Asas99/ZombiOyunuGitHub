using UnityEngine;
using TMPro;

public class RadioTriggerTMP : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public AudioSource radioAudio; // Ses kayna��

    [TextArea(3, 10)]
    public string message = "Hey tekrar merhaba. �u an beni net duyuyorsan, ben yeralt� �ss�nde mahsur kald�m. Beni buradan kurtarabilirsen e�er, bu salg�n� durdurabilirim. Yeralt� �ss�, hastanenin ilerisindeki orman�n i�erisinde. Patikay� takip et...";

    private bool hasTriggered = false;

    void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            ShowDialogue();
        }
    }

    void ShowDialogue()
    {
        dialoguePanel.SetActive(true);
        dialogueText.text = message;

        if (radioAudio != null)
        {
            radioAudio.Play(); // Ses �almaya ba�la
        }

        Invoke(nameof(HideDialogue), 20f); // 10 saniye sonra kapat
    }

    void HideDialogue()
    {
        dialogueText.text = "";
        dialoguePanel.SetActive(false);

        if (radioAudio != null && radioAudio.isPlaying)
        {
            radioAudio.Stop(); // Ses durdur
        }
    }
}
