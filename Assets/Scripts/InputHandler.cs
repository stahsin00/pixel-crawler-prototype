using UnityEngine;

// TODO
public class InputHandler : MonoBehaviour
{
    Player player;

    public float InputX { get; private set; }
    public bool JumpInput { get; private set; } = false;
    public bool AttackInput { get; private set; } = false;

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
        // TODO : lol
        if (Input.GetKeyDown(KeyCode.Space))
        {
            JumpInput = true;
            return;
        } else if (Input.GetKeyDown(KeyCode.X))
        {
            AttackInput = true;
            return;
        } else if (Input.GetKeyDown(KeyCode.R)) {
            player.projectileSpawner.ToggleRandom();
        } else if (Input.GetKeyDown(KeyCode.F)) {
            player.projectileSpawner.Fire();
        }

        InputX = Input.GetAxisRaw("Horizontal");
        player.movement.Flip(InputX);
    }

    public void UseJumpInput() {
        JumpInput = false;
    }

    public void UseAttackInput() {
        AttackInput = false;
    }
}
