using UnityEngine;

public class RandomMermiRelocator : MonoBehaviour
{
    public Transform[] spawnPoints; // Silahlar�n ta��nabilece�i konumlar
    public GameObject[] AmmoPacks; // Mevcut silahlar

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
        if (AmmoPacks.Length == 0 || spawnPoints.Length == 0) return;

        foreach (GameObject ammoPack in AmmoPacks)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Instantiate(ammoPack, spawnPoints[randomIndex].position,transform.rotation);
        }
    }
}
