using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CinematicTrigger : MonoBehaviour
{
    public GameObject[] charactersToAppear;
    public GameObject characterToDisappearAfter;

    public GameObject[] cameras;
    public float[] camDurations;

    public Canvas gameCanvas;
    public GameObject player; // Gerçek oyuncu
    public GameObject dummyPlayer; // Sinematikteki karakter

    public SkinnedMeshRenderer dummyMeshRenderer;
    public string smileBlendShapeName = "Smile";
    private int smileBlendShapeIndex = -1;

    public GameObject blackScreen; // Siyah ekran (UI Image)
    public AudioSource audioSource;
    public AudioClip gunfireClip;

    private bool hasTriggered = false;

    private void Awake()
    {
        foreach (var character in charactersToAppear)
            character.SetActive(false);

        dummyPlayer.SetActive(false);
        blackScreen.SetActive(false);

        // Blend shape indexini al
        if (dummyMeshRenderer != null && dummyMeshRenderer.sharedMesh != null)
        {
            var mesh = dummyMeshRenderer.sharedMesh;
            for (int i = 0; i < mesh.blendShapeCount; i++)
            {
                if (mesh.GetBlendShapeName(i) == smileBlendShapeName)
                {
                    smileBlendShapeIndex = i;
                    break;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;
        StartCoroutine(PlayCinematic());
    }

    IEnumerator PlayCinematic()
    {
        // --- [BAÞLANGIÇTAKÝ SÝYAH EKRAN] ---
        yield return StartCoroutine(FadeBlackScreen(true, 1f));
        yield return new WaitForSeconds(2f);
        yield return StartCoroutine(FadeBlackScreen(false, 1f));

        // Oyuncuyu devre dýþý býrak, dummy'yi aktif et
        player.SetActive(false);
        dummyPlayer.SetActive(true);
        gameCanvas.enabled = false;

        foreach (var character in charactersToAppear)
            character.SetActive(true);

        // Kamera geçiþleri
        for (int i = 0; i < cameras.Length; i++)
        {
            EnableOnlyCamera(i);

            if (i == 5)
            {
                if (smileBlendShapeIndex != -1)
                    dummyMeshRenderer.SetBlendShapeWeight(smileBlendShapeIndex, 100f);

                yield return StartCoroutine(MoveCam6Forward(cameras[i].transform, 3f, 2f));
            }

            yield return new WaitForSeconds(camDurations[i]);
        }

        // Tüm kameralarý kapat
        foreach (var cam in cameras)
            cam.SetActive(false);

        // --- [SÝNEMATÝK SONU: SÝYAH EKRAN ve SES] ---
        yield return StartCoroutine(FadeBlackScreen(true, 1f));

        if (audioSource != null && gunfireClip != null)
        {
            audioSource.clip = gunfireClip;
            audioSource.Play();
        }

        yield return new WaitForSeconds(4f); // siyah ekran + ses süresi

        // Dummy kapat, oyuncu aç
        dummyPlayer.SetActive(false);
        player.SetActive(true);
        gameCanvas.enabled = true;

        if (characterToDisappearAfter != null)
            characterToDisappearAfter.SetActive(false);

        yield return StartCoroutine(FadeBlackScreen(false, 1f));
    }


    void EnableOnlyCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
            cameras[i].SetActive(i == index);
    }
    IEnumerator FadeBlackScreen(bool fadeIn, float duration)
    {
        if (blackScreen == null) yield break;

        Image img = blackScreen.GetComponent<Image>();
        if (img == null) yield break;

        blackScreen.SetActive(true);
        float startAlpha = fadeIn ? 0f : 1f;
        float endAlpha = fadeIn ? 1f : 0f;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            float t = elapsed / duration;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, t);
            img.color = new Color(img.color.r, img.color.g, img.color.b, alpha);
            elapsed += Time.deltaTime;
            yield return null;
        }

        img.color = new Color(img.color.r, img.color.g, img.color.b, endAlpha);

        if (!fadeIn)
            blackScreen.SetActive(false);
    }


    IEnumerator MoveCam6Forward(Transform cam, float distance, float duration)
    {
        Vector3 startPos = cam.position;
        Vector3 direction = cam.forward.normalized;
        Vector3 targetPos = startPos + direction * distance;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            cam.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.position = targetPos;
    }
}
