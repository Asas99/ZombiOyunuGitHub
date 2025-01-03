using UnityEngine;
using UnityEngine.UI;

public class ZombieHealthManager : MonoBehaviour, ITakeDamage
{
    public ZombieManager zombieManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        zombieManager = transform.root.GetComponent<ZombieManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(float amount)
    {
        if (zombieManager != null)
        {
            zombieManager.Health -= amount;
        }
    }
}
