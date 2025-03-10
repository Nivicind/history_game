using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerVariables : MonoBehaviour
{
    // Static variables - có thể truy cập từ các script khác
    public static bool isWalking, isJumping, isGrabbing, isGrounded, isFalling;
    private Rigidbody2D RB;

    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private Vector2 groundCheckSize = new Vector2(0.49f, 0.03f);
    [SerializeField] private LayerMask groundLayer;




    private void HandleFalling()
    {
        if (RB.velocity.y < -0.1f) // Slight threshold to prevent premature falling state
        {
            isFalling = true;


        }
        else if (RB.velocity.y > 0.1f)
        {
            isFalling = false;


        }
        else
        {
            isFalling = false;

        }
    }

    void Start()
    {
        RB = GetComponent<Rigidbody2D>();
        // Kiểm tra nếu groundCheckPoint chưa được gán thì tìm hoặc tạo
        if (groundCheckPoint == null)
        {
            groundCheckPoint = transform.Find("GroundCheck");

            if (groundCheckPoint == null)
            {
                Debug.LogWarning("GroundCheck không được gán! Đang tạo một GroundCheck mới.");
                GameObject obj = new GameObject("GroundCheck");
                obj.transform.parent = transform;
                obj.transform.localPosition = new Vector3(0, -0.5f, 0); // Điều chỉnh vị trí phù hợp
                groundCheckPoint = obj.transform;
            }
        }

        Debug.Log("Initial values: isWalking = " + isWalking + ", isJumping = " + isJumping + ", isGrabbing = " + isGrabbing);
    }

    void Update()
    {
        // Kiểm tra nhân vật có đang chạm đất không
        isGrounded = Physics2D.OverlapBox(groundCheckPoint.position, groundCheckSize, 0, groundLayer);
        HandleFalling();

        // Ghi log trạng thái hiện tại
        UpdateLog();
    }

    private void UpdateLog()
    {
        Debug.Log("isWalking: " + isWalking + ", isJumping: " + isJumping + ", isGrabbing: " + isGrabbing + ", isGrounded: " + isGrounded + ", isFalling: " + isFalling );
    }

    // Hiển thị Gizmos để dễ debug vùng check ground
    void OnDrawGizmos()
    {
        if (groundCheckPoint != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireCube(groundCheckPoint.position, groundCheckSize);
        }
    }
}
