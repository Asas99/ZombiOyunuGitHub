using UnityEngine;

public class ZombiSesTrigger : MonoBehaviour
{
    public float detectionRange = 10f;
    public AudioClip zombiSesi;
    private AudioSource audioSource;
    private Transform player;
    private bool sesCaldi = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
        }
        else
        {
            Debug.LogWarning("Player tag'li obje bulunamadý.");
        }
    }

    void Update()
    {
        if (player == null || sesCaldi)
            return;

        float distance = Vector3.Distance(transform.position, player.position);
        if (distance <= detectionRange)
        {
            SesCal();
        }
    }

    void SesCal()
    {
        if (zombiSesi != null && audioSource != null)
        {
            audioSource.PlayOneShot(zombiSesi);
            sesCaldi = true;
        }
    }
}
