using UnityEngine;

public class DoorPushSimple : MonoBehaviour
{
    public Transform doorTransform;
    public float pushAmount = 5f;
    public float pushSpeed = 2f;
    public float pushInterval = 3f;

    private Vector3 originalRotation;
    private bool isPushing = false;

    void Start()
    {
        originalRotation = doorTransform.localEulerAngles;
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

        // Kapýyý hafifçe aç
        float elapsed = 0f;
        while (elapsed < 0.5f)
        {
            float angle = Mathf.Lerp(0, pushAmount, elapsed / 0.5f);
            doorTransform.localEulerAngles = originalRotation + new Vector3(0, angle, 0);
            elapsed += Time.deltaTime * pushSpeed;
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
