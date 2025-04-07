using UnityEngine;

public class CounterweightLift : MonoBehaviour
{
    private HingeJoint2D hinge;
    private bool isSpinning = false;

    void Start()
    {
        hinge = GetComponent<HingeJoint2D>();
        hinge.useMotor = false;  // Initially not spinning
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            StartSpinning();
        }
    }

    void StartSpinning()
    {
        if (!isSpinning)
        {
            JointMotor2D motor = hinge.motor;
            motor.motorSpeed = 200f;  // Adjust speed (negative for opposite direction)
            motor.maxMotorTorque = 1000f;
            hinge.motor = motor;
            hinge.useMotor = true;
            isSpinning = true;
        }
    }
}