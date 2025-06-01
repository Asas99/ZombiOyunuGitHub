using UnityEngine;

public class GlassMissionManager : MonoBehaviour
{
    [Header("G�rev Ayarlar�")]
    public Transform[] glassObjects; // 9 cam fanusu buraya ekle
    public Collider missionTrigger;
    public Transform player;

    [Header("Silah ve Raycast")]
    public Transform rayOrigin;
    public float fireRange = 100f;
    public KeyCode fireKey = KeyCode.Mouse0;
    public AudioClip glassBreakSound;

    [Header("Kap� Ayarlar�")]
    public Transform door;
    public float doorOpenHeight = 3f;
    public float doorOpenSpeed = 2f;
    public AudioClip doorOpenSound;

    private bool missionActive = false;
    private int[] hitCounts;
    private int[] hitsToBreak;
    private bool[] isBroken;
    private int brokenCount = 0;
    private bool doorOpening = false;
    private bool doorSoundPlayed = false; // Sesin bir kere �almas� i�in

    void Start()
    {
        int length = glassObjects.Length;
        hitCounts = new int[length];
        hitsToBreak = new int[length];
        isBroken = new bool[length];

        for (int i = 0; i < length; i++)
        {
            hitsToBreak[i] = Random.Range(2, 4); // 2 veya 3 vuru�
            hitCounts[i] = 0;
            isBroken[i] = false;
        }
    }

    void Update()
    {
        // G�rev tetikleyicisine girildiyse g�rev ba�las�n
        if (!missionActive && missionTrigger.bounds.Contains(player.position))
        {
            missionActive = true;
            Debug.Log("Fanus k�rma g�revi ba�lad�!");
        }

        // Silah ate�i
        if (missionActive && Input.GetKeyDown(fireKey))
        {
            RaycastHit hit;
            if (Physics.Raycast(rayOrigin.position, rayOrigin.forward, out hit, fireRange))
            {
                for (int i = 0; i < glassObjects.Length; i++)
                {
                    if (glassObjects[i] != null && hit.transform == glassObjects[i])
                    {
                        hitCounts[i]++;

                        if (hitCounts[i] >= hitsToBreak[i] && !isBroken[i])
                        {
                            isBroken[i] = true;

                            if (glassBreakSound != null)
                                AudioSource.PlayClipAtPoint(glassBreakSound, glassObjects[i].position, 1f);

                            Destroy(glassObjects[i].gameObject);
                            brokenCount++;

                            if (brokenCount >= glassObjects.Length)
                            {
                                doorOpening = true;
                                Debug.Log("T�m fanuslar k�r�ld�! Kap� a��l�yor...");
                            }
                        }

                        break;
                    }
                }
            }
        }

        // Kap� a��lma animasyonu ve ses
        if (doorOpening && door != null)
        {
            if (!doorSoundPlayed && doorOpenSound != null)
            {
                AudioSource.PlayClipAtPoint(doorOpenSound, door.position, 1f);
                doorSoundPlayed = true;
            }

            Vector3 targetPos = new Vector3(door.position.x, door.position.y + doorOpenHeight, door.position.z);
            door.position = Vector3.MoveTowards(door.position, targetPos, doorOpenSpeed * Time.deltaTime);
        }
    }
}
