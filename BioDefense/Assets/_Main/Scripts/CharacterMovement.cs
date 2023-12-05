using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Animator animator;
    private Vector3 lastPosition;

    void Start()
    {
        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    void Update()
    {
        // Calculate the direction based on the change in position
        Vector3 currentPosition = transform.position;
        Vector3 movementDirection = (currentPosition - lastPosition).normalized;

        // Update Animator parameters based on the movement direction
        UpdateAnimatorParameters(movementDirection);

        // Update the last position for the next frame
        lastPosition = currentPosition;
    }

    void UpdateAnimatorParameters(Vector3 direction)
    {
        // Set parameters in the Animator based on the movement direction
        animator.SetFloat("Horizontal", direction.x);
        animator.SetFloat("Vertical", direction.y);
    }
}
