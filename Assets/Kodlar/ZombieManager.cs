using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;
using Unity.VisualScripting;

public class ZombieManager : ZombieStats
{
    [Header("Oyuncuya doðru gidecek")]
    public GameObject TargetObj;
    public Vector3 Target;
    [Space(10)]
    [Header("Algýlama parametreleri")]
    public float viewRadius;
    [Range(0, 360)]
    public float viewAngle;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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

    public bool CanSeeTarget()
    {
        if (Target == null)
            return false;

        Vector3 directionToTarget = (Target - transform.position).normalized;  
        float distanceToTarget = Vector3.Distance(transform.position, Target);

        Ray ray = new Ray(transform.position, directionToTarget);
        RaycastHit hit;
        // Check if the target is within the angle of view
        if (Vector3.Angle(transform.forward, directionToTarget) < viewAngle / 2)
        {
            // Check if there's an obstacle between enemy and target
            if (Physics.Raycast(ray, out hit,viewRadius))
            {
                if (hit.collider.tag == "Player")
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
        gameObject.GetComponent<NavMeshAgent>().SetDestination(new Vector3(Target.x,gameObject.transform.position.y,Target.z));
    }

    // Update is called once per frame
    void Update()
    {
        Target = new Vector3(TargetObj.transform.position.x, gameObject.transform.position.y, TargetObj.transform.position.z);

        Die();

        if (CanSeeTarget())
        {
            transform.LookAt(Target);
            MoveTowardsPlayer();
        }
        //print(CanSeeTarget());
    }
}

public class ZombieStats : MonoBehaviour, ICombat
{
    public float Health;
    public float Damage;

    public void Die()
    {
        if (Health <= 0)
        {
            Destroy(gameObject);
        }
    }
    public void Attack()
    {
        
    }
}
