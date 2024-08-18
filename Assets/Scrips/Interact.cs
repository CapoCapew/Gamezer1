using UnityEngine;
using UnityEngine.UI;

public class InteractionScript : MonoBehaviour
{
    public GameObject messageUI; // UI pour afficher le message
    public string messageText = "Appuyez sur 'E' pour interagir"; // Texte du message
    public Animator animator; // L'animator pour lancer l'animation
    public GameObject enemyPrefab; // Le prefab de l'ennemi à invoquer
    public Transform spawnPoint; // Le point de spawn de l'ennemi

    private bool isNearObject = false;

    void Start()
    {
        messageUI.SetActive(false); // Masquer le message au début
    }

    void Update()
    {
        if (isNearObject && Input.GetKeyDown(KeyCode.E))
        {
            messageUI.SetActive(false); // Masquer le message
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // Invoquer l'ennemi

            
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearObject = true;
            messageUI.SetActive(true); // Afficher le message
            messageUI.GetComponent<Text>().text = messageText; // Mettre à jour le texte du message
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isNearObject = false;
            messageUI.SetActive(false); // Masquer le message
        }
    }
}
