using UnityEngine;
using UnityEngine.UI;

public class MoveCam : MonoBehaviour
{
    public Vector3 In�tPos, ADSPos;
    public Vector3 In�tRot, ADSRot;
    public Camera cam;
    public Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam.transform.localPosition = In�tPos;
        cam.transform.localEulerAngles = In�tRot;

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
            cam.transform.localPosition = In�tPos;
            cam.transform.localEulerAngles = In�tRot;
        }
    }
}
