using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FishingZone : MonoBehaviour
{
    public GameObject fishingButton;
    public GameObject player;
    public GameObject fishingDummy;
    public Camera fishingCamera;
    public Camera mainCamera;
    public GameObject fishPrefab;
    public Transform fishSpawnPoint;
    public GameObject uiCanvas;

    public GameObject boat;         // 🛶 Sandal
    public GameObject fishingRod;   // 🎣 Olta

    public float cinematicDuration = 4f;
    public Vector3 cameraMoveOffset = new Vector3(4f, 0f, 0f);

    [Range(0f, 1f)]
    public float catchChance = 0.5f; // %50 şans

    private Vector3 initialCamPosition;

    private void Start()
    {
        fishingButton.SetActive(false);
        if (fishingCamera != null)
        {
            initialCamPosition = fishingCamera.transform.position;
            fishingCamera.enabled = false;
        }

        if (boat != null) boat.SetActive(false);         // Sandal başta kapalı
        if (fishingRod != null) fishingRod.SetActive(false); // Olta başta kapalı
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fishingButton.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            fishingButton.SetActive(false);
        }
    }

    public void StartFishing()
    {
        StartCoroutine(FishingSequence());
    }

    private IEnumerator FishingSequence()
    {
        // Geçiş
        player.SetActive(false);
        fishingDummy.SetActive(true);
        mainCamera.enabled = false;
        fishingCamera.enabled = true;
        if (uiCanvas != null) uiCanvas.SetActive(false);
        if (boat != null) boat.SetActive(true);              // Sandal aktif
        if (fishingRod != null) fishingRod.SetActive(true);  // Olta aktif

        fishingCamera.transform.position = initialCamPosition;
        Vector3 startPosition = initialCamPosition;
        Vector3 endPosition = startPosition + cameraMoveOffset;
        float elapsedTime = 0f;

        // Kamera kayması
        while (elapsedTime < cinematicDuration)
        {
            fishingCamera.transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / cinematicDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        fishingCamera.transform.position = endPosition;

        // Balık yakalanma şansı
        bool fishCaught = Random.value <= catchChance;

        // Geri dönüş
        fishingDummy.SetActive(false);
        player.SetActive(true);
        fishingCamera.enabled = false;
        mainCamera.enabled = true;
        if (uiCanvas != null) uiCanvas.SetActive(true);
        if (boat != null) boat.SetActive(false);             // Sandal gizlenir
        if (fishingRod != null) fishingRod.SetActive(false); // Olta gizlenir

        // Balık yakalandıysa
        if (fishCaught)
        {
            Instantiate(fishPrefab, fishSpawnPoint.position, Quaternion.identity);
        }
        else
        {
            Debug.Log("Balık kaçtı! 🎣");
        }
    }
}
