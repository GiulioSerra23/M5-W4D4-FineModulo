using UnityEngine;

public class GrabbedTransition : FSM_BaseTransition
{
    [SerializeField] private GrabHandler _grabber;

    private BaseEnemy _enemy;
    private IAttachable _grab;

    public override void SetUp(FSM_BaseState ownerState, FSM_Controller controller, Component owner)
    {
        base.SetUp(ownerState, controller, owner);
        _enemy = owner as BaseEnemy;
        _grab = _enemy.gameObject.GetComponent<IAttachable>();
    }

    public override bool IsConditionMet()
    {
        if (_grabber.IsAttached && _grabber.CurrentAttachable == _grab) return true;
        return false;
    }
}
