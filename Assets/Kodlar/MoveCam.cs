using UnityEngine;
using UnityEngine.UI;

public class MoveCam : MonoBehaviour
{
    public Vector3 InýtPos, ADSPosPistol, ADSPosRifle;
    public Vector3 InýtRot, ADSRifleRot;
    public Camera cam;
    public Animator animator;
    public GameObject normalParent, rifleAdsParent;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cam.transform.localPosition = InýtPos;
        cam.transform.localEulerAngles = InýtRot;

    }

    // Update is called once per frame
    void Update()
    {
            if (animator.GetBool("Has a pistol") || animator.GetBool("Shoot"))
            {
            print("a");
                //cam.transform.parent = normalParent.transform;
                cam.transform.localPosition = ADSPosPistol;
                //cam.transform.localEulerAngles = ADSRot;
            }
            if (animator.GetBool("Has a rifle") || animator.GetBool("Shoot"))
            {
                //cam.transform.parent = rifleAdsParent.transform;
                cam.transform.localPosition = ADSPosRifle;
                //cam.transform.localEulerAngles = ADSRifleRot;
            }

        else
        {
                if (!animator.GetBool("Has a pistol") && !animator.GetBool("Has a rifle"))
                {
                    //cam.transform.parent = normalParent.transform;
                    cam.transform.localPosition = InýtPos;
                    cam.transform.localEulerAngles = InýtRot;
                }
        }
    }
}
