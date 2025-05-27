using UnityEngine;
using System.Collections;

public class CinematicController : MonoBehaviour
{
    [Header("Referanslar")]
    public GameObject canvas;
    public GameObject player;
    public MonoBehaviour playerController;
    public Camera playerCamera;
    public Camera cam1, cam2, cam3;
    public AudioSource cinematicAudio; // 🎵 Ses kaynağı

    [Header("Ayarlar")]
    public float camSwitchDelay = 2f;
    public float cam3MoveDuration = 3.5f;
    public float cam3MoveSpeed = 2f;

    private bool hasPlayed = false;

    private void Start()
    {
        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = false;

        if (cinematicAudio != null)
            cinematicAudio.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!hasPlayed && other.CompareTag("Player"))
        {
            hasPlayed = true;
            StartCoroutine(PlayCinematic());
        }
    }

    IEnumerator PlayCinematic()
    {
        canvas.SetActive(false);
        playerController.enabled = false;
        playerCamera.enabled = false;

        // 🎵 Ses başlat
        if (cinematicAudio != null)
            cinematicAudio.Play();

        cam1.enabled = true;
        yield return new WaitForSeconds(camSwitchDelay);
        cam1.enabled = false;

        cam2.enabled = true;
        yield return new WaitForSeconds(camSwitchDelay);
        cam2.enabled = false;

        cam3.enabled = true;
        float elapsed = 0f;
        while (elapsed < cam3MoveDuration)
        {
            cam3.transform.position -= cam3.transform.forward * cam3MoveSpeed * Time.deltaTime;
            elapsed += Time.deltaTime;
            yield return null;
        }
        cam3.enabled = false;

        // 🎵 Ses durdur (isteğe bağlı)
        if (cinematicAudio != null && cinematicAudio.isPlaying)
            cinematicAudio.Stop();

        playerCamera.enabled = true;
        playerController.enabled = true;
        canvas.SetActive(true);
    }
}
