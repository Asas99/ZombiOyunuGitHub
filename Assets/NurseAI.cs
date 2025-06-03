using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;
using Unity.VisualScripting;

[RequireComponent(typeof(NavMeshAgent))]
public class NurseAI : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    public Animator animator;
    [SerializeField] private Transform targetZombie;

    public Transform firePoint; // Ateþ noktasý (silahýn ucu)
    public float fireRate = 1f; // Ateþ etme süresi
    public float fireRange = 20f; // Maksimum menzil
    private float nextFireTime = 0f;

    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (player != null)
        {
            FindNearestVisibleZombie();

            if (targetZombie != null)
            {
                agent.SetDestination(targetZombie.position);
                AimAtTarget();

                if (Time.time >= nextFireTime)
                {
                    FireWeapon(); // Ateþ et
                    nextFireTime = Time.time + 1f / fireRate;
                }
            }
            else
            {
                MoveTowardsPlayer();
            }
        }
    }

    void MoveTowardsPlayer()
    {
        float distance = Vector3.Distance(new Vector3(player.position.x, transform.position.y, player.position.z), transform.position);

        if (distance > agent.stoppingDistance)
        {
            animator.SetBool("Walk", true);
            agent.SetDestination(new Vector3(player.position.x, transform.position.y, player.position.z));
        }
        else
        {
            animator.SetBool("Walk", false);
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
                closestDistance = distanceToZombie;
                closestZombie = zombieTransform;
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
            transform.rotation = Quaternion.LookRotation(direction);
        }
    }

    void FireWeapon()
    {
        if (targetZombie != null)
        {
            Vector3 direction = targetZombie.position - firePoint.position;

            if (Physics.Raycast(firePoint.position, direction.normalized, out RaycastHit hit, fireRange))
            {
                if (hit.transform.CompareTag("zombi"))
                {
                    Debug.Log("Zombiye isabet etti!");
                    hit.transform.gameObject.GetComponent<ZombieManager>().Health = -1; // Zombiyi yok et
                    animator.SetBool("Shoot",true); // Ateþ animasyonunu tetikle
                }
                else
                {
                    Debug.Log("Arada bir engel var, ateþ edilmiyor!");
                }
            }
        }
        else
        {
            animator.SetBool("Shoot", false);
        }
    }
}
