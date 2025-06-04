using UnityEngine;
using UnityEngine.AI;
using System.Collections;

[RequireComponent(typeof(NavMeshAgent))]
public class NurseAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public Animator animator;
    [SerializeField] private Transform targetZombie;

    public Transform firePoint;
    public float fireRate = 1f;
    public float fireRange = 20f;
    private float nextFireTime = 0f;

    [Header("Ses ve Efektler")]
    public AudioSource gunAudioSource;
    public AudioClip gunShotClip;
    public ParticleSystem muzzleFlash;

    [Header("Takip Ayarlarý")]
    [SerializeField] private float followDistance = 5f;
    [SerializeField] private float rotationSpeed = 5f;

    private bool isShooting = false;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();

        if (gunAudioSource != null && gunShotClip != null)
        {
            gunAudioSource.clip = gunShotClip;
        }
    }

    void Update()
    {
        if (player == null) return;

        FindNearestVisibleZombie();

        if (targetZombie != null)
        {
            float distanceToZombie = Vector3.Distance(transform.position, targetZombie.position);

            if (distanceToZombie > agent.stoppingDistance)
            {
                agent.SetDestination(targetZombie.position);
                if (!isShooting) animator.SetBool("Walk", true);
            }
            else
            {
                agent.ResetPath();
                animator.SetBool("Walk", false);
            }

            AimAtTarget();

            if (Time.time >= nextFireTime && !isShooting)
            {
                StartCoroutine(FireWeapon());
                nextFireTime = Time.time + 1f / fireRate;
            }
        }
        else
        {
            FollowAndFacePlayer();
        }
    }

    void FollowAndFacePlayer()
    {
        float distance = Vector3.Distance(player.position, transform.position);

        if (distance > followDistance)
        {
            agent.SetDestination(player.position);
            animator.SetBool("Walk", true);
        }
        else
        {
            agent.ResetPath();
            animator.SetBool("Walk", false);
        }

        // Hemþire oyuncuya bakar
        Vector3 direction = (player.position - transform.position).normalized;
        direction.y = 0;
        if (direction.magnitude > 0.1f)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
        }
    }

    void FindNearestVisibleZombie()
    {
        GameObject[] zombies = GameObject.FindGameObjectsWithTag("zombi");
        Transform closestZombie = null;
        float closestDistance = Mathf.Infinity;

        foreach (GameObject zombie in zombies)
        {
            Transform zombieTransform = zombie.transform;
            float distanceToZombie = Vector3.Distance(transform.position, zombieTransform.position);

            if (distanceToZombie < closestDistance && IsZombieVisible(zombieTransform))
            {
                if(zombie.GetComponent<ZombieManager>().Health > 0)
                {
                    closestDistance = distanceToZombie;
                    closestZombie = zombieTransform;
                }

            }
        }

        targetZombie = closestZombie;
    }

    bool IsZombieVisible(Transform zombie)
    {
        Vector3 directionToZombie = zombie.position - transform.position;
        if (Physics.Raycast(transform.position, directionToZombie.normalized, out RaycastHit hit))
        {
            return hit.transform == zombie;
        }
        return false;
    }

    void AimAtTarget()
    {
        if (targetZombie != null)
        {
            Vector3 direction = targetZombie.position - transform.position;
            direction.y = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * rotationSpeed);
        }
    }

    IEnumerator FireWeapon()
    {
        isShooting = true;
        animator.SetBool("Shoot", true);
        animator.SetBool("Walk", false);
        yield return new WaitForSeconds(0.1f); // küçük gecikme

        if (targetZombie != null)
        {
            Vector3 direction = targetZombie.position - firePoint.position;

            if (Physics.Raycast(firePoint.position, direction.normalized, out RaycastHit hit, fireRange))
            {
                if (hit.transform.CompareTag("zombi"))
                {
                    Debug.Log("Zombiye isabet etti!");
                    ZombieManager zm = hit.transform.GetComponent<ZombieManager>();
                    if (zm != null)
                    {
                        zm.Health = -1; // yerine zm.TakeDamage(100); önerilir
                    }

                    if (muzzleFlash != null) muzzleFlash.Play();
                    if (gunAudioSource != null && gunShotClip != null)
                        gunAudioSource.PlayOneShot(gunShotClip);
                }
            }
        }

        yield return new WaitForSeconds(0.4f);
        animator.SetBool("Shoot", false);
        isShooting = false;
    }
}
