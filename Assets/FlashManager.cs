using UnityEngine;
using UnityEngine.UI;

public class FlashManager : MonoBehaviour
{
    public float time,TimeLeft;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        TimeLeft -= Time.deltaTime;
        if (Input.GetMouseButtonDown(0))
        {
            TimeLeft = time;
        }
            gameObject.GetComponent<MeshRenderer>().enabled = TimeLeft > 0;
    }
}
