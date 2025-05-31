using UnityEngine;

public class DoorTrigger : MonoBehaviour
{
    public Transform doorLeft;
    public Transform doorRight;
    public Vector3 leftOpenOffset = new Vector3(-1.5f, 0, 0); // sola kayacak mesafe
    public Vector3 rightOpenOffset = new Vector3(1.5f, 0, 0); // saða kayacak mesafe

    public float doorSpeed = 2f;
    public float autoCloseDelay = 5f;

    public TextMesh promptText;

    private Vector3 leftClosedPos, rightClosedPos;
    private Vector3 leftOpenPos, rightOpenPos;

    private bool isPlayerNear = false;
    private bool isOpen = false;
    private float autoCloseTimer = 0f;

    void Start()
    {
        promptText.gameObject.SetActive(false);

        leftClosedPos = doorLeft.position;
        rightClosedPos = doorRight.position;

        leftOpenPos = leftClosedPos + leftOpenOffset;
        rightOpenPos = rightClosedPos + rightOpenOffset;
    }

    void Update()
    {
        if (isPlayerNear)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                isOpen = !isOpen;

                if (isOpen)
                    autoCloseTimer = autoCloseDelay;
            }
        }

        // Kapýyý Lerp ile aç/kapat
        doorLeft.position = Vector3.Lerp(doorLeft.position, isOpen ? leftOpenPos : leftClosedPos, Time.deltaTime * doorSpeed);
        doorRight.position = Vector3.Lerp(doorRight.position, isOpen ? rightOpenPos : rightClosedPos, Time.deltaTime * doorSpeed);

        // Otomatik kapanma
        if (isOpen)
        {
            autoCloseTimer -= Time.deltaTime;

            if (autoCloseTimer <= 0f)
            {
                isOpen = false;
            }
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = true;
            promptText.gameObject.SetActive(true);
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isPlayerNear = false;
            promptText.gameObject.SetActive(false);
        }
    }
}
