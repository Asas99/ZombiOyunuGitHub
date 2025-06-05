using UnityEngine;

public class FlashlightController : MonoBehaviour
{
    public Light flashlight;              // Fenerin kendisi (Spot Light veya Point Light olabilir)
    public AudioSource audioSource;      // Ses oynatýcý
    public AudioClip toggleSound;        // Fener açma/kapama sesi

    private bool isOn = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H))
        {
            ToggleFlashlight();
        }
    }

    void ToggleFlashlight()
    {
        isOn = !isOn;
        flashlight.enabled = isOn;

        if (toggleSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(toggleSound);
        }
    }
}
