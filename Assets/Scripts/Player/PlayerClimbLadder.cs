using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerClimbLadder : MonoBehaviour
{
    [SerializeField]
    private PlayerMovement player;

    private void Awake()
    {
        if (player == null)
        {
            player = GetComponent<PlayerMovement>();
            if (player == null)
            {
                Debug.LogError("PlayerMovement is not assigned in PlayerClimbLadder!");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<Ladder>() && PlayerVariables.isJumping == false && PlayerVariables.isFalling == false)
        {
            if (player != null)
            {
                player.ClimbingAllowed = true;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<Ladder>())
        {
            if (player != null)
            {
                player.ClimbingAllowed = false;
            }
        }
    }
}
