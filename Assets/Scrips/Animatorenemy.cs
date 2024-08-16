using UnityEngine;

public class EnemyAnimatorController : MonoBehaviour
{
    private Animator _animator;
    private bool _isWalking = false; // Change this based on your game logic

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        // Your logic to determine if the enemy should walk
        // For example, if the player is nearby, set _isWalking to true

        // Update the animator parameter
        _animator.SetBool("isWalking", _isWalking);
    }
}

