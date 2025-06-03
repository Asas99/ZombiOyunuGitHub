using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AlfaZombiDeathCinematic : MonoBehaviour
{
    [Header("Zombi Referansý")]
    public GameObject zombiObject;  // Zombi GameObject referansý (Animator deðil!)

    [Header("Oyuncu ve UI")]
    public MonoBehaviour playerControlScript; // Oyuncu kontrol scripti (disable/enable için)
    public GameObject canvasToHide;            // Sinematikte kapanacak canvas

    [Header("Sinematik")]
    public Image blackScreenImage;       // Siyah ekran Image (Canvas altýnda, Canvas baþta kapalý)
    public AudioSource cinematicMusic;   // Sinematik müziði
    public Camera[] cinematicCameras;    // Sýrayla aktif olacak kameralar
    public float cameraDuration = 3f;    // Her kameranýn gösterim süresi

    [Header("Dummy Karakterler")]
    public GameObject[] dummyCharacters; // Sadece sinematikte aktif olacak karakterler

    [Header("Blend Shape Ayarlarý")]
    public SkinnedMeshRenderer faceRenderer;       // Karakter yüzü (SkinnedMeshRenderer)
    public int eyeCloseBlendShapeIndex = 0;        // Göz kýrpma blend shape indeksi
    public int browFrownBlendShapeIndex = 1;       // Kaþ çatma blend shape indeksi
    public float blendShapeTransitionTime = 1.5f;  // Blend shape animasyon süresi

    private bool cinematicStarted = false;

    private Canvas blackScreenCanvas;

    void Start()
    {
        // Siyah ekranýn Canvas componentini bul ve baþlangýçta kapalý yap
        if (blackScreenImage != null)
        {
            blackScreenCanvas = blackScreenImage.GetComponentInParent<Canvas>();
            if (blackScreenCanvas != null)
                blackScreenCanvas.gameObject.SetActive(false);
        }

        // Baþlangýçta dummy karakterler kapalý olsun
        foreach (var dummy in dummyCharacters)
        {
            if (dummy != null)
                dummy.SetActive(false);
        }

        // Baþta tüm sinematik kameralarýný kapat
        DisableAllCameras();

        // Siyah ekran aktif deðil baþta
        if (blackScreenImage != null)
            blackScreenImage.gameObject.SetActive(false);
    }

    void Update()
    {
        if (cinematicStarted) return;

        // Zombi objesi sahneden yoksa sinematik baþlasýn
        if (zombiObject == null)
        {
            cinematicStarted = true;
            StartCoroutine(PlayCinematic());
        }
    }

    IEnumerator PlayCinematic()
    {
        // Oyuncu kontrolü kapat
        if (playerControlScript != null)
            playerControlScript.enabled = false;

        // Canvas gizle
        if (canvasToHide != null)
            canvasToHide.SetActive(false);

        // Cursor gizle ve kilitle
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        // Siyah ekran Canvas'ýný aç ve Image'ý aktif et, renk siyah yap
        if (blackScreenCanvas != null)
            blackScreenCanvas.gameObject.SetActive(true);

        if (blackScreenImage != null)
        {
            blackScreenImage.gameObject.SetActive(true);
            blackScreenImage.color = Color.black;
        }

        yield return new WaitForSeconds(2f);

        // Siyah ekraný kapat ve Canvas'ý kapat
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

        // Sinematik müziði çal
        if (cinematicMusic != null)
            cinematicMusic.Play();

        // Kameralarý sýrayla göster
        for (int i = 0; i < cinematicCameras.Length; i++)
        {
            EnableOnlyCamera(i);

            // Son kamera sýrasýnda blend shape animasyonunu baþlat
            if (i == cinematicCameras.Length - 1)
            {
                yield return StartCoroutine(BlendShapeAnimate());
            }
            else
            {
                yield return new WaitForSeconds(cameraDuration);
            }
        }

        // Tüm kameralarý kapat
        DisableAllCameras();

        // Siyah ekran Canvas'ýný aç ve fade ile yavaþça karart
        if (blackScreenCanvas != null)
            blackScreenCanvas.gameObject.SetActive(true);

        if (blackScreenImage != null)
        {
            blackScreenImage.gameObject.SetActive(true);
            yield return StartCoroutine(FadeToBlack(2f));
        }

        // Ana menü sahnesine dön (sahne indeksi 0)
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
