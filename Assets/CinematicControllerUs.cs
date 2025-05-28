
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
    public GameObject player;
    public GameObject dummyPlayer;

    public SkinnedMeshRenderer dummyMeshRenderer;
    public string smileBlendShapeName = "Smile";
    private int smileBlendShapeIndex = -1;

    public GameObject blackScreen;
    public AudioSource audioSource;
    public AudioClip gunfireClip;
    public AudioClip cinematicMusicClip;
    public AudioClip reloadClip;

    public GameObject bandit;
    public Animator banditAnimator;
    public string banditDeathTrigger = "Die";

    public Animator womanAnimator;
    public GameObject womanGunMuzzleFlash;
    public AudioClip womanGunfireClip;

    public GameObject[] enemyBandits;
    public Animator[] enemyAnimators;
    public string enemyDeathTrigger = "Die";

    public GameObject[] enemyGunMuzzleFlashes;
    public AudioClip enemyGunfireClip;

    private bool hasTriggered = false;

    private void Awake()
    {
        foreach (var character in charactersToAppear)
            character.SetActive(false);

        dummyPlayer.SetActive(false);
        blackScreen.SetActive(false);

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

        if (womanGunMuzzleFlash != null)
            womanGunMuzzleFlash.SetActive(false);

        foreach (var flash in enemyGunMuzzleFlashes)
        {
            if (flash != null)
                flash.SetActive(false);
        }

        // Kadının Animator'ını başlangıçta devre dışı bırak
        if (womanAnimator != null)
            womanAnimator.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (hasTriggered || !other.CompareTag("Player")) return;

        hasTriggered = true;
        StartCoroutine(PlayCinematic());
    }

    IEnumerator PlayCinematic()
    {
        SetBlackScreen(true);
        yield return new WaitForSeconds(2f);
        SetBlackScreen(false);

        if (audioSource != null && cinematicMusicClip != null)
        {
            audioSource.clip = cinematicMusicClip;
            audioSource.loop = false;
            audioSource.Play();
        }

        player.SetActive(false);
        dummyPlayer.SetActive(true);
        gameCanvas.gameObject.SetActive(false);

        foreach (var character in charactersToAppear)
            character.SetActive(true);

        for (int i = 0; i < cameras.Length; i++)
        {
            EnableOnlyCamera(i);

            if (i == 2 && charactersToAppear.Length > 0)
            {
                StartCoroutine(MoveCharacterForward(charactersToAppear[0].transform, 1.5f, 2f));
                if (reloadClip != null)
                    AudioSource.PlayClipAtPoint(reloadClip, cameras[i].transform.position);
            }

            if (i == 5)
            {
                if (smileBlendShapeIndex != -1)
                    dummyMeshRenderer.SetBlendShapeWeight(smileBlendShapeIndex, 100f);

                StartCoroutine(MoveCam6Forward(cameras[i].transform, 2f, camDurations[i]));
            }

            yield return new WaitForSeconds(camDurations[i]);
        }

        foreach (var cam in cameras)
            cam.SetActive(false);

        dummyPlayer.SetActive(false);
        player.SetActive(true);
        gameCanvas.gameObject.SetActive(true);

        if (characterToDisappearAfter != null)
            characterToDisappearAfter.SetActive(false);

        if (banditAnimator != null)
            banditAnimator.SetTrigger(banditDeathTrigger);

        if (audioSource != null && gunfireClip != null)
            audioSource.PlayOneShot(gunfireClip);

        StartCoroutine(ContinueMusicForDuration(15f));

        // Kadın banditleri vurur
        StartCoroutine(WomanShootAndKillSequence());

        SetBlackScreen(false);
    }

    void EnableOnlyCamera(int index)
    {
        for (int i = 0; i < cameras.Length; i++)
            cameras[i].SetActive(i == index);
    }

    void SetBlackScreen(bool active)
    {
        if (blackScreen == null) return;

        Image img = blackScreen.GetComponent<Image>();
        if (img != null)
            img.color = new Color(img.color.r, img.color.g, img.color.b, active ? 1f : 0f);

        blackScreen.SetActive(active);
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

    IEnumerator MoveCharacterForward(Transform character, float distance, float duration)
    {
        Vector3 startPos = character.position;
        Vector3 targetPos = startPos + character.forward * distance;

        float elapsed = 0f;
        while (elapsed < duration)
        {
            character.position = Vector3.Lerp(startPos, targetPos, elapsed / duration);
            elapsed += Time.deltaTime;
            yield return null;
        }

        character.position = targetPos;
    }

    IEnumerator ContinueMusicForDuration(float duration)
    {
        yield return new WaitForSeconds(duration);

        if (audioSource != null && audioSource.isPlaying)
            audioSource.Stop();
    }

    IEnumerator WomanShootAndKillSequence()
    {
        // Animator'ı aktif hale getir
        if (!womanAnimator.enabled)
            womanAnimator.enabled = true;

        // Shoot başlasın
        womanAnimator.SetBool("Shoot", true);

        yield return new WaitForSeconds(1f);

        for (int i = 0; i < enemyBandits.Length; i++)
        {
            Vector3 dir = enemyBandits[i].transform.position - charactersToAppear[0].transform.position;
            dir.y = 0;
            charactersToAppear[0].transform.rotation = Quaternion.LookRotation(dir);

            if (womanGunMuzzleFlash != null)
                StartCoroutine(MuzzleFlashEffect(womanGunMuzzleFlash));

            if (audioSource != null && womanGunfireClip != null)
                audioSource.PlayOneShot(womanGunfireClip);

            if (i < enemyGunMuzzleFlashes.Length && enemyGunMuzzleFlashes[i] != null)
                StartCoroutine(MuzzleFlashEffect(enemyGunMuzzleFlashes[i]));

            if (audioSource != null && enemyGunfireClip != null)
                audioSource.PlayOneShot(enemyGunfireClip);

            yield return new WaitForSeconds(1.5f);

            if (enemyAnimators[i] != null)
                enemyAnimators[i].SetTrigger(enemyDeathTrigger);

            yield return new WaitForSeconds(2f);
        }

        // Tüm düşmanlar öldü → Shoot kapansın → Idle'a dön
        womanAnimator.SetBool("Shoot", false);
    }

    IEnumerator MuzzleFlashEffect(GameObject flash)
    {
        flash.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        flash.SetActive(false);
    }
}
