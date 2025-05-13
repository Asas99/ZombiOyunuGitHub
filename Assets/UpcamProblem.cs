using UnityEngine;

public class UpcamProblem : MonoBehaviour
{
    public GameObject Alex;
    public Vector3 normalOffset = new Vector3(0, 20, 0);  // Yakýn (minimap)
    public Vector3 mapViewOffset = new Vector3(0, 100, 0); // Yüksek (tüm harita)

    private Vector3 currentOffset;
    private bool isMapOpen = false;

    void Start()
    {
        currentOffset = normalOffset;
    }

    void Update()
    {
        // Kamera konumunu güncelle
        transform.position = Alex.transform.position + currentOffset;

        // M tuþuna basýldýðýnda geniþ harita görünümü
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapOpen = true;
            currentOffset = mapViewOffset;
        }

        // ESC tuþuna basýldýðýnda normale dön
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMapOpen = false;
            currentOffset = normalOffset;
        }
    }
}
