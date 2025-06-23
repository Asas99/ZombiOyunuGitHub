using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SinematikController : MonoBehaviour
{
    public Camera cinematicCamera1;
    public Camera cinematicCamera2;
    public Camera mainCamera;
    public GameObject player;
    public Canvas uiCanvas;

    public float camera1MoveDuration = 3f;
    public float camera1RotateDuration = 2f;
    public float camera2RotateDuration = 2f;

    private bool cinematicPlayed = false;

    void Start()
    {
        if (!cinematicPlayed)
        {
            StartCoroutine(PlayCinematic());
        }
    }

    IEnumerator PlayCinematic()
    {
        cinematicPlayed = true;

        // 🎯 Mouse'u kilitle ve gizle
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        // 🎬 Başlangıç hazırlığı
        mainCamera.enabled = false;
        cinematicCamera1.enabled = true;
        cinematicCamera2.enabled = false;
        uiCanvas.enabled = false;

        // 🕒 Zamanı durdur
        Time.timeScale = 0f;

        // Kamera 1 ileri gitsin (realtime)
        Vector3 startPos = cinematicCamera1.transform.position;
        Vector3 endPos = startPos + cinematicCamera1.transform.forward * 5f;
        float elapsed = 0f;

       

        // Kamera 1 sağa dönsün
        elapsed = 0f;
        Quaternion startRot = cinematicCamera1.transform.rotation;
        Quaternion endRot = startRot *  Quaternion.Euler(0, 120, 0);

        while (elapsed < camera1RotateDuration)
        {
            float delta = Time.unscaledDeltaTime;
            cinematicCamera1.transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / camera1RotateDuration);
            elapsed += delta;
            yield return null;
        }

        // 1 saniye bekle
        yield return new WaitForSecondsRealtime(1f);

        // Kamera 2 aktif
        cinematicCamera1.enabled = false;
        cinematicCamera2.enabled = true;

        // Kamera 2 sola dönsün
        elapsed = 0f;
        startRot = cinematicCamera2.transform.rotation;
        endRot = startRot * Quaternion.Euler(0, -180, 0);

        while (elapsed < camera2RotateDuration)
        {
            float delta = Time.unscaledDeltaTime;
            cinematicCamera2.transform.rotation = Quaternion.Slerp(startRot, endRot, elapsed / camera2RotateDuration);
            elapsed += delta;
            yield return null;
        }

        // 1 saniye bekle
        yield return new WaitForSecondsRealtime(1f);

        // 🎬 Sinematik bitti
        cinematicCamera2.enabled = false;
        mainCamera.enabled = true;
        uiCanvas.enabled = true;

        // 🕒 Zamanı geri başlat
        Time.timeScale = 1f;

        // 🎯 Mouse serbest ve görünür hale gelsin
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        // (İsteğe bağlı) oyuncu kontrol scripti açılabilir
        // player.GetComponent<PlayerController>().enabled = true;
    }
}
