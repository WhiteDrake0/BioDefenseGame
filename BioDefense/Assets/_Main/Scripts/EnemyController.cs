using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private Animator animator;
    private bool isFacingRight = true; // Assuming the enemy starts facing right

    void Start()
    {
        // Assuming the Animator component is on the same GameObject as this script
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Detect input or conditions to determine the direction
        // For simplicity, let's say you use the horizontal input axis
        float horizontalInput = Input.GetAxis("Horizontal");

        if (horizontalInput > 0)
        {
            // Moving right
            Flip(true);
        }
        else if (horizontalInput < 0)
        {
            // Moving left
            Flip(false);
        }

        // Other game logic...

        // Set the "IsFacingRight" parameter in the Animator
        animator.SetBool("IsFacingRight", isFacingRight);
    }

    void Flip(bool faceRight)
    {
        // Flip the sprite horizontally
        transform.localScale = new Vector3(faceRight ? 1 : -1, 1, 1);

        // Update the facing direction
        isFacingRight = faceRight;
    }
}
