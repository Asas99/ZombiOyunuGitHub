using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Spawnpoint;
    public GameObject ShootPointer;
    public bool CanShoot;
    public Vector3 RayRot;
    private Vector3 direciton;
    public WeaponEquipManager weaponEquipManager;
    public SwitchWeaponInHand switchWeaponInHand;

    [Header("Ses için")]
    public AudioSource audioSource;
    public AudioClip Defaultclip;
    public AudioClip PistolClip;
    public AudioClip Ak47Clip;
    public AudioClip RemingtonClip;
    public AudioClip KragClip;
    public AudioClip SpringfieldClip;
    public AudioClip WincehsterClip;

    void Start()
    {
        weaponEquipManager = GameObject.FindAnyObjectByType<WeaponEquipManager>();
    }

    void Update()
    {
        // Envanter veya oyun duraklatýldýðýnda iþlem yapma
        if (Time.timeScale == 0) return;

        #region Ses atama
        if (weaponEquipManager.tag == "Colt" || weaponEquipManager.tag == "revolver")
        {
            Defaultclip = PistolClip;
        }
        else if (weaponEquipManager.tag == "ak47")
        {
            Defaultclip = Ak47Clip;
        }
        else if (weaponEquipManager.tag == "Krag-Jergesen")
        {
            Defaultclip = KragClip;
        }
        else if (weaponEquipManager.tag == "remington")
        {
            Defaultclip = RemingtonClip;
        }
        else if (weaponEquipManager.tag == "springfield")
        {
            Defaultclip = SpringfieldClip;
        }
        else if (weaponEquipManager.tag == "winchester1894" || weaponEquipManager.tag == "winchester1897")
        {
            Defaultclip = WincehsterClip;
        }
        audioSource.clip = Defaultclip;
        #endregion

        if (weaponEquipManager.CurrentAmmo > 0)
        {
            Shoot();
            direciton = Quaternion.Euler(RayRot) * Camera.main.transform.forward;
        }
    }

    public void Shoot()
    {
        // Oyun duraklatýldýysa ateþ etme
        if (Time.timeScale == 0) return;

        if (Physics.Raycast(Spawnpoint.position, direciton, out RaycastHit hit, 1000f))
        {
            if (hit.collider.gameObject != null)
            {
                ShootPointer.transform.position = hit.point;
            }

            if (weaponEquipManager.BulletInCharger >= 0)
            {
                if (Input.GetMouseButtonUp(0))
                {
                    weaponEquipManager.CurrentAmmo--;
                    weaponEquipManager.BulletInCharger--;
                    weaponEquipManager.DecreaseBullet();
                    audioSource.PlayOneShot(Defaultclip);

                    if (hit.collider.transform.CompareTag("zombi"))
                    {
                        if (hit.collider.gameObject.TryGetComponent<ZombieHealthManager>(out var healthManager))
                        {
                            healthManager.TakeDamage(weaponEquipManager.Damage);
                        }
                    }

                    if (hit.collider.transform.CompareTag("zombie head"))
                    {
                        if (hit.collider.gameObject.TryGetComponent<ZombieHealthManager>(out var healthManager))
                        {
                            healthManager.TakeDamage(healthManager.zombieManager.Health + 1);
                        }
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(Spawnpoint.position, direciton * 1000f, Color.green);
    }
}
