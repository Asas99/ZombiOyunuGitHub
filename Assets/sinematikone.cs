using System.Collections;
using UnityEngine;
using TMPro;

public class CinematicAndElevator : MonoBehaviour
{
    public Camera cinematicCam1;
    public Camera cinematicCam2;
    public Camera cinematicCam3;
    public Camera mainCam;
    public GameObject playerController;
    public Canvas gameplayCanvas;
    public Canvas cinematicCanvas;
    public TextMeshProUGUI dialogueText;
    public TextMesh promptText;
    public GameObject blackScreen;
    public Transform spawnPoint;
    public Transform player;
    public Transform elevator;
    public GameObject alphaZombie;
    public string[] dialogues;
    public float cinematicDuration = 10.0f;
    public float interactionDistance = 3.0f;
    public float camera3PanDuration = 2.0f; // 2 saniyeye düşürüldü

    [Header("Sesler")]
    public AudioClip camera1Sound;
    public AudioClip zombieSound;

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

        // Başta 1 saniye siyah ekran
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(1f);
        blackScreen.SetActive(false);

        gameplayCanvas.gameObject.SetActive(false);
        cinematicCanvas.gameObject.SetActive(true);
        cinematicCam1.gameObject.SetActive(true);
        mainCam.gameObject.SetActive(false);
        playerController.SetActive(false);

        // Kamera 1 sesi
        if (camera1Sound != null)
            AudioSource.PlayClipAtPoint(camera1Sound, cinematicCam1.transform.position);

        // Diyaloglar
        for (int i = 0; i < dialogues.Length; i++)
        {
            dialogueText.text = dialogues[i];
            yield return new WaitForSeconds(cinematicDuration / dialogues.Length);
        }

        // Kamera 1 kapanışı ve diyalog panel gizleme
        dialogueText.text = "";
        cinematicCanvas.gameObject.SetActive(false); // Paneli de kapat
        cinematicCam1.gameObject.SetActive(false);

        // Kamera 2
        cinematicCam2.gameObject.SetActive(true);
        yield return new WaitForSeconds(2.0f);
        cinematicCam2.gameObject.SetActive(false);

        // Kamera 3
        cinematicCanvas.gameObject.SetActive(false); // Diyalog panel gözükmesin
        cinematicCam3.gameObject.SetActive(true);

        // Zombi animasyonu ve sesi
        alphaZombie.GetComponent<Animator>().SetBool("İşaret", true);
        if (zombieSound != null)
            AudioSource.PlayClipAtPoint(zombieSound, alphaZombie.transform.position);

        // Kamera 3 pan hareketi (2 saniye)
        float elapsedTime = 0.0f;
        Vector3 initialPosition = cinematicCam3.transform.position;
        Vector3 targetPosition = initialPosition + new Vector3(0, 0, -3); // Daha kısa mesafe istersen azalt

        while (elapsedTime < camera3PanDuration)
        {
            cinematicCam3.transform.position = Vector3.Lerp(initialPosition, targetPosition, elapsedTime / camera3PanDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        alphaZombie.GetComponent<Animator>().SetBool("İşaret", false);

        // Kamera 3 bittikten sonra 2 saniye siyah ekran
        blackScreen.SetActive(true);
        yield return new WaitForSeconds(2f);
        blackScreen.SetActive(false);

        // Her şey kapanır, kontrol geri gelir
        cinematicCam3.gameObject.SetActive(false);
        gameplayCanvas.gameObject.SetActive(true);
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
