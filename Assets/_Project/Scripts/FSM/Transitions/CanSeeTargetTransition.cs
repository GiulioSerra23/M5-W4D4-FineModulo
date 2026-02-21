using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanSeeTargetTransition : FSM_BaseTransition
{
    private BaseEnemy _enemy;

    public override void SetUp(FSM_BaseState ownerState, FSM_Controller controller, Component owner)
    {
        base.SetUp(ownerState, controller, owner);
        _enemy = owner as BaseEnemy;
    }

    public override bool IsConditionMet()
    {
        return _enemy.Detection.CanSeePlayer();
    }
}
