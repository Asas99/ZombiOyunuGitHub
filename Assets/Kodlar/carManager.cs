using UnityEngine;
using UnityEngine.UI;

public class carManager : MonoBehaviour
{
    [Header("Genel Kodlar")]
    public Camera CarCam;
    public GameObject player;
    public bool IsDriving;
    public float MaxDist;
    [SerializeField] private float Dist;
    public Vector3 Offset;
    public float Accel;
    public float NaturalDecel;
    [SerializeField] private float Speed;
    public float MinSpeed, MaxSpeed;
    public float TurnSpeed;

    [Header("Yakıt Sistemi")]
    public float MaxFuel = 100f;
    public float CurrentFuel = 100f; // Inspector'dan ayarlanabilir
    public float OneFuelWorth = 25f;
    public float FuelComsumption = 1f;
    public Button fuelButton;
    public Slider fuelSlider;

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);
        if (CurrentFuel < 0) CurrentFuel = 0;
        if (Speed != 0) Speed = Mathf.MoveTowards(Speed, 0, NaturalDecel);

        Dist = Vector3.Distance(transform.position, player.transform.position);
        HopOnOff();
        CheckExit();

        if (IsDriving)
        {
            Drive();
            player.SetActive(false);
            CarCam.gameObject.SetActive(true);
            player.transform.position = gameObject.transform.position + Offset;
        }
        else
        {
            player.SetActive(true);
            CarCam.gameObject.SetActive(false);
        }

        UpdateFuelUI();
    }

    void FixedUpdate()
    {
        if (!IsDriving) return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (x != 0)
            transform.Rotate(Vector3.forward, x * TurnSpeed * Time.fixedDeltaTime);

        if (y != 0 && CurrentFuel > 0)
        {
            CurrentFuel -= FuelComsumption * Time.fixedDeltaTime;
            Speed += Accel * y * Time.fixedDeltaTime;
        }

        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);

        if (y == 0)
            Speed = Mathf.MoveTowards(Speed, 0f, NaturalDecel * Time.fixedDeltaTime);

        if (CurrentFuel > 0 && Speed != 0)
        {
            Vector3 forward = transform.right;
            Vector3 movement = forward * -y * Speed * Time.fixedDeltaTime;
            rb.MovePosition(rb.position + movement);
        }
    }

    void UpdateFuelUI()
    {
        if (fuelSlider != null)
        {
            fuelSlider.minValue = 0;
            fuelSlider.maxValue = MaxFuel;
            fuelSlider.value = CurrentFuel;
            fuelSlider.gameObject.SetActive(IsDriving); // Sadece sürerken göster
        }

        bool isNear = Dist < MaxDist;
        bool hasFuelCan = GameManager.Instance.HasItem(ItemType.FuelCan);

        if (CurrentFuel <= 0 && isNear && hasFuelCan)
        {
            fuelButton.gameObject.SetActive(true);
            fuelButton.onClick.RemoveAllListeners();
            fuelButton.onClick.AddListener(Refuel);
        }
        else
        {
            fuelButton.gameObject.SetActive(false);
        }
    }

    void Refuel()
    {
        if (GameManager.Instance.RemoveItem(ItemType.FuelCan))
        {
            CurrentFuel = MaxFuel;
            fuelButton.gameObject.SetActive(false);
        }
    }

    public void HopOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Dist < MaxDist)
        {
            IsDriving = true;
        }
    }

    void CheckExit()
    {
        if (IsDriving && Input.GetKeyDown(KeyCode.X))
        {
            player.transform.position = transform.position + Offset;
            IsDriving = false;
        }
    }

    public void Drive()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (y != 0 && CurrentFuel > 0)
        {
            CurrentFuel -= FuelComsumption * Time.deltaTime;
            Speed += Accel * -y * Time.deltaTime;
        }

        Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);

        if (y == 0)
            Speed = Mathf.MoveTowards(Speed, 0f, NaturalDecel * Time.deltaTime);

        if (CurrentFuel > 0 && Speed != 0)
        {
            Vector3 forward = transform.right;
            Vector3 movement = forward * 1 * Speed * Time.deltaTime;
            rb.MovePosition(transform.position + movement);
        }

        if (x != 0)
        {
            float direction = y <= 0 ? 1f : -1f;
            transform.Rotate(Vector3.forward, x * TurnSpeed * Time.deltaTime * direction);
        }
    }
}
