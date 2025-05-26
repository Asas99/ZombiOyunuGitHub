using UnityEngine;

public class DoorPushSimple : MonoBehaviour
{
    public Transform doorTransform;
    public Transform characterTransform;

    public float pushAmount = 5f;
    public float pushSpeed = 2f;
    public float pushInterval = 3f;

    public float knockbackDistance = 0.5f;
    public float returnDelay = 0.5f;
    public float returnSpeed = 1f;

    private Vector3 originalRotation;
    private bool isPushing = false;
    private Vector3 characterStartPos;

    void Start()
    {
        originalRotation = doorTransform.localEulerAngles;
        characterStartPos = characterTransform.position;
        InvokeRepeating("StartPush", 1f, pushInterval);
    }

    void StartPush()
    {
        if (!isPushing)
            StartCoroutine(PushDoor());
    }

    System.Collections.IEnumerator PushDoor()
    {
        isPushing = true;

        // Kapýyý aç
        float elapsed = 0f;
        while (elapsed < 0.5f)
        {
            float angle = Mathf.Lerp(0, pushAmount, elapsed / 0.5f);
            doorTransform.localEulerAngles = originalRotation + new Vector3(0, angle, 0);
            elapsed += Time.deltaTime * pushSpeed;
            yield return null;
        }

        // Karakteri geri it
        Vector3 knockbackPos = characterTransform.position + doorTransform.forward * knockbackDistance;
        characterTransform.position = knockbackPos;

        // Bekle
        yield return new WaitForSeconds(returnDelay);

        // Karakter eski pozisyonuna dönsün
        elapsed = 0f;
        Vector3 startReturn = characterTransform.position;
        while (elapsed < 1f)
        {
            characterTransform.position = Vector3.Lerp(startReturn, characterStartPos, elapsed / returnSpeed);
            elapsed += Time.deltaTime;
            yield return null;
        }

        // Geri kapat
        elapsed = 0f;
        while (elapsed < 0.5f)
        {
            float angle = Mathf.Lerp(pushAmount, 0, elapsed / 0.5f);
            doorTransform.localEulerAngles = originalRotation + new Vector3(0, angle, 0);
            elapsed += Time.deltaTime * pushSpeed;
            yield return null;
        }

        doorTransform.localEulerAngles = originalRotation;
        isPushing = false;
    }
}
