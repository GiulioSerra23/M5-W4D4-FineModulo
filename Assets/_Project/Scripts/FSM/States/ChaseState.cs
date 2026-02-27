using UnityEngine;

public class ChaseState : FSM_BaseState
{
    [SerializeField] private float _chaseSpeed = 3;

    private BaseEnemy _enemy;

    public override EnemyState State => EnemyState.CHASE;

    public override void SetUp(FSM_Controller controller, Component owner)
    {
        base.SetUp(controller, owner);
        _enemy = owner as BaseEnemy;
    }

    public override void OnStateEnter()
    {
        _enemy.CanBeAlerted = false;
        _enemy.AlertAllies(_enemy.Detection.Target.position);
        _enemy.Detection.SetVision(1.6f, 1.4f);
    }

    public override void StateUpdate()
    {
        _enemy.Agent.isStopped = false;
        _enemy.Agent.SetDestination(_enemy.Detection.Target.position);
        _enemy.SetSpeed(_chaseSpeed);
    }

    public override void OnStateExit()
    {
        _enemy.CanBeAlerted = true;
        _enemy.ResetSpeed();
    }
}
