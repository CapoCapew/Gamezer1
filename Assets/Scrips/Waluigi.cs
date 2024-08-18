using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class EnemyAI : MonoBehaviour
{
    public float detectionRange = 10f;
    public float attackRange = 2f;
    public float patrolTime = 5f;
    public Animator animator;
    public int maxHealth = 100;
    public Slider healthBar; // Reference to the health bar UI

    private Transform player;
    private NavMeshAgent agent;
    private float patrolTimer;
    private Vector3 patrolTarget;
    private int currentHealth;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        patrolTimer = patrolTime;
        SetNewPatrolTarget();
        agent.updatePosition = false; // Disable automatic position update

        currentHealth = maxHealth;
        UpdateHealthBar();

        // Find the player by tag
        player = GameObject.FindWithTag("Player").transform;
    }

    void Update()
    {
        if (player == null) return; // Ensure player is assigned

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

        // Apply the NavMeshAgent position to the transform
        if (agent.remainingDistance > agent.stoppingDistance)
        {
            transform.position = agent.nextPosition;
        }

        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
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

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        UpdateHealthBar();

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void UpdateHealthBar()
    {
        healthBar.value = (float)currentHealth / maxHealth;
    }

    void Die()
    {
        animator.SetBool("isDying", true);
        // Additional logic for when the enemy dies (e.g., disable AI, play sound, etc.)
        GetComponent<NavMeshAgent>().enabled = false;
        GetComponent<EnemyAI>().enabled = false;
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        // Wait for the animation to finish
        yield return new WaitForSeconds(10);
        Destroy(gameObject);
    }
}
