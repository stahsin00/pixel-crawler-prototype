using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Rigidbody2D rb;

    int facingDirection = 1;
    public Vector2 currentVelocity;

    public bool CanMove { get; set;} = true;
    public bool CanJump { get; set;} = true;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        facingDirection = 1;
        currentVelocity = Vector2.zero;
        rb.velocity = currentVelocity;
    }

    void Update()
    {
        currentVelocity = rb.velocity;
    }

    public void SetVelocityX(float velocity)
    {
        currentVelocity = new Vector2(velocity * facingDirection, currentVelocity.y);
        rb.velocity = currentVelocity;
    }

    public void SetVelocityY(float velocity)
    {
        currentVelocity = new Vector2(currentVelocity.x, velocity);
        rb.velocity = currentVelocity;
    }

    public void Flip(float input)
    {
        if (input != 0 && input != facingDirection)
        {
            Flip();
        }
    }

    public void Flip()
    {
        facingDirection *= -1;
    }
}
