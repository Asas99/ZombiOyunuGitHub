using UnityEngine;

public class CamAdjustDistance : MonoBehaviour
{
    public Transform target; // Karakterin kafasýnýn veya üst gövdesinin transform'u
    public float maxDistance = 5f;
    public float minDistance = 1f;
    public LayerMask wallLayer;

    private Vector3 cameraOffset;

    void Start()
    {
        cameraOffset = transform.localPosition; // Kamera, karakterin child'ý olduðundan local position kullanýlmalý
    }

    void LateUpdate()
    {
        Vector3 desiredCameraPos = target.TransformPoint(cameraOffset); // hedef pozisyon (normalde olmasý gereken yer)
        Vector3 direction = desiredCameraPos - target.position;
        float distance = direction.magnitude;

        // Raycast yap
        if (Physics.Raycast(target.position, direction.normalized, out RaycastHit hit, maxDistance, wallLayer))
        {
            float hitDistance = Mathf.Clamp(hit.distance, minDistance, maxDistance);
            transform.position = target.position + direction.normalized * hitDistance;
        }
        else
        {
            transform.position = desiredCameraPos;
        }

        transform.LookAt(target);
    }
}
