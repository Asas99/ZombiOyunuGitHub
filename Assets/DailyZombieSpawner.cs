using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DailyZombieSpawner : MonoBehaviour
{
    [Header("Spawn Ayarlarý")]
    public List<GameObject> spawnableZombies;
    public string[] validTags = { "Road", "City" };
    public int minSpawnCount = 3;
    public int maxSpawnCount = 7;

    [Header("Görünürlük Ayarlarý")]
    public float visibilityDistance = 30f;

    private Transform playerTransform;
    private List<GameObject> spawnedZombies = new List<GameObject>();

    void OnEnable()
    {
        DayNightCycle.OnNewDay += SpawnForNewDay;
    }

    void OnDisable()
    {
        DayNightCycle.OnNewDay -= SpawnForNewDay;
    }

    void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
        if (playerObj != null)
        {
            playerTransform = playerObj.transform;
        }
        else
        {
            Debug.LogError("Player tag'ine sahip bir GameObject bulunamadý! Zombie görünürlük sistemi çalýþmayacak.");
        }
    }

    void Update()
    {
        if (playerTransform == null) return;

        foreach (var zombie in spawnedZombies)
        {
            if (zombie != null)
            {
                float dist = Vector3.Distance(playerTransform.position, zombie.transform.position);
                bool shouldBeVisible = dist <= visibilityDistance;
                if (zombie.activeSelf != shouldBeVisible)
                    zombie.SetActive(shouldBeVisible);
            }
        }
    }

    void SpawnForNewDay(int day)
    {
        // Önceki zombileri temizle
        foreach (var obj in spawnedZombies)
        {
            if (obj != null)
                Destroy(obj);
        }
        spawnedZombies.Clear();

        // Geçerli tag'li bölgeleri bul
        List<Transform> validLocations = new List<Transform>();
        foreach (string tag in validTags)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
            validLocations.AddRange(taggedObjects.Select(obj => obj.transform));
        }

        if (validLocations.Count == 0 || spawnableZombies.Count == 0)
        {
            Debug.LogWarning("Spawn için uygun yer veya zombi prefab'ý yok.");
            return;
        }

        int spawnCount = Mathf.Min(Random.Range(minSpawnCount, maxSpawnCount + 1), validLocations.Count);
        List<Transform> chosenLocations = validLocations.OrderBy(x => Random.value).Take(spawnCount).ToList();

        foreach (Transform location in chosenLocations)
        {
            GameObject zombiePrefab = spawnableZombies[Random.Range(0, spawnableZombies.Count)];
            Vector3 spawnPos = location.position + Vector3.up * 1f;
            GameObject zombie = Instantiate(zombiePrefab, spawnPos, Quaternion.identity);
            zombie.SetActive(false); // Baþlangýçta görünmez
            spawnedZombies.Add(zombie);
        }

        Debug.Log($"Gün {day}: {spawnCount} zombi spawn edildi.");
    }
}
