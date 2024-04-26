using System.Collections.Generic;
using UnityEngine;

public abstract class State
{
    protected Entity entity;
    protected StateMachine stateMachine;
    protected List<Transition> transitions;

    protected float startTime;

    public State(Entity entity, StateMachine stateMachine)
    {
        this.entity = entity;
        this.stateMachine = stateMachine;

        transitions = new List<Transition>();

        // TODO : temp
        AddTransition(new EntityFallTransition(entity));
        AddTransition(new EntityLandTransition(entity));
    }

    public void AddTransition(Transition transition)
    {
        transitions.Add(transition);
    }

    public void PrintTransitions() {
        foreach (var transition in transitions)
        {
            Debug.Log("PrintTransitions: " + transition.GetType().Name);
        }
    }

    public virtual void Enter()
    {
        startTime = Time.time;
    }

    public virtual void Update()
    {
        // TODO : prioritize transitions?
        foreach (Transition transition in transitions)
        {
            if (transition.ShouldTransition())
            {
                Debug.Log(transition.NextState().GetType().Name);
                //stateMachine.ChangeState(transition.NextState());
                break;
            }
        }
    }

    public virtual void FixedUpdate() {}

    public virtual void Exit() {}
}
