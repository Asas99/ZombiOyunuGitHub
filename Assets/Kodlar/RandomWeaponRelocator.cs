using UnityEngine;

public class RandomWeaponRelocator : MonoBehaviour
{
    public Transform[] spawnPoints; // Silahlarýn taþýnabileceði konumlar

    void Start()
    {
        DayNightCycle.OnNewDay += ChangeWeaponPositions; // Yeni gün baþladýðýnda tetiklensin
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
