using UnityEngine;

public class ToggleCanvas : MonoBehaviour
{
    public Canvas canvas; // Canvas referansı

    void Start()
    {
        // Başlangıçta Canvas'ın gizli olmasını istiyorsan bunu kullanabilirsin.
        canvas.enabled = false;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            canvas.enabled = !canvas.enabled; // Canvas'ı aktif/pasif yap
        }
    }
}
