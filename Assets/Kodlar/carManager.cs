using UnityEngine;
using UnityEngine.UI;

public class carManager : MonoBehaviour
{
    [Header("Genel Kodlar")]
    public Camera CarCam;
    public GameObject player;
    public bool IsDriving;
    public float MaxDist;
    [SerializeField]
    private float Dist;
    public Vector3 Offset;
    public float Speed;
    public float TurnSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        Dist = Vector3.Distance(transform.position, player.transform.position);
        HopOnOff();

        if (IsDriving)
        {    
            Drive();
            player.SetActive(false);
            CarCam.gameObject.SetActive(true);
            player.transform.position = transform.position + Offset;

        }
        else if (!IsDriving)
        {
            player.SetActive(true);
            CarCam.gameObject.SetActive(false);
        }

    }

    public void HopOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (Dist < MaxDist)
            {
                IsDriving = !IsDriving;
            }
        }
    }

    public void Drive()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        gameObject.transform.position += transform.forward * Speed * Time.deltaTime * y;
        gameObject.transform.eulerAngles += new Vector3(0, x * TurnSpeed * Time.deltaTime, 0);
    }
}
