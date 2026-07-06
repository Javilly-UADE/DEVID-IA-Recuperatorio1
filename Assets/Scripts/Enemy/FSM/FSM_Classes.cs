using UnityEngine;

public class FSM_Classes : MonoBehaviour
{
    public State CurrentState { get; private set; }

    private PatrolState patrolState;
    private PursuitState pursuitState;
    private FleeState fleeState;

    private void Awake()
    {
        patrolState = new PatrolState(this);
        pursuitState = new PursuitState(this);
        fleeState = new FleeState(this);

        CurrentState = patrolState;
    }

    public void UpdateState(bool canSeePlayer, bool isClose)
    {
        CurrentState.Update(canSeePlayer, isClose);
    }

    public void ChangeToPatrol()
    {
        ChangeState(patrolState);
    }

    public void ChangeToPursuit()
    {
        ChangeState(pursuitState);
    }

    public void ChangeToFlee()
    {
        ChangeState(fleeState);
    }

    private void ChangeState(State newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState.Exit();
        CurrentState = newState;
        CurrentState.Enter();
    }
}

public abstract class State
{
    protected FSM_Classes fsm;

    public State(FSM_Classes fsm)
    {
        this.fsm = fsm;
    }

    public virtual void Enter() { }

    public virtual void Exit() { }

    public abstract void Update(bool canSeePlayer, bool isClose);
}

public class PatrolState : State
{
    public PatrolState(FSM_Classes fsm) : base(fsm) { }

    public override void Update(bool canSeePlayer, bool isClose)
    {
        if (canSeePlayer)
        {
            fsm.ChangeToPursuit();
        }
    }
}

public class PursuitState : State
{
    public PursuitState(FSM_Classes fsm) : base(fsm) { }

    public override void Update(bool canSeePlayer, bool isClose)
    {
        if (!canSeePlayer)
        {
            fsm.ChangeToPatrol();
        }
    }
}

public class FleeState : State
{
    public FleeState(FSM_Classes fsm) : base(fsm) { }

    public override void Update(bool canSeePlayer, bool isClose)
    {
        
    }
}