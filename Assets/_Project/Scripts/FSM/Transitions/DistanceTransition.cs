using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceTransition : FSM_BaseTransition
{
    public enum Comparison { GREATER_THAN, LESS_THAN }

    [SerializeField] private Comparison _comparison = Comparison.GREATER_THAN;
    [SerializeField] private float _distanceTreshold = 10f;

    private BaseEnemy _enemy;

    public override void SetUp(FSM_BaseState ownerState, FSM_Controller controller, Component owner)
    {
        base.SetUp(ownerState, controller, owner);
        _enemy = owner as BaseEnemy;
    }

    public override bool IsConditionMet()
    {
        Vector3 targetPos = _enemy.Detection.Target.position;
        Vector3 ownerPos = _enemy.transform.position;

        float sqrDistance = Vector3.SqrMagnitude(targetPos - ownerPos);
        float sqrTreshold = _distanceTreshold * _distanceTreshold;

        switch (_comparison)
        {
            case Comparison.GREATER_THAN:
                return sqrDistance > sqrTreshold;
            case Comparison.LESS_THAN:
                return sqrDistance < sqrTreshold;
        }
        return false;
    }
}
