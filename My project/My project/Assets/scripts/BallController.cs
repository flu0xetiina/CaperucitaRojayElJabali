using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public int speedBall; // Default speed of the ball
    public float vidaFuego; // Lifetime of the ball

    private Vector2 direccionFuego;

    private Rigidbody2D rb2Dball;
    private float startTime = 0f;

    void Start()
    {
        rb2Dball = GetComponent<Rigidbody2D>();
        startTime = Time.time;
    }

    // Initialize the ball's movement in the given direction
   private void movimientoFuego(){
    transform.Translate(direccionFuego*speedBall*Time.fixedDeltaTime);
    if (direccionFuego == Vector2.right){
        GetComponent<SpriteRenderer>().flipX=false;
    }
    else {
        GetComponent<SpriteRenderer>().flipX=true;

    }
    startTime += Time.fixedDeltaTime;
    if (startTime>=vidaFuego){
        Destroy(gameObject);
    }
   }

    public void setDirection(Vector2 direccion) {
        direccionFuego = direccion;
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
        movimientoFuego();
    }
}