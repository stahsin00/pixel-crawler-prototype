using UnityEngine;

public class AttackState : State
{
    float attack = 1f;
    GameObject attackPos;

    public AttackState(Entity entity, StateMachine stateMachine, GameObject attackPos) : base(entity, stateMachine)
    {
        this.attackPos = attackPos;
    }

    public override void Enter()
    {
        base.Enter();

        Debug.Log("Attack");
        attackPos.SetActive(true);
    }

    public override void Exit()
    {
        // TODO
        attackPos.SetActive(false);

        base.Exit();
    }
}
