using System;
using System.Collections.Generic;
using UnityEngine;

public class StateAnimationController : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private FSM_Controller _controller;
    [SerializeField] private AnimationParamHandler _animHandler;

    [Header ("States")]
    [SerializeField] private SearchingState _searchingState;

    private Dictionary<State, Action> _stateAnimations;

    private void Awake()
    {
        _stateAnimations = new Dictionary<State, Action>
        { 
            { State.CHASE, HandleChase },
            { State.STUNNED, HandleStunned },
        };
    }

    private void OnEnable()
    {
        _controller.OnStateChanged += HandleStateChanged;
        _searchingState.OnStartLookingAround += HandleSearching;
    }

    private void ResetAllAnimations()
    {
        _animHandler.SetIsChasing(false);
        _animHandler.SetIsSearching(false);
    }

    public void HandleStateChanged(State state)
    {
        if (_stateAnimations.TryGetValue(state, out var animAction))
        {
            animAction.Invoke();
        }
        else
        {
            ResetAllAnimations();
        }
    }

    private void HandleChase()
    {
        ResetAllAnimations();
        _animHandler.SetIsChasing(true);
    }

    private void HandleSearching()
    {
        ResetAllAnimations();
        _animHandler.SetIsSearching(true);
    }

    private void HandleStunned()
    {
        ResetAllAnimations();
    }

    private void OnDisable()
    {
        _controller.OnStateChanged -= HandleStateChanged;
        _searchingState.OnStartLookingAround -= HandleSearching;
    }
}
