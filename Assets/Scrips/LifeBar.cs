using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Transform enemy; // Référence à l'ennemi
    public Vector3 offset; // Décalage pour positionner la barre de vie au-dessus de la tête

    private Slider healthBar;

    void Start()
    {
        healthBar = GetComponentInChildren<Slider>();
    }

    void Update()
    {
        if (enemy != null)
        {
            // Mettre à jour la position de la barre de vie
            transform.position = enemy.position + offset;
        }
    }

    public void SetHealth(float health, float maxHealth)
    {
        healthBar.value = health / maxHealth;
    }
}
