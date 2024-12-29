using UnityEngine;
using UnityEngine.UI;

public class LookAround : MonoBehaviour
{
    public GameObject Player;
    public float Sensitivity;

    void Update()
    {
        Look(); 
    }

    // Update is called once per frame
    public void Look()
    {
        var x = Input.GetAxis("Mouse X");
        var y = Input.GetAxis("Mouse Y");

        if (GameObject.FindObjectOfType<Shooting>().CanShoot)
        {
            Camera.main.transform.eulerAngles += new Vector3(y * Sensitivity, 0, 0);
        }
        Player.transform.eulerAngles += new Vector3(0, x * Sensitivity, 0);

        //Mathf.Clamp(Cam.transform.eulerAngles.x, -90, 90);
    }
}
