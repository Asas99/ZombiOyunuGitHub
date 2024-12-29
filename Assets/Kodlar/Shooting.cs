using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Spawnpoint;
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
        if (Input.GetMouseButtonUp(0))
        {
            if (Physics.Raycast(Spawnpoint.position,direciton,out RaycastHit hit,1000f))
            {
                if (hit.collider.transform.CompareTag("zombi"))
                {
                    if(hit.collider.gameObject.GetComponent<ZombieManager>() != null)
                    {
                        hit.collider.gameObject.GetComponent<ZombieManager>().Health -= DamageOfCurrentWeapon;
                    }
                    print(hit.collider.gameObject.name);
                }
            }
            // Instantiate(Bullet, Spawnpoint.position, Spawnpoint.rotation);
        }

       
    }
    private void OnDrawGizmos()
    {
          Debug.DrawRay(Spawnpoint.position, direciton * 1000f, Color.green);
    }
}
