using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public InputHandler inputHandler;
    public ProjectileSpawner projectileSpawner;

    public PlayerJumpState jumpState;
    public PlayerAttackState attackState;

    protected override void Awake()
    {
        base.Awake();

        inputHandler = GetComponent<InputHandler>();
        projectileSpawner = GetComponent<ProjectileSpawner>();

        jumpState = new PlayerJumpState(this, stateMachine);
        attackState = new PlayerAttackState(this, stateMachine, attackPos);

        idleState.AddTransition(new PlayerIdleTransition(this));
        walkState.AddTransition(new PlayerMoveTransition(this));
        jumpState.AddTransition(new PlayerJumpFallTransition(this));

        PlayerJumpTransition playerJumpTransition= new PlayerJumpTransition(this);
        idleState.AddTransition(playerJumpTransition);
        walkState.AddTransition(playerJumpTransition);

        idleState.AddTransition(new PlayerAttackTransition(this));
        attackState.AddTransition(new PlayerFinishAttackTransition(this));
    }

    void Start()
    {
        // TODO : temp
        speed = 10f;
        stateMachine.Initialize(idleState);
    }

    public override void DoDamage()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPos.transform.position, 0.5f, whatIsDamageable);
        foreach (Collider2D collision in hits) {
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (damageable != null) {
                damageable.Damage(attack);
            }
        }
    }
}
