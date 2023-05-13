using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gliding : MonoBehaviour
{
    
    public float glideDescentSpeed = 2f;
    private bool isGliding = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // Implement your jump logic for the frog here
        }

        if (Input.GetKeyDown(KeyCode.G))
        {
            StartGliding();
        }
        else if (Input.GetKeyUp(KeyCode.G))
        {
            StopGliding();
        }
    }

    void FixedUpdate()
    {
        if (isGliding)
        {
            ApplyGlideDescent();
        }
    }

    void StartGliding()
    {
        isGliding = true;
    }

    void StopGliding()
    {
        isGliding = false;
    }

    void ApplyGlideDescent()
    {
        rb.velocity = new Vector2(rb.velocity.x, -glideDescentSpeed);
    }
}
