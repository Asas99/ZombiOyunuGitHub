using UnityEngine;

public class WaterSoundController : MonoBehaviour
{
    public Transform player;            // Oyuncu transformu (örn: Camera.main.transform)
    public float maxDistance = 20f;     // Su sesinin duyulmaya baþladýðý maksimum mesafe
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
            Debug.LogWarning("Lake tag'ine sahip obje bulunamadý!");
        }

        if (waterAudio != null)
        {
            waterAudio.volume = 0f;  // Baþlangýçta ses kapalý
            waterAudio.loop = true;
            waterAudio.Play();
        }
        else
        {
            Debug.LogWarning("AudioSource atanmamýþ!");
        }
    }

    void Update()
    {
        if (lakeTransform == null || player == null || waterAudio == null)
            return;

        // Oyuncu ile göl arasýndaki mesafeyi ölç
        float distance = Vector3.Distance(player.position, lakeTransform.position);

        // Mesafeye göre ses seviyesini ayarla (mesafe azaldýkça ses artar)
        if (distance < maxDistance)
        {
            // Ses þiddeti mesafeye ters orantýlý, 0 ile 1 arasýnda
            float volume = 1f - (distance / maxDistance);
            waterAudio.volume = Mathf.Clamp01(volume);
        }
        else
        {
            waterAudio.volume = 0f;
        }
    }
}
