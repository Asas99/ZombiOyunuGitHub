using UnityEngine;

public class StairClimber : MonoBehaviour
{
    public Animator animator; // Karakterin Animator bile�eni
    public float stairClimbSpeed = 2f; // Merdiven t�rmanma h�z�
    public Transform stairDirection; // Merdivenin y�n�n� temsil eden bo� GameObject (up + forward y�nl�)

    private bool isOnStairs = false;

    void Update()
    {
        if (isOnStairs && Input.GetKey(KeyCode.W))
        {
            // Merdiven ��kma animasyonunu ba�lat
            animator.SetBool("isClimbingStairs", true);

            // Karakteri yukar� ve ileri do�ru hareket ettir (merdiven y�n�nde)
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
