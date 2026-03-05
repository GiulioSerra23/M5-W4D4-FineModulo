using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbedState : FSM_BaseState
{
    private BaseEnemy _enemy;
    private Rigidbody _rb;
    private DangerZone _zone;
    private Collider _grabbedCollider;

    public override State State => State.GRABBED;

    public override void SetUp(FSM_Controller controller, Component owner)
    {
        base.SetUp(controller, owner);
        _enemy = owner as BaseEnemy;
        _rb = _enemy.gameObject.GetComponent<Rigidbody>();
        _zone = _enemy.gameObject.GetComponent<DangerZone>();
        _grabbedCollider = _enemy.gameObject.GetComponent<Collider>();
    }

    public override void OnStateEnter()
    {
        _grabbedCollider.enabled = true;
        _zone.CanDoDamage = false;
        _rb.isKinematic = false;
        _enemy.Agent.enabled = false;
        _enemy.enabled = false;
    }

    public override void StateUpdate() { }  

    public override void OnStateExit()
    {
        _grabbedCollider.enabled = false;
        _zone.CanDoDamage = true;
        _rb.isKinematic = true;
        _enemy.Agent.enabled = true;
        _enemy.enabled = true;
    }    
}
