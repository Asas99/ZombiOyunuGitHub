using UnityEngine;

public class WaterSoundController : MonoBehaviour
{
    public Transform player;            // Oyuncu transformu (�rn: Camera.main.transform)
    public float maxDistance = 20f;     // Su sesinin duyulmaya ba�lad��� maksimum mesafe
    public AudioSource waterAudio;      // Su sesi AudioSource'u

    private Transform lakeTransform;

    void Start()
    {
        // Lake tag'ine sahip ilk objeyi bul
        GameObject lake = GameObject.FindGameObjectWithTag("Lake");
        if (lake != null)
        {
            lakeTransform = lake.transform;
        }
        else
        {
            Debug.LogWarning("Lake tag'ine sahip obje bulunamad�!");
        }

        if (waterAudio != null)
        {
            waterAudio.volume = 0f;  // Ba�lang��ta ses kapal�
            waterAudio.loop = true;
            waterAudio.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource atanmam��!");
        }
    }

    void Update()
    {
        if (lakeTransform == null || player == null || waterAudio == null)
            return;

        // Oyuncu ile g�l aras�ndaki mesafeyi �l�
        float distance = Vector3.Distance(player.position, lakeTransform.position);

        // Mesafeye g�re ses seviyesini ayarla (mesafe azald�k�a ses artar)
        if (distance < maxDistance)
        {
            // Ses �iddeti mesafeye ters orant�l�, 0 ile 1 aras�nda
            float volume = 1f - (distance / maxDistance);
            waterAudio.volume = Mathf.Clamp01(volume);
        }
        else
        {
            waterAudio.volume = 0f;
        }
    }
}
