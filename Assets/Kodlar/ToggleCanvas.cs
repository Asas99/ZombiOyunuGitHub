using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    public Canvas canvas; // Canvas referansı

    void Start()
    {
        // Başlangıçta Canvas'ın gizli olmasını istiyorsan bunu kullanabilirsin.
        
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            canvas.enabled = !canvas.enabled; // Canvas'ı aktif/pasif yap
        }
    }
}
