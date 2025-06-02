using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public class ZombieManager : MonoBehaviour
{
    [Header("Oyuncuya doðru gidecek")]
    public GameObject TargetObj;
    public Vector3 Target;
    public Animator animator;
    [Space(10)]
    [Header("Algýlama parametreleri")]
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    [SerializeField]
    private ZombieAnimator zombieanimator;
    public float Health;
    [Header("Oyun objesini yok etme")]
    public float DestroyCounter;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // ZombieAnimator component'ini al
        zombieanimator = gameObject.GetComponent<ZombieAnimator>();

        // TargetObj yoksa, otomatik olarak Player tag'li objeyi bul
        if (TargetObj == null)
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                TargetObj = player;
            }
            else
            {
                Debug.LogError("ZombieManager: 'Player' tag'li obje bulunamadý!");
            }
        }
    }

    void OnDrawGizmos()
    {
        // Draw the field of view in the Scene view
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, viewRadius);

        // Draw FOV angle lines
        Vector3 viewAngleA = DirectionFromAngle(-viewAngle / 2, false);
        Vector3 viewAngleB = DirectionFromAngle(viewAngle / 2, false);

        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(transform.position, transform.position + viewAngleA * viewRadius);
        Gizmos.DrawLine(transform.position, transform.position + viewAngleB * viewRadius);

        // Visualize the target detection
        //if (Target != null && CanSeeTarget())
        //{
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, Target);
        //}
    }

    public void DestroyObj()
    {
        if (Health <= 0)
        {
            DestroyCounter -= Time.deltaTime;
            if (DestroyCounter < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public bool CanSeeTarget()
    {
        if (Target == null)
            return false;

        Vector3 directionToTarget = (Target - transform.position).normalized;  

        Ray ray = new(transform.position, directionToTarget);
        // Check if the target is within the angle of view
        if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
        {
            // Check if there's an obstacle between enemy and target
            if (Physics.Raycast(ray, out RaycastHit hit, viewRadius))
            {
                if (hit.collider.CompareTag("Player"))
                {
                    return true; // Target is visible
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    // Helper function to get direction from angle
    public Vector3 DirectionFromAngle(float angleInDegrees, bool isGlobal)
    {
        if (!isGlobal)
        {
            angleInDegrees += transform.eulerAngles.y;
        }
        return new Vector3(Mathf.Sin(angleInDegrees * Mathf.Deg2Rad), 0, Mathf.Cos(angleInDegrees * Mathf.Deg2Rad));
    }

    public void MoveTowardsPlayer()
    {
        zombieanimator.PlayWalk(animator);
        gameObject.GetComponent<NavMeshAgent>().SetDestination(new Vector3(Target.x, gameObject.transform.position.y, Target.z));
    }

    // Update is called once per frame
    void Update()
    {
        if (Health <= 0)
        {
            zombieanimator.PlayDie(animator);
            gameObject.GetComponent<NavMeshAgent>().velocity = new Vector3(0,0,0);
            gameObject.GetComponent<NavMeshAgent>().angularSpeed = 0;
        }
        DestroyObj();

        Target = new Vector3(TargetObj.transform.position.x, gameObject.transform.position.y, TargetObj.transform.position.z);

        if (Health > 0)
        {
            if (CanSeeTarget())
            {
                if (gameObject.GetComponent<NavMeshAgent>().stoppingDistance < Vector3.Distance(gameObject.transform.position, TargetObj.transform.position))
                {
                    transform.LookAt(Target);
                    MoveTowardsPlayer();
                }
                else {
                    zombieanimator.PlayAttack(animator);
                }
            }
            else
            {
                zombieanimator.PlayIdle(animator);
            }
        }

        //print(CanSeeTarget());
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("býçak"))
        {
            gameObject.GetComponent<ZombieHealthManager>().TakeDamage(10);
        }
    }
}
