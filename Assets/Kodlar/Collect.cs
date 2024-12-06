using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Collect : MonoBehaviour
{
    public ResourcesManager resourcesManager;
    public int HealthPrize;
    public int FoodPrize;
    public int WaterPrize;
    [Header("Animatör")]
    public Animator AlexAnimator;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown()
    {
        resourcesManager.Health += HealthPrize;
        resourcesManager.Food += FoodPrize;
        resourcesManager.Water += WaterPrize;
        AlexAnimator.SetBool("Take item", true);
        Destroy(gameObject);
    }
}
