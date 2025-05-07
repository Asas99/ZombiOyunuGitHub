using UnityEngine;

public class StairClimber : MonoBehaviour
{
    public Animator animator; // Karakterin Animator bileþeni
    public float stairClimbSpeed = 2f; // Merdiven týrmanma hýzý
    public Transform stairDirection; // Merdivenin yönünü temsil eden boþ GameObject (up + forward yönlü)

    private bool isOnStairs = false;

    void Update()
    {
        if (isOnStairs && Input.GetKey(KeyCode.W))
        {
            // Merdiven çýkma animasyonunu baþlat
            animator.SetBool("isClimbingStairs", true);

            // Karakteri yukarý ve ileri doðru hareket ettir (merdiven yönünde)
            Vector3 climbDir = (stairDirection.up + stairDirection.forward).normalized;
            transform.position += climbDir * stairClimbSpeed * Time.deltaTime;
        }
        else
        {
            // Animasyonu durdur
            animator.SetBool("isClimbingStairs", false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Stairs"))
        {
            isOnStairs = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Stairs"))
        {
            isOnStairs = false;
        }
    }
}
