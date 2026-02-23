using UnityEngine;

public abstract class FSM_BaseTransition : MonoBehaviour
{
    [SerializeField] private FSM_BaseState _targetState;

    protected FSM_BaseState _ownerState;
    protected FSM_Controller _controller;
    protected Component _owner;

    public FSM_BaseState TargetState => _targetState;

    public virtual void SetUp(FSM_BaseState ownerState, FSM_Controller controller, Component owner)
    {
        _ownerState = ownerState;
        _controller = controller;
        _owner = owner;
    }

    public abstract bool IsConditionMet();
}
