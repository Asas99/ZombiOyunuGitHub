using UnityEngine;

public class LightSwitch : MonoBehaviour
{
    public Light connectedLight; // Bu anahtar�n kontrol edece�i ���k
    public KeyCode interactionKey = KeyCode.E;
    private bool isPlayerNearby = false;

    void Update()
    {
        if (isPlayerNearby && Input.GetKeyDown(interactionKey))
        {
            connectedLight.enabled = !connectedLight.enabled;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNearby = false;
        }
    }
}
