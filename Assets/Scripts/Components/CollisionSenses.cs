using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionSenses : MonoBehaviour
{
    public Transform groundCheck;
    public float checkRadius = 0.5f;
    public LayerMask whatIsGround;

    public bool IsGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);
    }
}
