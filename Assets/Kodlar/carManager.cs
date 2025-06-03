using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

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
    public float CurrentFuel = 100f;
    public float OneFuelWorth = 25f;
    public float FuelComsumption = 1f;
    public TextMesh fuelText;
    public Slider fuelSlider;

    [Header("Dış Ses Sistemleri")]
    public AudioSource outsideAudioSource1;  // Örneğin gece sesi
    public AudioSource outsideAudioSource2;  // Örneğin gündüz sesi

    [Header("Ses Sistemi")]
    public AudioSource engineAudioSource;
    public AudioClip enterCarSound;
    public AudioClip drivingLoopSound;

    [Header("Stop Lambaları")]
    public Light[] rearStopLights;

    private Rigidbody rb;
    private bool isEngineReady = false;

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (fuelSlider != null)
        {
            fuelSlider.minValue = 0;
            fuelSlider.maxValue = 1;
            fuelSlider.gameObject.SetActive(false);
        }
    }

    void Start()
    {
        if (engineAudioSource != null)
        {
            engineAudioSource.loop = true;
            engineAudioSource.playOnAwake = false;
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (PlayerPrefs.HasKey("CarPosX"))
        {
            float x = PlayerPrefs.GetFloat("CarPosX");
            float y = PlayerPrefs.GetFloat("CarPosY");
            float z = PlayerPrefs.GetFloat("CarPosZ");
            StartCoroutine(SetPositionNextFrame(new Vector3(x, y, z)));
        }
    }

    IEnumerator SetPositionNextFrame(Vector3 pos)
    {
        yield return null; // bir frame bekle
        transform.position = pos;
        Debug.Log("Araba konumu sahne yüklendikten sonra ayarlandı: " + pos);
    }

    void Update()
    {
        float outsideVolume = IsDriving ? 0.1f : 0.7f;

        if (outsideAudioSource1 != null)
            outsideAudioSource1.volume = outsideVolume;

        if (outsideAudioSource2 != null)
            outsideAudioSource2.volume = outsideVolume; 

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

        if (fuelSlider != null)
            fuelSlider.gameObject.SetActive(IsDriving);

        UpdateFuelUI();

        if (Input.GetKey(KeyCode.S) && IsDriving)
            SetRearLights(true);
        else
            SetRearLights(false);

        bool isNear = Dist < MaxDist;
        bool hasFuelCan = GameManager.Instance.HasItem(ItemType.FuelCan);

        if (CurrentFuel <= 0 && isNear && hasFuelCan && Input.GetKeyDown(KeyCode.F))
        {
            if (GameManager.Instance.RemoveItem(ItemType.FuelCan))
            {
                CurrentFuel = MaxFuel;
                if (fuelText != null)
                    fuelText.text = "";
            }
        }

        if (CurrentFuel <= 0f && engineAudioSource.isPlaying)
        {
            engineAudioSource.Stop();
        }

        // Konumu sürekli değil, sadece sahne geçişi öncesi çağrılmalı
    }

    void FixedUpdate()
    {
        if (!IsDriving || !isEngineReady) return;

        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

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

        if (y != 0 && x != 0)
        {
            float direction = y <= 0 ? 1f : -1f;
            transform.Rotate(Vector3.forward, x * TurnSpeed * Time.fixedDeltaTime * direction);
        }

        if (engineAudioSource != null && isEngineReady)
        {
            if ((y != 0 || x != 0) && !engineAudioSource.isPlaying && CurrentFuel > 0)
                engineAudioSource.Play();
            else if (y == 0 && x == 0 && engineAudioSource.isPlaying)
                engineAudioSource.Stop();
        }
    }

    void UpdateFuelUI()
    {
        if (fuelSlider != null)
            fuelSlider.value = CurrentFuel / MaxFuel;

        if (fuelText != null)
        {
            if (CurrentFuel <= 0 && Dist < MaxDist && GameManager.Instance.HasItem(ItemType.FuelCan))
            {
                fuelText.gameObject.SetActive(true);
                fuelText.text = "Press F and fueling";
            }
            else
            {
                fuelText.gameObject.SetActive(false);
            }
        }
    }

    public void HopOnOff()
    {
        if (Input.GetKeyDown(KeyCode.Z) && Dist < MaxDist)
        {
            IsDriving = true;
            isEngineReady = false;

            if (engineAudioSource != null && enterCarSound != null)
            {
                engineAudioSource.PlayOneShot(enterCarSound);
                Invoke(nameof(EnableEngine), enterCarSound.length);
            }
            else
            {
                EnableEngine();
            }
        }
    }

    void EnableEngine()
    {
        isEngineReady = true;

        if (engineAudioSource != null && drivingLoopSound != null)
        {
            engineAudioSource.clip = drivingLoopSound;
        }
    }

    void CheckExit()
    {
        if (IsDriving && Input.GetKeyDown(KeyCode.X))
        {
            player.transform.position = transform.position + Offset;
            IsDriving = false;
            isEngineReady = false;

            if (engineAudioSource != null)
                engineAudioSource.Stop();
        }
    }

    public void SaveCarPosition()
    {
        PlayerPrefs.SetFloat("CarPosX", transform.position.x);
        PlayerPrefs.SetFloat("CarPosY", transform.position.y);
        PlayerPrefs.SetFloat("CarPosZ", transform.position.z);
        PlayerPrefs.Save();

        Debug.Log("Araba konumu kaydedildi: " + transform.position);
    }

    public void Drive()
    {
        float x = Input.GetAxis("Horizontal");
        float y = Input.GetAxis("Vertical");

        if (y != 0 && CurrentFuel > 0 && isEngineReady)
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

        if (y != 0 && x != 0)
        {
            float direction = y <= 0 ? 1f : -1f;
            transform.Rotate(Vector3.forward, x * TurnSpeed * Time.deltaTime * direction);
        }
    }

    void SetRearLights(bool state)
    {
        if (rearStopLights != null)
        {
            foreach (var light in rearStopLights)
            {
                if (light != null)
                    light.enabled = state;
            }
        }
    }
}
