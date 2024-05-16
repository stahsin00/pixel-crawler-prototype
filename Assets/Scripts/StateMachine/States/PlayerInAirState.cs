using UnityEngine;

public class PlayerInAirState : InAirState
{
    public PlayerInAirState(Player player, StateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void Update()
    {
        base.Update();

        Player player = entity as Player;
        if (Mathf.Abs(player.inputHandler.InputX) > 0.1f) {
            player.movement.SetVelocityX(player.speed);
        } else {
            player.movement.SetVelocityX(0);
        }
    }
}
