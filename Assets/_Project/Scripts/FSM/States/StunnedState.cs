using Unity.VisualScripting;
using UnityEngine;

public class StunnedState : FSM_BaseState
{
    private BaseEnemy _enemy;
    private LineRenderer _lineRenderer;
    private float _timer;

    public override void SetUp(FSM_Controller controller, Component owner)
    {
        base.SetUp(controller, owner);
        _enemy = owner as BaseEnemy;
        _lineRenderer = _enemy.GetComponentInChildren<LineRenderer>();
    }

    public override void OnStateEnter()
    {
        _timer = 0f;

        _enemy.Agent.isStopped = true;
        _enemy.CanBeAlerted = false;
        _enemy.Detection.enabled = false;
    }

    public override void StateUpdate()
    {
        _timer += Time.deltaTime;
        _lineRenderer.enabled = false;

        if (_timer >= _enemy.StunDuration)
        {           
            _enemy.IsStunned = false;
            _lineRenderer.enabled = true;
        }
    }

    public override void OnStateExit()
    {
        _enemy.Agent.isStopped = false;
        _enemy.CanBeAlerted = true;
        _enemy.Detection.enabled = true;
    }    
}
