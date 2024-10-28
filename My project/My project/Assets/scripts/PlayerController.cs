using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour // Class that accompanies the player
{
    public float runSpeed;
    public float jumpSpeed;
    public GameObject ballPrefab;

    private Rigidbody2D rb2D;
    private Animator anim;
    private SpriteRenderer sprtrRnd;
    private Transform trfm;
    private float lastShoot;
    private float waitShootTime;

    private Vector2 direccionFuego;

    public GameObject fireOut;

    private bool isSlashing;
    public float slashRange = 1f; // Range for the slash attack
    public int slashDamage = 1;   // Damage dealt by the slash

    // Start is called before the first frame update
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sprtrRnd = GetComponent<SpriteRenderer>();
        trfm = GetComponent<Transform>();
        waitShootTime = 0.5f;
    }

    // Update is called once per frame
    void Update()
    {
        checkJump(); // Move this from FixedUpdate to Update for better input detection
        checkMovement();
        Shoot();
        Slash(); // Call the Slash method in Update
    }

    private void FixedUpdate()
    {
        // This is now only for physics-related updates if needed
    }

    private void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.E) && (Time.time > lastShoot + waitShootTime))
        {
         GameObject fuego = Instantiate(ballPrefab, fireOut.transform.position, Quaternion.identity);
         if(sprtrRnd.flipX){
            direccionFuego = Vector2.left;

         }
         else {
            direccionFuego = Vector2.right;
         }
         fuego.GetComponent<BallController>().setDirection(direccionFuego);
        }
    
    }

    private void checkJump() 
    {
        // Checks if jump should be triggered
        if (Input.GetKeyDown(KeyCode.Space) && ChechGround.isGrounded)
        {
            Jump();
        }
        else if (ChechGround.isGrounded && rb2D.velocity.y <= 0) 
        {
            anim.SetBool("isJumping", false); // Reset to false when grounded
        }
    }

    private void Jump()
    {
        // Apply jump force
        rb2D.velocity = new Vector2(rb2D.velocity.x, jumpSpeed);
        anim.SetBool("isJumping", true);
    }

   private void Slash()
{
    // Check if the slash key is pressed and the player is not already slashing
    if (Input.GetKeyDown(KeyCode.C) && !isSlashing)
    {
        isSlashing = true; // Set the flag to prevent retriggering
        anim.SetTrigger("isSlashing"); // Trigger the slash animation

        // Detect enemies in range and deal damage
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(trfm.position + new Vector3(sprtrRnd.flipX ? -slashRange : slashRange, 0, 0), slashRange);
        foreach (Collider2D enemy in hitEnemies)
        {
            if (enemy.CompareTag("Enemy"))
            {
                enemy.GetComponent<EnemyController>().TakeDamage(slashDamage); // Deal damage
            }
        }

        // Start coroutine to reset the isSlashing state after the animation length
        StartCoroutine(ResetSlash());
    }
}

private IEnumerator ResetSlash()
{
    // Wait for the length of the slashing animation to complete
    yield return new WaitForSeconds(0.933f); // Match this to your animation length
    isSlashing = false; // Reset the slashing state to allow a new slash
}

    private void checkMovement() 
    {
        bool isRunning = false; // Initialize isRunning as false

        if (Input.GetKey(KeyCode.A))
        {
            rb2D.velocity = new Vector2(-runSpeed, rb2D.velocity.y);
            isRunning = true; // Set isRunning to true if moving left
            sprtrRnd.flipX = true;
        }
        else if (Input.GetKey(KeyCode.D))
        {
            rb2D.velocity = new Vector2(runSpeed, rb2D.velocity.y);
            isRunning = true; // Set isRunning to true if moving right
            sprtrRnd.flipX = false;
        }
        else
        {
            // Player is idle
            rb2D.velocity = new Vector2(0, rb2D.velocity.y);
        }

        // Set the isRunning parameter in the Animator based on movement state
        anim.SetBool("isRunning", isRunning);
    }
}
