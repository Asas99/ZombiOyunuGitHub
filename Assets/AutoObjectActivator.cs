using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;

public class OptimizedAutoActivator : MonoBehaviour
{
    public Transform player;
    public float activationDistance = 10f;
    public float checkInterval = 0.5f;
    public string[] targetTags = { "Road", "City" };

    public GameObject[] decalPrefabs; // 7 prefab
    public int decalsPerObject = 1;
    public float maxRayHeight = 10f;

    private List<GameObject> targetObjects = new List<GameObject>();
    private Dictionary<GameObject, bool> objectStates = new Dictionary<GameObject, bool>();
    private bool decalsSpawned = false;

    void Start()
    {
        foreach (string tag in targetTags)
        {
            GameObject[] foundObjects = GameObject.FindGameObjectsWithTag(tag);
            foreach (GameObject obj in foundObjects)
            {
                targetObjects.Add(obj);
                obj.SetActive(true);
                objectStates[obj] = true;
            }
        }

        SpawnDecals();

        InvokeRepeating(nameof(CheckObjects), 0f, checkInterval);
    }

    void SpawnDecals()
    {
        if (decalsSpawned) return;

        foreach (var obj in targetObjects)
        {
            SpawnDecalsOnObject(obj);
        }

        decalsSpawned = true;
    }

    void CheckObjects()
    {
        foreach (GameObject obj in targetObjects)
        {
            if (obj == null) continue;

            float distance = Vector3.Distance(player.position, obj.transform.position);
            bool isActive = objectStates[obj];

            if (distance <= activationDistance && !isActive)
            {
                obj.SetActive(true);
                objectStates[obj] = true;
            }
            else if (distance > activationDistance && isActive)
            {
                obj.SetActive(false);
                objectStates[obj] = false;
            }
        }
    }

    void SpawnDecalsOnObject(GameObject obj)
    {
        Collider collider = obj.GetComponent<Collider>();
        if (collider == null) return;

        Bounds bounds = collider.bounds;

        for (int i = 0; i < decalsPerObject; i++)
        {
            Vector3 randomPoint = new Vector3(
                Random.Range(bounds.min.x, bounds.max.x),
                maxRayHeight,
                Random.Range(bounds.min.z, bounds.max.z)
            );

            Ray ray = new Ray(randomPoint, Vector3.down);
            if (Physics.Raycast(ray, out RaycastHit hit, maxRayHeight * 2f))
            {
                if (hit.collider.gameObject == obj)
                {
                    // Random prefab seç
                    GameObject selectedPrefab = decalPrefabs[Random.Range(0, decalPrefabs.Length)];

                    Quaternion rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
                    rotation *= Quaternion.Euler(90, Random.Range(0f, 360f), 0);

                    GameObject decal = Instantiate(selectedPrefab, hit.point + hit.normal * 0.01f, rotation, obj.transform);
                    decal.transform.localScale = Vector3.one * Random.Range(0.5f, 1.2f);

                    DecalProjector projector = decal.GetComponent<DecalProjector>();
                    if (projector != null)
                    {
                        projector.enabled = true;
                    }
                }
            }
        }
    }
}
