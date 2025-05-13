using UnityEngine;

public class UpcamProblem : MonoBehaviour
{
    public GameObject Alex;
    public Vector3 normalOffset = new Vector3(0, 20, 0);  // Yak�n (minimap)
    public Vector3 mapViewOffset = new Vector3(0, 100, 0); // Y�ksek (t�m harita)

    private Vector3 currentOffset;
    private bool isMapOpen = false;

    void Start()
    {
        currentOffset = normalOffset;
    }

    void Update()
    {
        // Kamera konumunu g�ncelle
        transform.position = Alex.transform.position + currentOffset;

        // M tu�una bas�ld���nda geni� harita g�r�n�m�
        if (Input.GetKeyDown(KeyCode.M))
        {
            isMapOpen = true;
            currentOffset = mapViewOffset;
        }

        // ESC tu�una bas�ld���nda normale d�n
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            isMapOpen = false;
            currentOffset = normalOffset;
        }
    }
}
