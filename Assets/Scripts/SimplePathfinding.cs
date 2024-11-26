using UnityEngine;

public class SimplePathfinding : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] public GameObject P1Object;
    [SerializeField] public GameObject P2Object;
    private bool P1Closer = true;
    [SerializeField] private Vector3 P1offset;
    [SerializeField] private Vector3 P2offset;
    [SerializeField] private float smoothSpeed = 5f;
    private float attackRange = 1f;
    private float attackCooldown = 2f;
    private float nextAttackTime = 0;
    [SerializeField] private Animator animator;

    void Start()
    {
        //assign objects for each player to be tracked.
        try
        {
            P1Object = GameObject.Find("Players").transform.Find("Player 1").gameObject;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"P1 Not Found, Cannot Pathfind. Error: {e.Message}");
        }
        try
        {
            P2Object = GameObject.Find("Players").transform.Find("Player 2").gameObject;
        }
        catch (System.Exception e)
        {
            Debug.LogWarning($"P1 Not Found, Cannot Pathfind. Error: {e.Message}");
        }
    }

    private void FixedUpdate()
    {
        Vector3 currentPos = transform.position;
        Vector3 desiredPosition = currentPos;
        float P1Distance = 1000f;
        float P2Distance = 1000f;

        // Calculate positions and distances if players exist
        Vector3? P1Pos = P1Object ? (Vector3?)(P1Object.transform.position + P1offset) : null;
        Vector3? P2Pos = P2Object ? (Vector3?)(P2Object.transform.position + P2offset) : null;

        if (P1Pos.HasValue)
        {
            P1Distance = (currentPos - P1Pos.Value).magnitude;
        }
        if (P2Pos.HasValue)
        {
            P2Distance = (currentPos - P2Pos.Value).magnitude;
        }

        // Determine target position, based on distance and whether players have any health.
        if (P1Object && P2Object)
        {
            var P1Health = P1Object.GetComponent<Health>();
            var P2Health = P2Object.GetComponent<Health>();

            if (P1Health && P2Health)
            {
                P1Closer = !(P1Health.currentHealth <= 0 ||
                            (P2Health.currentHealth > 0 && P2Distance <= P1Distance));
            }

            desiredPosition = P1Closer ? P1Pos.Value : P2Pos.Value;
        }
        else if (P1Object && P1Pos.HasValue)
        {
            desiredPosition = P1Pos.Value;
        }
        else if (P2Object && P2Pos.HasValue)
        {
            desiredPosition = P2Pos.Value;
        }
        else
        {
            Debug.Log("No Players Exist, cannot pathfind.");
            return;
        }

        // Move towards target
        Vector3 direction = (desiredPosition - currentPos).normalized;
        direction.y = 0; // Lock vertical direction for both rotation and movement

        if (direction != Vector3.zero) // Prevent errors when direction is zero
        {
            transform.rotation = Quaternion.LookRotation(direction);
            rb.MovePosition(currentPos + direction * smoothSpeed * Time.fixedDeltaTime);
        }

        if (desiredPosition != currentPos)
        {
            animator.SetBool("IsWalking", true);
            Debug.Log("IsWalking set to true.");
        }
        else
        {
            animator.SetBool("IsWalking", false);
            Debug.Log("IsWalking set to false.");
        }

        // Attack logic with null checks
        if (Time.time > nextAttackTime)
        {
            if (P1Object && P1Distance <= attackRange)
            {
                animator.SetTrigger("Attack");
                var health = P1Object.GetComponent<Health>();
                if (health != null)
                {
                    health.Damage(1);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
            if (P2Object && P2Distance <= attackRange)
            {
                animator.SetTrigger("Attack");
                var health = P2Object.GetComponent<Health>();
                if (health != null)
                {
                    health.Damage(1);
                    nextAttackTime = Time.time + attackCooldown;
                }
            }
        }
    }
}
