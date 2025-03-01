using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


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

        }

        if (Input.GetKeyUp(KeyCode.C))
        {
            //SpriteRenderer.sprite = Standing;

            Collider.size = StandingSize;

        }

    }




}
