using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCrouch : MonoBehaviour
{
    [Header("Player Components")]
    public Rigidbody2D Player;
    public CapsuleCollider2D PlayerCollider;

    [Header("Player Sprites")]
    public Sprite StandingSprite;
    public Sprite CrouchingSprite;

    [Header("Collider Sizes")]
    public Vector2 StandingSize;
    public Vector2 CrouchingSize;

    [Header("Collider Offsets")]
    public Vector2 StandingOffset;
    public Vector2 CrouchingOffset;

    void Start()
    {
        Player = GetComponent<Rigidbody2D>();
        PlayerCollider = GetComponent<CapsuleCollider2D>();

        PlayerCollider.size = StandingSize;
        PlayerCollider.offset = StandingOffset;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            PlayerCollider.size = CrouchingSize;
            PlayerCollider.offset = CrouchingOffset;
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            PlayerCollider.size = StandingSize;
            PlayerCollider.offset = StandingOffset;
        }
    }
}
