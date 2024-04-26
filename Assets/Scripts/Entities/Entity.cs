using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour
{
    // Components
    public Movement movement;
    public CollisionSenses collisionSenses;

    // State
    public StateMachine stateMachine;

    // States
    public IdleState idleState;
    public WalkState walkState;
    public JumpState jumpState;
    public InAirState inAirState;

    // Stats
    public float speed;

    protected virtual void Awake()
    {
        movement = GetComponent<Movement>();
        collisionSenses = GetComponent<CollisionSenses>();

        stateMachine = new StateMachine();

        idleState = new IdleState(this, stateMachine);
        walkState = new WalkState(this, stateMachine);
        jumpState = new JumpState(this, stateMachine);
        inAirState = new InAirState(this, stateMachine);
    }

    protected virtual void Update()
    {
        stateMachine.CurrentState.Update();
    }

    private void FixedUpdate()
    {
        stateMachine.CurrentState.FixedUpdate();
    }
}
