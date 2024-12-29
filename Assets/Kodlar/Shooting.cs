using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public Transform Spawnpoint;
    public bool CanShoot;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Shoot();
    }

    public void Shoot()
    {
        if (Input.GetMouseButtonUp(0))
        {

            if (Input.GetMouseButtonUp(0))
            {
                Instantiate(Bullet, Spawnpoint.position, Spawnpoint.rotation);
            }

        }
        //private void OnDrawGizmos()
        //{
        //    Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.forward* 1000f,Color.green);
        //}
    }
}
