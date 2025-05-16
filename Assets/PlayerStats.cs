using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    [Header("Sliders")]
    public Slider hungerSlider;
    public Slider thirstSlider;
    public Slider healthSlider;

    [Header("Ayarlar")]
    public float decreaseInterval = 5f; // Her kaç saniyede açlýk/susuzluk azalacak
    public int decreaseAmount = 1;      // Her azalmada ne kadar azalacak

    public float healthDecreaseRate = 2f; // Açlýk/susuzluk 0 olunca ne sýklýkla can azalacak
    public int healthDecreaseAmount = 1;

    public float healthRegenRate = 3f; // Tokluk ve su %50'den fazlaysa kaç saniyede 1 can artsýn
    public int healthRegenAmount = 1;

    private float hungerTimer;
    private float thirstTimer;
    private float healthLossTimer;
    private float healthRegenTimer;

    void Awake() => Instance = this;

    void Update()
    {
        HandleHungerDecrease();
        HandleThirstDecrease();
        HandleHealthDecreaseIfNecessary();
        HandleHealthRegenIfPossible();
    }

    void HandleHungerDecrease()
    {
        hungerTimer += Time.deltaTime;
        if (hungerTimer >= decreaseInterval)
        {
            hungerSlider.value = Mathf.Clamp(hungerSlider.value - decreaseAmount, 0, 100);
            hungerTimer = 0f;
        }
    }

    void HandleThirstDecrease()
    {
        thirstTimer += Time.deltaTime;
        if (thirstTimer >= decreaseInterval)
        {
            thirstSlider.value = Mathf.Clamp(thirstSlider.value - decreaseAmount, 0, 100);
            thirstTimer = 0f;
        }
    }

    void HandleHealthDecreaseIfNecessary()
    {
        if (hungerSlider.value == 0 || thirstSlider.value == 0)
        {
            healthLossTimer += Time.deltaTime;
            if (healthLossTimer >= healthDecreaseRate)
            {
                healthSlider.value = Mathf.Clamp(healthSlider.value - healthDecreaseAmount, 0, 100);
                healthLossTimer = 0f;
            }
        }
        else
        {
            healthLossTimer = 0f;
        }
    }

    void HandleHealthRegenIfPossible()
    {
        if (healthSlider.value > 0 && healthSlider.value < 100 &&
            hungerSlider.value > 50 && thirstSlider.value > 50)
        {
            healthRegenTimer += Time.deltaTime;
            if (healthRegenTimer >= healthRegenRate)
            {
                healthSlider.value = Mathf.Clamp(healthSlider.value + healthRegenAmount, 0, 100);
                healthRegenTimer = 0f;
            }
        }
        else
        {
            healthRegenTimer = 0f;
        }
    }

    public void IncreaseHunger(int amount)
    {
        hungerSlider.value = Mathf.Clamp(hungerSlider.value + amount, 0, 100);
    }

    public void IncreaseThirst(int amount)
    {
        thirstSlider.value = Mathf.Clamp(thirstSlider.value + amount, 0, 100);
    }

    public void IncreaseHealth(int amount)
    {
        healthSlider.value = Mathf.Clamp(healthSlider.value + amount, 0, 100);
    }
}
