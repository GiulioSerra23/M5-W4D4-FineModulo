
using System;
using UnityEngine;

public class FSM_Controller : MonoBehaviour
{
    [SerializeField] private FSM_BaseState _initialState;

    private FSM_BaseState _currentState;
    private FSM_BaseState[] _allStates;

    public event Action<EnemyState> OnStateChanged;

    public Component Owner { get; private set; }

    private void Awake()
    {
        SetUp();
    }

    private void Start()
    {
        if (_initialState != null)
        {
            SetState(_initialState);
        }        
    }

    private void SetUp()
    {
        _allStates = GetComponentsInChildren<FSM_BaseState>();
        Owner = GetComponentInParent<BaseEnemy>(); // Ho fatto l'Owner come Component ma il GetComponent come BaseEnemy perchè se lo facevo Component mi prendeva il primo componente quindi sempre
                                                   // il Transform, ma ho lasciato la property come Component cosi da poter lasciare il tutto abbastanza generico e riutilizzabile, magari in caso 
        foreach (var state in _allStates)          // mi serva la State Machine anche per Player, NPC ecc potrei fare un BaseController e poi dei figli che fanno l'override di questa funzione
        {                                          // e cambiando il GetComponentInParent<T>() in base alla classe che deve usare la State Machine
            state.SetUp(this, Owner);
        }
    }

    public void SetState(FSM_BaseState state)
    {
        if (_currentState != null)
        {
            _currentState.OnStateExit();
        }
        
        _currentState = state;        
        _currentState.OnStateEnter();

        OnStateChanged?.Invoke(_currentState.State);
    }

    private void Update()
    {
        if (_currentState == null) return;

        foreach (var transition in _currentState.Transitions)
        {
            if (transition.IsConditionMet())
            {
                SetState(transition.TargetState);
                break;
            }
        }

        _currentState.StateUpdate();
    }
}
