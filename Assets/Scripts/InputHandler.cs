using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Player player;

    public float InputX { get; private set; }
    public bool JumpInput { get; private set; } = false;

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
            JumpInput = true;
            return;
        } 

        InputX = Input.GetAxisRaw("Horizontal");
        player.movement.Flip(InputX);
    }

    public void UseJumpInput() {
        JumpInput = false;
    }
}
