using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{

    private Rigidbody2D rb;
    private bool isBeingPushed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isBeingPushed && rb.velocity.magnitude > 0f)
        {
            // If the object is not being pushed and is still moving, apply a force in the opposite direction to stop it
            rb.AddForce(-rb.velocity.normalized * 10f, ForceMode2D.Impulse);
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Princess"))
        {
            // Calculate the direction of the push based on the player's position relative to the object
            Vector2 direction = transform.position - collision.gameObject.transform.position;

            // Apply a force to the object in the direction of the push
            rb.AddForce(direction.normalized * 500f);
            isBeingPushed = true;
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Princess"))
        {
            // Remove the force applied to the object when the player stops pushing it
            rb.velocity = Vector2.zero;
            isBeingPushed = false;
            rb.bodyType = RigidbodyType2D.Kinematic;
        }
    }
}
