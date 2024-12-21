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
            //Burada kurþun olmayacaðý için ateþ etme sesi ve silahtan çýkan ateþle bir þeyler yapacaðýz. 
            Ray ray = new Ray(transform.position, transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                print("Görüyor: " + hit.collider.name);
                if (hit.collider.CompareTag("zombi"))
                {
                    //Zombinin canýný falan azalt.
                }
            }
        }
    }
    private void OnDrawGizmos()
    {
        Debug.DrawRay(new Vector3(transform.position.x, transform.position.y + 1, transform.position.z), transform.forward* 1000f);
    }
}
