using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class gravity : MonoBehaviour
{
    private Rigidbody barrelRigidbody;

    private void Start()
    {
        barrelRigidbody = GetComponent<Rigidbody>();
    }

    private void OnTriggerStay(Collider other)
    {
        // Check if the barrel is touching a surface
        if (other.gameObject.CompareTag("Ground"))
        {
            // If the barrel is touching the ground, do nothing
            return;
        }

        // If the barrel is not touching a surface, set the Rigidbody to Dynamic
        barrelRigidbody.isKinematic = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the barrel has come to a stop on a new surface, set the Rigidbody back to Kinematic
        if (collision.gameObject.CompareTag("Ground"))
        {
            barrelRigidbody.isKinematic = true;
        }
    }
}
