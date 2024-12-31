using UnityEngine;
using UnityEngine.UI;

public class DayNightCycle : MonoBehaviour
{
    [Range(0f, 24f)]
    public float DayTime;
    public GameObject SunHolder;
    public float Speed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        DayTime += (Time.deltaTime * Speed);
        SunHolder.transform.eulerAngles = new Vector3(0 + ((360 / 24) * DayTime), 0, 0);
    }
}
