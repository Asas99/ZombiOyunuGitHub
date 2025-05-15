using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats Instance;

    public Slider hungerSlider;
    public Slider thirstSlider;

    void Awake() => Instance = this;

    public void IncreaseHunger(int amount)
    {
        hungerSlider.value = Mathf.Clamp(hungerSlider.value + amount, 0, 100);
    }

    public void IncreaseThirst(int amount)
    {
        thirstSlider.value = Mathf.Clamp(thirstSlider.value + amount, 0, 100);
    }
}
