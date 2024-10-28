using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour  // Clase que acompa�a al enemigo
{
    public int runEnemy;
    public Transform transformPlayer;
    [Range(1, 5)] public int vida;

    private Rigidbody2D rb2D;
    private Transform transformEnemy;
    private SpriteRenderer sprtEnemy;
    private Animator animEnemy;
    private int factorX;
    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        transformEnemy = GetComponent<Transform>();
        animEnemy = GetComponent<Animator>();
        sprtEnemy = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        checkMov();
        
    }

    private void checkMov() {
        if (transformPlayer.position.x - transformEnemy.position.x <= 0)
        {
            factorX = -1;
            sprtEnemy.flipX = false;
        }
        else
        {
            factorX = 1;
            sprtEnemy.flipX = true;
        }

        if (checkArea.checkFollow)
        {
            rb2D.velocity = new Vector2(factorX * runEnemy, rb2D.velocity.y);
            animEnemy.SetBool("isRunning", true);
        }
        else
        {
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
            animEnemy.SetBool("isRunning", false);
        }
    }
     public void TakeDamage(int damage)
    {
        vida -= damage;
        
        if (vida <= 0)
        {
            Die();
        }
        else
        {
            // Optionally trigger a "hit" animation
            GetComponent<Animator>().SetTrigger("hit");
        }
    }

    private void Die()
    {
        Destroy(gameObject); // Destroy the enemy game object
    }
}
