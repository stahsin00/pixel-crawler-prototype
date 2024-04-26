using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : MonoBehaviour
{
    public Transform groundCheck;
    public float checkRadius = 0.5f;
    public LayerMask whatIsGround;
    public bool HasCollided { get; private set; } = false;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        HasCollided = true;
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        HasCollided = false;
    }
}
