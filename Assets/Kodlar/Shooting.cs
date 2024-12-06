using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shooting : MonoBehaviour
{
    public GameObject Bullet;
    public GameObject Spawnpoint;
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
            Instantiate(Bullet, Spawnpoint.transform.position, Spawnpoint.transform.rotation);
        }
    }
}
