using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

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
    public string smileBlendShapeName = "Mutluolma";
    private int smileBlendShapeIndex = -1;

    public GameObject blackScreen;
    public AudioSource audioSource;
    public AudioClip gunfireClip;
    public AudioClip cinematicMusicClip;
    public AudioClip reloadClip;
    public AudioClip extraSoundClip; // Ekstra ses

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

    public SkinnedMeshRenderer alexMeshRenderer;
    public string blinkBlendShapeName = "Gozkırpma";
    private int blinkBlendShapeIndex = -1;

    public Transform playerTransform;
    public Transform nurseTransform;
    public float kissDistance = 4f;
    private bool kissTriggered = false;

    public GameObject dialoguePanel;
    public TextMeshProUGUI dialogueText;

    private bool hasTriggered = false;
    private bool followPlayer = false;
    private bool cinematicFinished = false;

    private bool extraSoundPlayed = false; // Ek sesin çalınıp çalınmadığını kontrol eder

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

        if (alexMeshRenderer != null && alexMeshRenderer.sharedMesh != null)
        {
            var mesh = alexMeshRenderer.sharedMesh;
            for (int i = 0; i < mesh.blendShapeCount; i++)
            {
                if (mesh.GetBlendShapeName(i) == blinkBlendShapeName)
                {
                    blinkBlendShapeIndex = i;
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

        dialoguePanel.SetActive(false);

        StartCoroutine(AlexBlinkSequence());
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

            if (i == 1 && !extraSoundPlayed && extraSoundClip != null && audioSource != null)
            {
                audioSource.PlayOneShot(extraSoundClip);
                extraSoundPlayed = true;
            }

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
        StartCoroutine(WomanShootAndKillSequence());

        SetBlackScreen(false);
        cinematicFinished = true;
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
        if (!womanAnimator.enabled)
            womanAnimator.enabled = true;

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

        womanAnimator.SetBool("Shoot", false);
    }

    IEnumerator MuzzleFlashEffect(GameObject flash)
    {
        flash.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        flash.SetActive(false);
    }

    IEnumerator AlexBlinkSequence()
    {
        while (true)
        {
            if (blinkBlendShapeIndex != -1)
            {
                for (float t = 0; t <= 1f; t += Time.deltaTime * 10f)
                {
                    float weight = Mathf.Lerp(0f, 100f, t);
                    alexMeshRenderer.SetBlendShapeWeight(blinkBlendShapeIndex, weight);
                    yield return null;
                }

                for (float t = 0; t <= 1f; t += Time.deltaTime * 10f)
                {
                    float weight = Mathf.Lerp(100f, 0f, t);
                    alexMeshRenderer.SetBlendShapeWeight(blinkBlendShapeIndex, weight);
                    yield return null;
                }
            }

            yield return new WaitForSeconds(Random.Range(3f, 7f));
        }
    }

    void Update()
    {
        if (cinematicFinished && !kissTriggered && Vector3.Distance(playerTransform.position, nurseTransform.position) <= kissDistance)
        {
            kissTriggered = true;
            StartCoroutine(TriggerKiss());
        }

        if (followPlayer)
        {
            float distance = Vector3.Distance(playerTransform.position, nurseTransform.position);
            if (distance > 2.5f)
            {
                Vector3 direction = (playerTransform.position - nurseTransform.position).normalized;
                nurseTransform.position += direction * Time.deltaTime * 2f;
                nurseTransform.rotation = Quaternion.LookRotation(direction);

                womanAnimator.SetBool("Walk", true);
            }
            else
            {
                womanAnimator.SetBool("Walk", false);
            }
        }
    }

    IEnumerator TriggerKiss()
    {
        womanAnimator.SetBool("Kiss", true);
        dialoguePanel.SetActive(true);
        dialogueText.text = "İyi ki buraya geldiğini duymuşum... Bensiz başaramazsın Alex..";
        yield return new WaitForSeconds(10f);
        womanAnimator.SetBool("Kiss", false);
        dialoguePanel.SetActive(false);

        followPlayer = true;
    }
}
