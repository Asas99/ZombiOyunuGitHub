using UnityEngine;

public class RandomWeaponRelocator : MonoBehaviour
{
    public Transform[] spawnPoints; // Silahlar�n ta��nabilece�i konumlar

    void Start()
    {
        DayNightCycle.OnNewDay += ChangeWeaponPositions; // Yeni g�n ba�lad���nda tetiklensin
    }

    void OnDestroy()
    {
        DayNightCycle.OnNewDay -= ChangeWeaponPositions; // Event'i temizle
    }

    void ChangeWeaponPositions(int dayCount)
    {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            transform.position = spawnPoints[randomIndex].position;

    }
}
