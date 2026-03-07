using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedState : FSM_BaseState
{
    private BaseEnemy _enemy;

    public override State State => State.GRABBED;

    public override void SetUp(FSM_Controller controller, Component owner)
    {
        base.SetUp(controller, owner);
        _enemy = owner as BaseEnemy;
    }

    public override void OnStateEnter()
    {
        _enemy.SetGrabbedState(true);
    }

    public override void StateUpdate() { }  

    public override void OnStateExit()
    {
        _enemy.SetGrabbedState(false);
    }    
}
