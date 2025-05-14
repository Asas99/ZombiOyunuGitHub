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

        // D�n��
        if (x != 0)
        {
            transform.Rotate(Vector3.forward, x * TurnSpeed * Time.fixedDeltaTime);
        }

        // Yak�t t�ketimi ve h�zlanma
        if (y != 0 && CurrentFuel > 0)
        {
            CurrentFuel -= FuelComsumption * Time.fixedDeltaTime;
            Speed += Accel * y * Time.fixedDeltaTime;
        }

        // H�z� s�n�rla
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);

        // Do�al yava�lama
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

        // Yak�t t�ketimi ve h�zlanma
        if (y != 0 && CurrentFuel > 0)
        {
            CurrentFuel -= FuelComsumption * Time.deltaTime;
            Speed += Accel * y * Time.deltaTime;
        }

        // H�z� s�n�rla
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);

        // Do�al yava�lama
        if (y == 0)
        {
            Speed = Mathf.MoveTowards(Speed, 0f, NaturalDecel * Time.deltaTime);
        }

        // Araba'n�n hareketi (X ekseni ileri y�n)
        if (CurrentFuel > 0 && Speed != 0)
        {
            Vector3 forward = transform.right; // X ekseni ileri y�n
            Vector3 movement = forward * -y * Speed * Time.deltaTime; // y ters �evrildi
            rb.MovePosition(transform.position + movement);
        }

        // D�n��ler (Z ekseni etraf�nda d�ner)
        if (x != 0)
        {
            float direction = y <= 0 ? 1f : -1f; // ileri/geri y�n�ne g�re d�n�� y�n�
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
