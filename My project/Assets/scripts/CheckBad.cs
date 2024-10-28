using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckBad : MonoBehaviour
{
    public Transform spawnPoint; // Assign this in the Inspector
    public GameObject player; // Assign the Player GameObject in the Inspector

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bad"))
        {
            Debug.Log("Collision with 'Bad' object detected.");
            RespawnPlayer();
        }
    }

    private void RespawnPlayer()
    {
        // Set the player's position to the spawn point position
        player.transform.position = spawnPoint.position;
        Debug.Log("Player has been respawned.");
    }
}