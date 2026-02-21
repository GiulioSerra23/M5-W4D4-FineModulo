using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class PatrolState : FSM_BaseState
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
        _enemy.HandlePatrol();
    }

    public override void OnStateExit()
    {

    }
}
