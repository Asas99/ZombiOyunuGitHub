using UnityEngine;
using UnityEngine.UI;

public class MoveCam : MonoBehaviour
{
    public Vector3 InýtPos, ADSPos;
    public Vector3 InýtRot, ADSRot;
    public Camera cam;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam.transform.localPosition = InýtPos;
        cam.transform.localEulerAngles = InýtRot;

    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetBool("Has a pistol"))
        {
            cam.transform.localPosition = ADSPos;
            cam.transform.localEulerAngles = ADSRot;
        }
        else
        {
            cam.transform.localPosition = InýtPos;
            cam.transform.localEulerAngles = InýtRot;
        }
    }
}
