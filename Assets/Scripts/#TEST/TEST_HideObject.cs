using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_HideObject : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TEST_Detector detector = FindObjectOfType<TEST_Detector>();
            if (detector != null)
            {
                detector.SetPlayerHidden(true);
                Debug.Log("Player is Hiding.");
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            TEST_Detector detector = FindObjectOfType<TEST_Detector>();
            if (detector != null)
            {
                detector.SetPlayerHidden(false);
                Debug.Log("Player is No Longer Hiding.");
            }
        }
    }
}
