using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CinematicTriggerrr : MonoBehaviour
{
    public GameObject playerController;
    public GameObject canvasToHide;
    public GameObject blackScreenCanvas;
    public Image blackScreenImage;

    public AudioSource cinematicMusic;

    public Camera playerMainCamera;
    public Camera[] cinematicCameras;
    public float cameraDuration = 3f;

    private bool hasTriggered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!hasTriggered && other.CompareTag("Player"))
        {
            hasTriggered = true;
            StartCoroutine(PlayCinematic());
        }
    }

    IEnumerator PlayCinematic()
    {
        // 1. Siyah ekranı göster ve canvasları ayarla
        if (blackScreenCanvas != null)
            blackScreenCanvas.SetActive(true);
        if (blackScreenImage != null)
            blackScreenImage.color = new Color(0, 0, 0, 1);

        if (canvasToHide != null)
            canvasToHide.SetActive(false);
        if (playerController != null)
            playerController.SetActive(false);

        yield return new WaitForSeconds(0.5f);

        // 2. Cinematic kamera 0’ı aktif etmeden önce oyuncu kamerasını kapatma!
        if (cinematicCameras.Length > 0 && cinematicCameras[0] != null)
            cinematicCameras[0].gameObject.SetActive(true);

        yield return null; // 1 frame bekle

        if (playerMainCamera != null)
            playerMainCamera.gameObject.SetActive(false);

        // 3. Siyah ekranı yavaşça kapat
        if (blackScreenImage != null)
            blackScreenImage.CrossFadeAlpha(0f, 1f, false);

        // 4. Müzik başlat
        if (cinematicMusic != null)
            cinematicMusic.Play();

        // 5. Kamera geçişleri
        for (int i = 0; i < cinematicCameras.Length; i++)
        {
            EnableOnlyCamera(i);
            yield return new WaitForSeconds(cameraDuration);
        }

        // 6. Son siyah ekran
        if (blackScreenImage != null)
        {
            blackScreenImage.CrossFadeAlpha(1f, 0.5f, false);
            yield return new WaitForSeconds(0.5f);
        }

        // 7. Oyuncu kamerasını geri aç → sonra cinematic kameraları kapat
        if (playerMainCamera != null)
            playerMainCamera.gameObject.SetActive(true);
        DisableAllCameras();

        // 8. Canvas ve kontrol geri gelsin
        if (blackScreenImage != null)
            blackScreenImage.CrossFadeAlpha(0f, 1f, false);
        if (blackScreenCanvas != null)
            blackScreenCanvas.SetActive(false);

        if (playerController != null)
            playerController.SetActive(true);
        if (canvasToHide != null)
            canvasToHide.SetActive(true);
    }

    void EnableOnlyCamera(int index)
    {
        for (int i = 0; i < cinematicCameras.Length; i++)
        {
            cinematicCameras[i].gameObject.SetActive(i == index);
        }
    }

    void DisableAllCameras()
    {
        foreach (var cam in cinematicCameras)
        {
            cam.gameObject.SetActive(false);
        }
    }
}
