
using System.Collections;
using UnityEngine;
using TMPro;

public class CinematicAndElevator : MonoBehaviour
{
    public Camera cinematicCam;
    public Camera mainCam;
    public GameObject playerController;
    public Canvas gameplayCanvas;
    public Canvas cinematicCanvas;
    public TextMeshProUGUI dialogueText;
    public TextMesh  promptText;
    public GameObject blackScreen;
    public Transform spawnPoint;
    public Transform player;
    public Transform elevator;
    public string[] dialogues;
    public float cinematicDuration = 10.0f;
    public float interactionDistance = 3.0f;

    private bool cinematicActive = false;
    private bool canInteractWithElevator = false;

    void Start()
    {
        cinematicCanvas.gameObject.SetActive(false);
        promptText.gameObject.SetActive(false);
        blackScreen.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !cinematicActive)
        {
            StartCoroutine(StartCinematic());
        }
    }

    IEnumerator StartCinematic()
    {
        cinematicActive = true;
        gameplayCanvas.gameObject.SetActive(false);
        cinematicCanvas.gameObject.SetActive(true);
        cinematicCam.gameObject.SetActive(true);
        mainCam.gameObject.SetActive(false);
        playerController.SetActive(false);

        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueText.text = dialogues[i];
            yield return new WaitForSeconds(cinematicDuration / dialogues.Length);
        }

        dialogueText.text = "";
        cinematicCanvas.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
        cinematicCam.gameObject.SetActive(false);
        mainCam.gameObject.SetActive(true);
        playerController.SetActive(true);

        canInteractWithElevator = true;
    }

    void Update()
    {
        if (canInteractWithElevator && Vector3.Distance(player.position, elevator.position) <= interactionDistance)
        {
            promptText.gameObject.SetActive(true);

            if (Input.GetKeyDown(KeyCode.E))
            {
                StartCoroutine(UseElevator());
            }
        }
        else
        {
            promptText.gameObject.SetActive(false);
        }
    }

    IEnumerator UseElevator()
    {
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        player.position = spawnPoint.position;
        blackScreen.SetActive(false);
    }
}
