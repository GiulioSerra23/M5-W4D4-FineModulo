using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : FSM_BaseState
{
    private BaseEnemy _enemy;

    public override void SetUp(FSM_Controller controller, Component owner)
    {
        base.SetUp(controller, owner);
        _enemy = owner as BaseEnemy;
    }

    public override void OnStateEnter()
    {
        
    }

    public override void StateUpdate()
    {
        Transform target = _enemy.Detection.Target;

        _enemy.HandleChase(target);
    }

    public override void OnStateExit()
    {

    }
}
