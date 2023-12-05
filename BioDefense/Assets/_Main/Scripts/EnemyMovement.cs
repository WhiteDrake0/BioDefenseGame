using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class EnemyMovement : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Rigidbody2D rb;


    [Header("Attributes")]
    [SerializeField] private float moveSpeed = 2f;


    private Animator animator;
    private Vector3 lastPosition;
 

    
    private Transform target;
    private int pathIndex = 0;
    public static int pathIndex1; 

    private float baseSpeed;

    private void Start()
    {
        baseSpeed = moveSpeed;
        target = LevelManager.main.path[pathIndex];

        animator = GetComponent<Animator>();
        lastPosition = transform.position;
    }

    private void Update()
    {
        if (Vector2.Distance(target.position, transform.position) <= 0.1f)
        {
            pathIndex++;

            if (pathIndex == LevelManager.main.path.Length)
            {
                EnemySpawner.onEnemyDestroy.Invoke();
                Destroy(gameObject);
                pathIndex1 = pathIndex;


                return;
            }
            else
            {
                target = LevelManager.main.path[pathIndex];
            }
        }

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


    private void FixedUpdate()
    {
        Vector2 direction = (target.position - transform.position).normalized;
        rb.velocity = direction * moveSpeed;
    }

    public void UpdateSpeed(float newSpeed)
    {
        moveSpeed = newSpeed;
    }

    public void ResetSpeed()
    {
        moveSpeed = baseSpeed;
    }
}

