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
    public float Accel;
    public float NaturalDecel;
    [SerializeField]
    private float Speed;
    public float MinSpeed,MaxSpeed;
    public float TurnSpeed;
    [Space(10)]
    public float MaxFuel, CurrentFuel;
    public float OneFuelWorth;
    public float FuelComsumption;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);
        if (CurrentFuel < 0)
        {
            CurrentFuel = 0;
        }
        if (Speed != 0)
        {
            Speed = Mathf.MoveTowards(Speed, 0, NaturalDecel);
        }
        Dist = Vector3.Distance(transform.position, player.transform.position);
        HopOnOff();

        if (IsDriving)
        {    
            Drive();
            if (MaxFuel > CurrentFuel)
            {
                if (MaxFuel - CurrentFuel > OneFuelWorth)
                {
                if (GameObject.FindFirstObjectByType<PlayerInventory>().ItemInfos[17].Quantity > 0)
                    {
                        GameObject.FindFirstObjectByType<PlayerInventory>().ItemInfos[17].Quantity--;
                        CurrentFuel += OneFuelWorth;
                    }
                }
            }
            player.SetActive(false);
            CarCam.gameObject.SetActive(true);
            player.transform.position = gameObject.transform.position + Offset;

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
                    IsDriving = true;
                }
        }
    }

    public void Drive()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        if (y != 0)
        {
            CurrentFuel -= FuelComsumption * Time.deltaTime;
        }
        #region Hýzlan ve yavaþla
        Speed += Accel * y;
        #endregion

        if (CurrentFuel > 0)
        {
            #region Hareket Et
            gameObject.transform.position += transform.forward * Speed * Time.deltaTime;
            gameObject.transform.eulerAngles += new Vector3(0, x * TurnSpeed * Time.deltaTime, 0);
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            player.transform.position = gameObject.transform.position + Offset;
            IsDriving = false;
        }
        #endregion

    }
}
