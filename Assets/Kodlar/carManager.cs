using UnityEngine;

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
    public float MinSpeed, MaxSpeed;
    public float TurnSpeed;
    [Space(10)]
    public float MaxFuel, CurrentFuel;
    public float OneFuelWorth;
    public float FuelComsumption;

    private Rigidbody rb;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();

       
            rb = GetComponent<Rigidbody>();
           
        

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
    void FixedUpdate()
    {
        if (!IsDriving) return; // Karakter binmeden araba hareket etmesin

        float x = Input.GetAxis("Horizontal"); // A / D
        float y = Input.GetAxis("Vertical");   // W / S

        // Dönüþ
        if (x != 0)
        {
            transform.Rotate(Vector3.forward, x * TurnSpeed * Time.fixedDeltaTime);
        }

        // Yakýt tüketimi ve hýzlanma
        if (y != 0 && CurrentFuel > 0)
        {
            CurrentFuel -= FuelComsumption * Time.fixedDeltaTime;
            Speed += Accel * y * Time.fixedDeltaTime;
        }

        // Hýzý sýnýrla
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);

        // Doðal yavaþlama
        if (y == 0)
        {
            Speed = Mathf.MoveTowards(Speed, 0f, NaturalDecel * Time.fixedDeltaTime);
        }

        // Hareket
        if (CurrentFuel > 0 && Speed != 0)
        {
            Vector3 forward = transform.right;
            Vector3 movement = forward * -y * Speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
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
        float x = Input.GetAxis("Horizontal"); // A / D
        float y = Input.GetAxis("Vertical");   // W / S

        // Yakýt tüketimi ve hýzlanma
        if (y != 0 && CurrentFuel > 0)
        {
            CurrentFuel -= FuelComsumption * Time.deltaTime;
            Speed += Accel * y * Time.deltaTime;
        }

        // Hýzý sýnýrla
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);

        // Doðal yavaþlama
        if (y == 0)
        {
            Speed = Mathf.MoveTowards(Speed, 0f, NaturalDecel * Time.deltaTime);
        }

        // Araba'nýn hareketi (X ekseni ileri yön)
        if (CurrentFuel > 0 && Speed != 0)
        {
            Vector3 forward = transform.right; // X ekseni ileri yön
            Vector3 movement = forward * -y * Speed * Time.deltaTime; // y ters çevrildi
            rb.MovePosition(transform.position + movement);
        }

        // Dönüþler (Z ekseni etrafýnda döner)
        if (x != 0)
        {
            float direction = y <= 0 ? 1f : -1f; // ileri/geri yönüne göre dönüþ yönü
            transform.Rotate(Vector3.forward, x * TurnSpeed * Time.deltaTime * direction);
        }

        // Arabadan inme
        if (Input.GetKeyDown(KeyCode.X))
        {
            player.transform.position = transform.position + Offset;
            IsDriving = false;
        }
    }





}
