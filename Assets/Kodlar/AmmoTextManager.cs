using UnityEngine;
using UnityEngine.UI;

public class AmmoTextManager : MonoBehaviour
{
    public Text Text;
    public float VisibleTime;
    public float FadeAmountEachStep;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Text.color = new Color(1, 1, 1, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(VisibleTime > 0)
        {
            VisibleTime -= Time.deltaTime;
        }
        if(VisibleTime < 0)
        {
            Text.color = new Color(1, 1, 1, Text.color.a - FadeAmountEachStep);
        }
    }

    public void DisplayText(string DisplayText)
    {
        Text.text = DisplayText;
        VisibleTime = 2; 
        Text.color = new Color(1, 1, 1, 1);
    }
}
