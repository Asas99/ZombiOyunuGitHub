using UnityEngine;

public class GunController : MonoBehaviour
{
    [Header("Fire Settings")]
    public float fireRate = 0.2f;
    private float nextFireTime = 0f;

    [Header("Recoil")]
    public float recoilAmount = 5f;
    public float recoilSpeed = 10f;
    private Vector3 originalPosition;

    [Header("Muzzle Flash")]
    public GameObject muzzleFlashPrefab;
    public Transform muzzlePoint;

    [Header("Sound")]
    public AudioSource audioSource;
    public AudioClip fireSound;

    void Start()
    {
        originalPosition = transform.localPosition;
    }

    void Update()
    {
        // Sol týklama ile ateþ etme kontrolü
        if (Input.GetButton("Fire1") && Time.time >= nextFireTime)
        {
            Fire();
            nextFireTime = Time.time + fireRate;
        }

        // Recoil (geri tepme)
        transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, Time.deltaTime * recoilSpeed);
    }

    void Fire()
    {
        // Geri tepme
        transform.localPosition -= Vector3.forward * recoilAmount;

        // Muzzle Flash (ateþ çýkýþý efekti)
        if (muzzleFlashPrefab && muzzlePoint)
        {
            GameObject flash = Instantiate(muzzleFlashPrefab, muzzlePoint.position, muzzlePoint.rotation);
            Destroy(flash, 0.1f); // Efektin ortadan kaybolma süresi
        }

        // Ateþ sesi
        if (audioSource && fireSound)
        {
            audioSource.PlayOneShot(fireSound);
        }
    }
}
