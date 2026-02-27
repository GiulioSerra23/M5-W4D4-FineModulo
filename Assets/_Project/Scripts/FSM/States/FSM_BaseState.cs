using System;
using UnityEngine;

public enum EnemyState
{
    IDLE,
    PATROL,
    CHASE,
    SEARCHING,
    STUNNED,

    NONE = 30
}

public abstract class FSM_BaseState : MonoBehaviour
{
    protected FSM_Controller _controller;
    protected FSM_BaseTransition[] _transition;
    protected Component _owner;

    public abstract EnemyState State { get;}
    public FSM_BaseTransition[] Transitions => _transition;

    public virtual void SetUp(FSM_Controller controller, Component owner) // Ho lasciato owner come Component anche qui per lo stesso motivo del controller, per non rendere la classe strettamente
    {                                                                     // legata ad un tipo, e quindi cosi posso gestire chi deve reagire ad un certo stato direttamente nello stato stesso,
        _controller = controller;                                         // quindi se io avessi la state machine anche sul player per esempio potrei fare un figlio del controller che prende il player
        _owner = owner;                                                   // il baseState e baseTransition rimarrebbero uguali e basterebbe fare altri stati diversi per il player che quindi faranno
                                                                          // l'override di SetUp ed il cast di owner in Player invece di BaseEnemy
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
