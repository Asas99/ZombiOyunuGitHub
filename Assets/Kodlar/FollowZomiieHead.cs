using UnityEngine;
using UnityEngine.UI;

public class FollowZomiieHead : MonoBehaviour
{
    public GameObject ZombieHead;
    public ZombieManager ZombieManager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = ZombieHead.transform.position;
    }
}
