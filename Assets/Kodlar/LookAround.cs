using UnityEngine;
using UnityEngine.UI;

public class LookAround : MonoBehaviour
{
    public GameObject Player;
    public Animator AlexAnimator;
    public float Sensitivity;

    void Update()
    {
        Look(); 
    }

    // Update is called once per frame
    public void Look()
    {
        var x = Input.GetAxis("Mouse X");


        if (GameObject.FindObjectOfType<Shooting>().CanShoot && (AlexAnimator.GetBool("Has a pistol") || AlexAnimator.GetBool("Has a rifle")))
        {
            print("qq");
            var y = Input.GetAxis("Mouse Y");
            Camera.main.transform.eulerAngles += new Vector3(-y * Sensitivity, 0, 0);
        }
        Player.transform.eulerAngles += new Vector3(0, x * Sensitivity, 0);

        //Mathf.Clamp(Cam.transform.eulerAngles.x, -90, 90);
    }
}
