using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    // Components
    public Movement movement;
    public CollisionSenses collisionSenses;
    public Combat combat;

    // State
    public StateMachine stateMachine;

    // States
    public IdleState idleState;
    public WalkState walkState;
    public InAirState inAirState;

    // Stats
    public float speed;

    public GameObject attackPos;

    protected virtual void Awake()
    {
        movement = GetComponent<Movement>();
        collisionSenses = GetComponent<CollisionSenses>();
        combat = GetComponent<Combat>();

        stateMachine = new StateMachine();

        idleState = new IdleState(this, stateMachine);
        walkState = new WalkState(this, stateMachine);
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

    public void Damage(float amount)
    {
        Debug.Log("Damage: " + amount);
        combat.Damage(amount);
    }
}
