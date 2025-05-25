using System.Collections;
using UnityEngine;

public class WakeUpCutscene : MonoBehaviour
{
    [Header("Cameras")]
    public Camera familyPhotoCam;    // Aile tablosu kamerası
    public float familyPhotoCamStartFOV = 60f;
    public float familyPhotoCamTargetFOV = 30f;
    public float familyPhotoFOVDuration = 2f;

    public Camera playerCam;
    public Camera cinematicCam1;     // Alex'e bakan
    public Camera cinematicCam2;     // Radyoya zoom
    public float radioCamStartFOV = 60f;
    public float radioCamTargetFOV = 30f;
    public float radioFOVDuration = 2f;

    [Header("Player & BlendShape")]
    public GameObject playerControl; // Oyuncu kontrol objesi
    public SkinnedMeshRenderer faceRenderer;
    public int eyeBlinkBlendShapeIndex = 0;

    [Header("Radyo")]
    public AudioSource radioAudio;

    [Header("Timing")]
    public float cam1Duration = 2f;
    public float cam1ReturnDuration = 1.5f;
    public float eyeOpenDuration = 1.5f;

    [Header("UI & Sleeping Object")]
    public GameObject canvasUI;          // Sinematik boyunca kapalı kalacak Canvas
    public GameObject sleepingManObj;   // Uyuyan adam objesi

    void Start()
    {
        playerControl.SetActive(false);
        playerCam.enabled = false;
        cinematicCam1.enabled = false;
        cinematicCam2.enabled = false;
        familyPhotoCam.enabled = true;

        familyPhotoCam.fieldOfView = familyPhotoCamStartFOV;
        cinematicCam2.fieldOfView = radioCamStartFOV;

        faceRenderer.SetBlendShapeWeight(eyeBlinkBlendShapeIndex, 100f);

        canvasUI.SetActive(false);
        sleepingManObj.SetActive(true);

        StartCoroutine(PlayCutscene());
    }

    IEnumerator PlayCutscene()
    {
        // 1. Aile tablosu kamerası FOV azalıyor
        yield return StartCoroutine(AnimateFOV(familyPhotoCam, familyPhotoCamStartFOV, familyPhotoCamTargetFOV, familyPhotoFOVDuration));

        // Aile tablosu kamerası kapat, Alex kamerası aç
        familyPhotoCam.enabled = false;
        cinematicCam1.enabled = true;

        // 2. Alex kamerası sabit süre
        yield return new WaitForSeconds(cam1Duration);

        // 3. Radyo kamerası aktif, FOV azalıyor
        cinematicCam1.enabled = false;
        cinematicCam2.enabled = true;
        yield return StartCoroutine(AnimateFOV(cinematicCam2, radioCamStartFOV, radioCamTargetFOV, radioFOVDuration));

        // 4. Tekrar Alex kamerası
        cinematicCam2.enabled = false;
        cinematicCam1.enabled = true;
        yield return new WaitForSeconds(cam1ReturnDuration);

        // 5. Radyo sesi
        radioAudio.Play();

        // 6. Göz açma animasyonu
        yield return StartCoroutine(AnimateEyeBlink());

        // 7. Oyuncuya geçiş
        cinematicCam1.enabled = false;
        playerCam.enabled = true;
        playerControl.SetActive(true);

        canvasUI.SetActive(true);
        sleepingManObj.SetActive(false);

        Debug.Log("Sinematik bitti, oyuncuya geçildi.");
    }

    IEnumerator AnimateFOV(Camera cam, float startFOV, float targetFOV, float duration)
    {
        float timer = 0f;
        while (timer < duration)
        {
            cam.fieldOfView = Mathf.Lerp(startFOV, targetFOV, timer / duration);
            timer += Time.deltaTime;
            yield return null;
        }
        cam.fieldOfView = targetFOV;
    }

    IEnumerator AnimateEyeBlink()
    {
        float timer = 0f;
        while (timer < eyeOpenDuration)
        {
            float value = Mathf.Lerp(100f, 0f, timer / eyeOpenDuration);
            faceRenderer.SetBlendShapeWeight(eyeBlinkBlendShapeIndex, value);
            timer += Time.deltaTime;
            yield return null;
        }
        faceRenderer.SetBlendShapeWeight(eyeBlinkBlendShapeIndex, 0f);
    }
}
