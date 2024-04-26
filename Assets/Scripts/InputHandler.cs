using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    Player player;

    public float InputX { get; private set; }

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

        InputX = Input.GetAxisRaw("Horizontal");
    }
}
