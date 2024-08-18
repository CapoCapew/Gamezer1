using UnityEngine;

public class Bench : MonoBehaviour
{
    public KeyCode interactKey = KeyCode.E; // Touche d'interaction
    public float timeScale = 10f; // Facteur d'accélération du temps
    private bool isSitting = false;
    private float originalTimeScale;
    private bool playerNearby = false;
    private GameObject player; // Référence au joueur

    void Start()
    {
        originalTimeScale = Time.timeScale; // Sauvegarder l'échelle de temps originale
    }

    void Update()
    {
        if (playerNearby && Input.GetKeyDown(interactKey))
        {
            ToggleSit();
        }
    }

    void ToggleSit()
    {
        isSitting = !isSitting;
        if (isSitting)
        {
            Time.timeScale = timeScale; // Accélérer le temps
            DisablePlayerMovement(); // Désactiver le mouvement du joueur
        }
        else
        {
            Time.timeScale = originalTimeScale; // Restaurer l'échelle de temps originale
            EnablePlayerMovement(); // Réactiver le mouvement du joueur
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = true;
            player = other.gameObject; // Sauvegarder la référence au joueur
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerNearby = false;
            player = null; // Réinitialiser la référence au joueur
        }
    }

    void DisablePlayerMovement()
    {
        if (player != null)
        {
            // Désactiver le CharacterController ou Rigidbody du joueur
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = false;
            }
            else
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = true;
                }
            }
        }
    }

    void EnablePlayerMovement()
    {
        if (player != null)
        {
            // Réactiver le CharacterController ou Rigidbody du joueur
            CharacterController controller = player.GetComponent<CharacterController>();
            if (controller != null)
            {
                controller.enabled = true;
            }
            else
            {
                Rigidbody rb = player.GetComponent<Rigidbody>();
                if (rb != null)
                {
                    rb.isKinematic = false;
                }
            }
        }
    }
}
