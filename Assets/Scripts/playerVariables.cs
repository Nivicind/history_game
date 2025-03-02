using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerVariables : MonoBehaviour
{
    // Static variables
<<<<<<< HEAD
    public static bool isWalking, isJumping, isGrabbing;
=======
    public static bool isWalking, isJumping, isGrabbing, isGrounded;
>>>>>>> b8982f2ecf1cb1adc76f9242e097dd6f13bd58c2

    // Start is called before the first frame update
    void Start()
    {
        // Initial log, only logs once when the game starts
        Debug.Log("Initial values: isWalking = " + isWalking + ", isJumping = " + isJumping + ", isGrabbing = " + isGrabbing);
    }

    // Update is called once per frame
    void Update()
    {
        // Log the values at the same position every frame
        UpdateLog();

    }

    // Method to update the log without stacking new logs
    private void UpdateLog()
    {
        // This clears the console (optional)


        // This will log the current values of isWalking, isJumping, and isGrabbing at the same position
<<<<<<< HEAD
        Debug.Log("isWalking: " + isWalking + ", isJumping: " + isJumping + ", isGrabbing: " + isGrabbing);
=======
        Debug.Log("isWalking: " + isWalking + ", isJumping: " + isJumping + ", isGrabbing: " + isGrabbing + ", isGrounded: " + isGrounded);
>>>>>>> b8982f2ecf1cb1adc76f9242e097dd6f13bd58c2
    }

}
