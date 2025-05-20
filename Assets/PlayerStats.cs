using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerStats : MonoBehaviour
{
    [Header("Sliders")]
    public Slider hungerSlider;
    public Slider thirstSlider;
    public Slider healthSlider;

    [Header("Ayarlar")]
    public float decreaseInterval = 5f;  // Her kaç saniyede açlık/susuzluk azalacak
    public int decreaseAmount = 1;       // Azalma miktarı

    public float healthDecreaseRate = 2f; // Açlık/susuzluk 0 olunca can azalma süresi
    public int healthDecreaseAmount = 1;

    public float healthRegenRate = 3f;   // Tokluk ve su %50'den fazlaysa can yenilenme süresi
    public int healthRegenAmount = 1;

    private float hungerTimer;
    private float thirstTimer;
    private float healthLossTimer;
    private float healthRegenTimer;

    private bool isDead = false;

    // 🔒 Sahneler arasında korunacak istatistik değerleri
    public static float savedHunger = 100f;
    public static float savedThirst = 100f;
    public static float savedHealth = 100f;

    void Start()
    {
        // Sahne yüklendiğinde kayıtlı değerleri sliderlara uygula
        hungerSlider.value = savedHunger;
        thirstSlider.value = savedThirst;
        healthSlider.value = savedHealth;
    }

    void Update()
    {
        // Can sıfırsa sadece bir kere ölüm işlemi yap
        if (!isDead && healthSlider.value <= 0)
        {
            HandleDeath();
        }

        if (isDead) return;

        HandleHungerDecrease();
        HandleThirstDecrease();
        HandleHealthDecreaseIfNecessary();
        HandleHealthRegenIfPossible();
    }

    void LateUpdate()
    {
        // Statik değişkenleri güncelle
        savedHunger = hungerSlider.value;
        savedThirst = thirstSlider.value;
        savedHealth = healthSlider.value;
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

    void HandleDeath()
    {
        isDead = true;

        Debug.Log("Karakter öldü!");

        // Statları sıfırla
        ResetStats();

        // Ölümden sonra sahneyi yeniden yükle (isteğe bağlı 3 saniye sonra)
        StartCoroutine(ReloadSceneAfterDelay(3f));
    }

    IEnumerator ReloadSceneAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
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

    public void ResetStats()
    {
        savedHunger = 100;
        savedThirst = 100;
        savedHealth = 100;
    }
}
