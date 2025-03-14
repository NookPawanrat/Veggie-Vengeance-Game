using System.Collections;
using System.Collections.Generic;
using Unity.Burst.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 2f;
    public Transform leftBoundary;  // Reference to the empty object on the left
    public Transform rightBoundary; // Reference to the empty object on the right
    [SerializeField] private bool hit;
    [SerializeField] private Transform enemyPos;

    private int direction = 1;
    

    void Update()
    {
        // Walk left and right
        transform.Translate(Vector2.right * speed * direction * Time.deltaTime);

        // Check if it has reached the boundaries
        if (transform.position.x <= leftBoundary.position.x || transform.position.x >= rightBoundary.position.x)
        {
            direction *= -1; // Reverse direction
        }
    }

}
