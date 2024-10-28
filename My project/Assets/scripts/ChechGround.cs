using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChechGround : MonoBehaviour
{
    public static bool isGrounded;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the object has the "Ground" tag
        if (collision.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Check if the object has the "Ground" tag
        if (!collision.CompareTag("Ground"))
        {    isGrounded = false;
            //return;
        }
        //isGrounded = false;
    }
}