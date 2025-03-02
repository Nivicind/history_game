using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

<<<<<<< HEAD

public class PlayerCrouch : MonoBehaviour
{

    public Rigidbody2D Me;

    //public SpriteRenderer SpriteRenderer;

    public Sprite Standing;
    public Sprite Crouching;

    public CapsuleCollider2D Collider;


    public Vector2 StandingSize;
    public Vector2 CrouchingSize;






    // Start is called before the first frame update
    void Start()
    {
        Me = GetComponent<Rigidbody2D>();
        Collider = GetComponent<CapsuleCollider2D>();

        Collider.size = StandingSize;

        //SpriteRenderer = GetComponent<SpriteRenderer>();
        //SpriteRenderer.sprite = Standing;





    }

    // Update is called once per frame
    void Update()
    {
        //MovementX = Input.GetAxisRaw("Horizontal");
        //MovementY = Input.GetAxisRaw("Vertical");

        //Me.velocity = new Vector2(MovementX * Time.deltaTime * speed, MovementY * Time.deltaTime * speed);


        if (Input.GetKeyDown(KeyCode.C))
        {
            //SpriteRenderer.sprite = Crouching;

            Collider.size = CrouchingSize;

=======
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
>>>>>>> b8982f2ecf1cb1adc76f9242e097dd6f13bd58c2
        }

        if (Input.GetKeyUp(KeyCode.C))
        {
<<<<<<< HEAD
            //SpriteRenderer.sprite = Standing;

            Collider.size = StandingSize;

        }

    }




=======
            PlayerCollider.size = StandingSize;
            PlayerCollider.offset = StandingOffset;
        }
    }
>>>>>>> b8982f2ecf1cb1adc76f9242e097dd6f13bd58c2
}
