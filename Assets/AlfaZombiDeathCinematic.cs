using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlfaZombiDeathCinematic : MonoBehaviour
{
    [Header("Zombi Referans�")]
    public GameObject zombiObject;  // Zombi GameObject referans� (Animator de�il!)

    [Header("Oyuncu ve UI")]
    public MonoBehaviour playerControlScript; // Oyuncu kontrol scripti (disable/enable i�in)
    public GameObject canvasToHide;            // Sinematikte kapanacak canvas

    [Header("Sinematik")]
    public Image blackScreenImage;       // Siyah ekran Image (Canvas alt�nda, Canvas ba�ta kapal�)
    public AudioSource cinematicMusic;   // Sinematik m�zi�i
    public Camera[] cinematicCameras;    // S�rayla aktif olacak kameralar
    public float cameraDuration = 3f;    // Her kameran�n g�sterim s�resi

    [Header("Dummy Karakterler")]
    public GameObject[] dummyCharacters; // Sadece sinematikte aktif olacak karakterler

    [Header("Blend Shape Ayarlar�")]
    public SkinnedMeshRenderer faceRenderer;       // Karakter y�z� (SkinnedMeshRenderer)
    public int eyeCloseBlendShapeIndex = 0;        // G�z k�rpma blend shape indeksi
    public int browFrownBlendShapeIndex = 1;       // Ka� �atma blend shape indeksi
    public float blendShapeTransitionTime = 1.5f;  // Blend shape animasyon s�resi

    private bool cinematicStarted = false;

    private Canvas blackScreenCanvas;

    void Start()
    {
        // Siyah ekran�n Canvas componentini bul ve ba�lang��ta kapal� yap
        if (blackScreenImage != null)
        {
            blackScreenCanvas = blackScreenImage.GetComponentInParent<Canvas>();
            if (blackScreenCanvas != null)
                blackScreenCanvas.gameObject.SetActive(false);
        }

        // Ba�lang��ta dummy karakterler kapal� olsun
        foreach (var dummy in dummyCharacters)
        {
            if (dummy != null)
                dummy.SetActive(false);
        }

        // Ba�ta t�m sinematik kameralar�n� kapat
        DisableAllCameras();

        // Siyah ekran aktif de�il ba�ta
        if (blackScreenImage != null)
            blackScreenImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (cinematicStarted) return;

        // Zombi objesi sahneden yoksa sinematik ba�las�n
        if (zombiObject == null)
        {
            cinematicStarted = true;
            StartCoroutine(PlayCinematic());
        }
    }

    IEnumerator PlayCinematic()
    {
        // Oyuncu kontrol� kapat
        if (playerControlScript != null)
            playerControlScript.enabled = false;

        // Canvas gizle
        if (canvasToHide != null)
            canvasToHide.SetActive(false);

        // Cursor gizle ve kilitle
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Siyah ekran Canvas'�n� a� ve Image'� aktif et, renk siyah yap
        if (blackScreenCanvas != null)
            blackScreenCanvas.gameObject.SetActive(true);

        if (blackScreenImage != null)
        {
            blackScreenImage.gameObject.SetActive(true);
            blackScreenImage.color = Color.black;
        }

        yield return new WaitForSeconds(2f);

        // Siyah ekran� kapat ve Canvas'� kapat
        if (blackScreenImage != null)
            blackScreenImage.gameObject.SetActive(false);

        if (blackScreenCanvas != null)
            blackScreenCanvas.gameObject.SetActive(false);

        // Dummy karakterleri aktif et
        foreach (var dummy in dummyCharacters)
        {
            if (dummy != null)
                dummy.SetActive(true);
        }

        // Sinematik m�zi�i �al
        if (cinematicMusic != null)
            cinematicMusic.Play();

        // Kameralar� s�rayla g�ster
        for (int i = 0; i < cinematicCameras.Length; i++)
        {
            EnableOnlyCamera(i);

            // Son kamera s�ras�nda blend shape animasyonunu ba�lat
            if (i == cinematicCameras.Length - 1)
            {
                yield return StartCoroutine(BlendShapeAnimate());
            }
            else
            {
                yield return new WaitForSeconds(cameraDuration);
            }
        }

        // T�m kameralar� kapat
        DisableAllCameras();

        // Siyah ekran Canvas'�n� a� ve fade ile yava��a karart
        if (blackScreenCanvas != null)
            blackScreenCanvas.gameObject.SetActive(true);

        if (blackScreenImage != null)
        {
            blackScreenImage.gameObject.SetActive(true);
            yield return StartCoroutine(FadeToBlack(2f));
        }

        // Ana men� sahnesine d�n (sahne indeksi 0)
        SceneManager.LoadScene(0);
    }

    IEnumerator BlendShapeAnimate()
    {
        float elapsed = 0f;

        while (elapsed < blendShapeTransitionTime)
        {
            elapsed += Time.deltaTime;
            float weight = Mathf.Lerp(0, 100, elapsed / blendShapeTransitionTime);

            if (faceRenderer != null)
            {
                faceRenderer.SetBlendShapeWeight(eyeCloseBlendShapeIndex, weight);
                faceRenderer.SetBlendShapeWeight(browFrownBlendShapeIndex, weight);
            }
            yield return null;
        }
    }

    IEnumerator FadeToBlack(float duration)
    {
        float elapsed = 0f;
        Color color = blackScreenImage.color;
        color.a = 0f;
        blackScreenImage.color = color;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsed / duration);
            blackScreenImage.color = color;
            yield return null;
        }
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
