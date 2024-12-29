using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BulletPush : MonoBehaviour
{
    public float ShootSpeed;
    public float Lifetime;
    public float Damage;
    public Vector3 PushDirection;
    // Start is called before the first frame update
    void Start()
    {
        PushDirection = Camera.main.transform.forward;
    }

    // Update is called once per frame
    void Update()
    {
        PushMermi();
        DestoryMermi();
    }

    public void PushMermi()
    {
        gameObject.GetComponent<Rigidbody>().AddForce(ShootSpeed * Time.deltaTime * PushDirection);
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
