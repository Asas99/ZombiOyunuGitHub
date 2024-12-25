using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPush : MonoBehaviour
{
    public float ShootSpeed;
    public float Lifetime;
    public float Damage;
    private float DestroyCounter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PushMermi();
        DestoryMermi();
        DestroyCounter -= Time.deltaTime;
        if (DestroyCounter < 0)
        {
            Destroy(gameObject);
        }
    }

    public void PushMermi()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * ShootSpeed * Time.deltaTime);
    }

    //Bir süre sonra mermiyi yok ediyoruz ki çok fazla mermi olduðunda bilgisayara yük olmasýn.
    public void DestoryMermi()
    {
        Lifetime -= Time.deltaTime;

        if (Lifetime < 0)
        {
            Destroy(gameObject);
        }

    }
}
