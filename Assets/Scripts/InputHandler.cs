using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Player player;

    private void Awake()
    {
        player = GetComponent<Player>();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.stateMachine.ChangeState(player.jumpState);
            return;
        } 

        float horizontalInput = Input.GetAxisRaw("Horizontal");

        if (Mathf.Abs(horizontalInput) > 0.1f)
        {
            player.stateMachine.ChangeState(player.walkState);
            player.movement.Flip(horizontalInput);
        }
        else if (player.collisionSenses.IsGrounded() && player.movement.currentVelocity.y < 0.01f && player.stateMachine.CurrentState != player.idleState)
        {
            player.stateMachine.ChangeState(player.idleState);
        } else if (!player.collisionSenses.IsGrounded() && player.movement.currentVelocity.y < 0.01f &&  player.stateMachine.CurrentState != player.inAirState)
        {
            player.stateMachine.ChangeState(player.inAirState);
        }
    }
}
