using UnityEngine;
using System.Collections.Generic;
using System.Linq;

public class DailyObjectSpawner : MonoBehaviour
{
    [Header("Spawn Ayarlar�")]
    public List<GameObject> spawnablePrefabs;
    public string[] validTags = { "Road", "City" };
    public int minSpawnCount = 3;
    public int maxSpawnCount = 7;

    private List<GameObject> spawnedObjects = new List<GameObject>();

    void OnEnable()
    {
        DayNightCycle.OnNewDay += SpawnForNewDay;

        // Ba�lang��ta g�n bilgisini al�p spawnla
        DayNightCycle dayNightCycle = Object.FindAnyObjectByType<DayNightCycle>();

        if (dayNightCycle != null)
        {
            int currentDay = dayNightCycle.GetCurrentDay();
            SpawnForNewDay(currentDay);
        }
    }

    void OnDisable()
    {
        DayNightCycle.OnNewDay -= SpawnForNewDay;
    }

    void SpawnForNewDay(int day)
    {
        // �ncekileri sil
        foreach (var obj in spawnedObjects)
        {
            if (obj != null)
                Destroy(obj);
        }
        spawnedObjects.Clear();

        // Ge�erli tag'li objeleri bul
        List<Transform> validLocations = new List<Transform>();
        foreach (string tag in validTags)
        {
            GameObject[] taggedObjects = GameObject.FindGameObjectsWithTag(tag);
            validLocations.AddRange(taggedObjects.Select(obj => obj.transform));
        }

        if (validLocations.Count == 0 || spawnablePrefabs.Count == 0)
        {
            Debug.LogWarning("Spawn i�in uygun b�lge veya obje yok.");
            return;
        }

        int spawnCount = Mathf.Min(Random.Range(minSpawnCount, maxSpawnCount + 1), validLocations.Count);
        List<Transform> chosenLocations = validLocations.OrderBy(x => Random.value).Take(spawnCount).ToList();

        foreach (Transform location in chosenLocations)
        {
            GameObject prefab = spawnablePrefabs[Random.Range(0, spawnablePrefabs.Count)];
            Vector3 spawnPos = location.position + Vector3.up * 5f;
            GameObject spawned = Instantiate(prefab, spawnPos, Quaternion.identity);
            spawnedObjects.Add(spawned);
        }

        Debug.Log($"G�n {day}: {spawnCount} obje spawn edildi.");
    }
}
