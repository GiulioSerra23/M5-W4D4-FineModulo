using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public abstract class FSM_BaseState : MonoBehaviour
{
    protected FSM_Controller _controller;
    protected FSM_BaseTransition[] _transition;
    protected Component _owner;

    public FSM_BaseTransition[] Transitions => _transition;

    public virtual void SetUp(FSM_Controller controller, Component owner)
    {
        _controller = controller;
        _owner = owner;

        _transition = GetComponents<FSM_BaseTransition>();

        foreach (var transition in _transition)
        {
            transition.SetUp(this, _controller, _owner);
        }
    }

    public abstract void OnStateEnter();
    public abstract void StateUpdate();
    public abstract void OnStateExit();
}
