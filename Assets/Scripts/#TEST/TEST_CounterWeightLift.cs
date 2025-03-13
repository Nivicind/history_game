using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_CounterWeightLift : MonoBehaviour
{
    public Rigidbody2D platformA; // First platform
    public Rigidbody2D platformB; // Second platform (counterweight)
    public float liftSpeed = 5f;  // Speed of movement
    public float maxHeight = 5f;  // Max movement range
    public float minHeight = -5f; // Min movement range

    private void FixedUpdate()
    {
        // Move platform A up and platform B down when A is lighter
        if (platformA.position.y < maxHeight && platformB.position.y > minHeight)
        {
            platformA.velocity = new Vector2(0, liftSpeed);
            platformB.velocity = new Vector2(0, -liftSpeed);
        }
        // Move platform A down and platform B up when A is heavier
        else if (platformA.position.y > minHeight && platformB.position.y < maxHeight)
        {
            platformA.velocity = new Vector2(0, -liftSpeed);
            platformB.velocity = new Vector2(0, liftSpeed);
        }
        else
        {
            // Stop movement if limits are reached
            platformA.velocity = Vector2.zero;
            platformB.velocity = Vector2.zero;
        }
    }
}
