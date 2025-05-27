using UnityEngine;
using TMPro;

public class RadioTriggerTMP : MonoBehaviour
{
    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;
    public AudioSource radioAudio; // Ses kaynaðý

    [TextArea(3, 10)]
    public string message = "Hey tekrar merhaba. Þu an beni net duyuyorsan, ben yeraltý üssünde mahsur kaldým. Beni buradan kurtarabilirsen eðer, bu salgýný durdurabilirim. Yeraltý üssü, hastanenin ilerisindeki ormanýn içerisinde. Patikayý takip et...";

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
            radioAudio.Play(); // Ses çalmaya baþla
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
