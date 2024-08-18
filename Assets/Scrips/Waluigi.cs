using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float patrolTime = 5f;
    public Animator animator;

    private NavMeshAgent agent;
    private float patrolTimer;
    private Vector3 patrolTarget;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolTimer = patrolTime;
        SetNewPatrolTarget();
        agent.updatePosition = false; // Désactiver la mise à jour automatique de la position
    }

    void Update()
    {
        float distanceToPlayer = Vector3.Distance(player.position, transform.position);

        if (distanceToPlayer <= attackRange)
        {
            AttackPlayer();
        }
        else if (distanceToPlayer <= detectionRange)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }

        // Synchronize speed with animation
        animator.SetFloat("Speed", agent.velocity.magnitude);

        // Appliquer la position du NavMeshAgent au transform
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            transform.position = agent.nextPosition;
        }
    }

    void Patrol()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);

        patrolTimer -= Time.deltaTime;
        if (patrolTimer <= 0)
        {
            SetNewPatrolTarget();
            patrolTimer = patrolTime;
        }

        agent.SetDestination(patrolTarget);
    }

    void SetNewPatrolTarget()
    {
        patrolTarget = new Vector3(
            transform.position.x + Random.Range(-10, 10),
            transform.position.y,
            transform.position.z + Random.Range(-10, 10)
        );
    }

    void ChasePlayer()
    {
        animator.SetBool("isWalking", true);
        animator.SetBool("isAttacking", false);
        agent.SetDestination(player.position);
    }

    void AttackPlayer()
    {
        animator.SetBool("isWalking", false);
        animator.SetBool("isAttacking", true);
        agent.SetDestination(transform.position); // Stop moving to attack
    }
}
