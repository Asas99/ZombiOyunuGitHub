using UnityEngine;
using UnityEngine.Rendering.Universal;

public class DecalSpawner : MonoBehaviour
{
    public GameObject decalPrefab;
    public int count = 100;
    public Vector2 areaSize = new Vector2(50f, 50f);
    public float maxRayHeight = 100f;
    public string[] validTags = { "Road", "City" }; // sadece bu tag'lere decal yerleþir

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            Vector3 randomXZ = new Vector3(
                Random.Range(-areaSize.x / 2f, areaSize.x / 2f),
                maxRayHeight,
                Random.Range(-areaSize.y / 2f, areaSize.y / 2f)
            );

            Ray ray = new Ray(randomXZ, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (IsValidSurface(hit.collider.tag))
                {
                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    rotation *= Quaternion.Euler(90, Random.Range(0f, 360f), 0);

                    GameObject decal = Instantiate(decalPrefab, hit.point + hit.normal * 0.05f, rotation);
                    decal.transform.localScale = Vector3.one * Random.Range(0.5f, 1.2f);
                }
            }
        }
    }

    bool IsValidSurface(string tag)
    {
        foreach (string validTag in validTags)
        {
            if (tag == validTag)
                return true;
        }
        return false;
    }
}
