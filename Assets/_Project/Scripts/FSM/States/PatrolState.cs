using UnityEngine;

public class PatrolState : FSM_BaseState
{
    private BaseEnemy _enemy;

    public override State State => State.PATROL;

    public override void SetUp(FSM_Controller controller, Component owner)
    {
        base.SetUp(controller, owner);
        _enemy = owner as BaseEnemy;
    }

    public override void OnStateEnter()
    {
        _enemy.Detection.ResetVision();
    }

    public override void StateUpdate()
    {
        _enemy.HandlePatrol();
    }

    public override void OnStateExit() { }
}
