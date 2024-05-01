using UnityEngine;

public class AttackState : State
{
    GameObject attackPos;

    public AttackState(Entity entity, StateMachine stateMachine, GameObject attackPos) : base(entity, stateMachine)
    {
        this.attackPos = attackPos;
    }

    public override void Enter()
    {
        base.Enter();

        attackPos.SetActive(true);
    }

    public override void Exit()
    {
        entity.DoDamage();
        attackPos.SetActive(false);

        base.Exit();
    }
}
