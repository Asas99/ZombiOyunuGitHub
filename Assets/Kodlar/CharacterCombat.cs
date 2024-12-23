using UnityEngine;
using UnityEngine.UI;

public class CharacterCombat : MonoBehaviour
{
    public GameObject obj;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.CompareTag("zombie hands"))
        {
            print("Col detected");
        }
    }
}
