using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player; // Référence au joueur
    public float detectionRange = 10f; // Portée de détection
    public float attackRange = 2f; // Portée d'attaque
    public float attackCooldown = 2f; // Temps entre les attaques
    public Animator animator; // Référence à l'animator
	
    private NavMeshAgent navMeshAgent;
    private float lastAttackTime;

    private void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        lastAttackTime = -attackCooldown; // Permet à l'ennemi d'attaquer immédiatement au démarrage
    }

    private void Update()
    {
        // Vérifie si le joueur est dans la portée de détection
        if (Vector3.Distance(transform.position, player.position) <= detectionRange)
        {
            // Déplace l'ennemi vers le joueur
            navMeshAgent.SetDestination(player.position);

            // Vérifie si le joueur est dans la portée d'attaque
            if (Vector3.Distance(transform.position, player.position) <= attackRange)
            {
                // Vérifie le temps écoulé depuis la dernière attaque
                if (Time.time - lastAttackTime >= attackCooldown)
                {
                    // Attaque le joueur (vous pouvez ajouter votre propre logique ici)
                    animator.SetTrigger("Attack"); // Déclenche l'animation d'attaque
                    lastAttackTime = Time.time; // Met à jour le temps de la dernière attaque
                }
            }
        }
        else
        {
            // Si le joueur n'est pas détecté, l'ennemi s'arrête
            navMeshAgent.SetDestination(transform.position);
        }
    }
}
