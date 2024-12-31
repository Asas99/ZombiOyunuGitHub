using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStatus : MonoBehaviour
{
    public float Health;
    public float Water;
    public float Food;

    [Header("Azalma")]
    public float WaterDecrease, FoodDecrease;

    [Header("Status UI")]
    public Slider HealthSlider;
    public Slider WaterSlider;
    public Slider FoodSlider;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        HealthSlider.maxValue = Health;
        WaterSlider.maxValue = Water;
        FoodSlider.maxValue = Food;
        InvokeRepeating("DecreaseStatus", 0, 1);
    }

    // Update is called once per frame
    void Update()
    {
        HealthSlider.value = Health;
        WaterSlider.value = Water;
        FoodSlider.value = Food;

        if (Health > HealthSlider.maxValue)
        {
            Health--;
        }
        if (Water > WaterSlider.maxValue)
        {
            Water--;
        }
        if (Food > FoodSlider.maxValue)
        {
            Food--;
        }
    }

    public void DecreaseStatus()
    {
        Water -= WaterDecrease;
        Food -= FoodDecrease;
    }
}
