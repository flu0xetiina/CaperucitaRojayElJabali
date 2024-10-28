using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public int speedBall = 12; // Default speed of the ball
    public float timeAlive = 4f; // Lifetime of the ball

    private Rigidbody2D rb2Dball;
    private float startTime;

    void Start()
    {
        rb2Dball = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    // Initialize the ball's movement in the given direction
    public void InitializeBall(float direction)
    {
        // Set the velocity immediately based on direction
        rb2Dball.velocity = new Vector2(direction * speedBall, 0);
        // Offset the position slightly in the direction to avoid initial overlap
        transform.position += new Vector3(direction * 0.5f, 0, 0);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            // Logic for handling collisions with enemies, if necessary
            Destroy(gameObject); // Destroy the ball after a hit
        }
    }

    void FixedUpdate()
    {
        // Destroy the ball if it exceeds its lifetime
        if (Time.time - startTime > timeAlive)
        {
            Destroy(gameObject);
        }
    }
}