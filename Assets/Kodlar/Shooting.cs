using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
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
    public float DamageOfCurrentWeapon;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
        direciton = Quaternion.Euler(RayRot) * Camera.main.transform.forward;

    }

    
    public void Shoot()
    {    
        if (Physics.Raycast(Spawnpoint.position,direciton,out RaycastHit hit,1000f))
            {        
                if (hit.collider.gameObject != null)
                {
                    ShootPointer.transform.position = hit.point;
                }
           
                if (Input.GetMouseButtonUp(0))
                {
                if (hit.collider.transform.CompareTag("zombi"))
                    {
                    if (hit.collider.gameObject.TryGetComponent<ZombieHealthManager>(out var healthManager))
                    {
                        healthManager.TakeDamage(DamageOfCurrentWeapon);
                    }
                    //if (hit.collider.gameObject.GetComponent<ZombieManager>() != null)
                    //    {
                    //    hit.collider.gameObject.GetComponent<ZombieHealthManager>().TakeDamage(DamageOfCurrentWeapon);
                    //    }
                }
                    if (hit.collider.transform.CompareTag("zombie head"))
                    {

                    //if (hit.collider.gameObject.GetComponent<ZombieManager>() != null)
                    //{
                    //    hit.collider.gameObject.GetComponent<ZombieHealthManager>().TakeDamage(hit.collider.gameObject.GetComponent<ZombieManager>().Health + 1);  

                    //}
                    //print("Headshot!");
                    //print(hit.collider.gameObject.GetComponent<ZombieHealthManager>();

                    if (hit.collider.gameObject.TryGetComponent<ZombieHealthManager>(out var healthManager))
                    {
                        healthManager.TakeDamage(healthManager.zombieManager.Health + 1);

                    }
                }
                //print(hit.collider.gameObject.name);
                // Instantiate(Bullet, Spawnpoint.position, Spawnpoint.rotation);
            }
            }       
    }
    private void OnDrawGizmos()
    {
          Debug.DrawRay(Spawnpoint.position, direciton * 1000f, Color.green);
    }
}
