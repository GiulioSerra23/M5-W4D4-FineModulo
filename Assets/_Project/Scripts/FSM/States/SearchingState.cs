using System;
using System.Collections;
using UnityEngine;

public class SearchingState : FSM_BaseState
{
    [Header ("Search Settings")]
    [SerializeField] private float _searchDuration = 3f;
    [SerializeField] private float _pauseDuration = 0.5f;
    [SerializeField] private float _lookAngle = 60f;

    private BaseEnemy _enemy;

    private float _searchTimer;
    private bool _reachedPoint;

    public event Action OnStartLookingAround;

    public override State State => State.SEARCHING;
    public bool HasFinished => _reachedPoint && _searchTimer >= _searchDuration;

    public override void SetUp(FSM_Controller controller, Component owner)
    {
        base.SetUp(controller, owner);
        _enemy = owner as BaseEnemy;
    }

    private void UpdateSearchRotation()
    {
        _searchTimer += Time.deltaTime;
        float time = Mathf.Clamp01(_searchTimer / _searchDuration);

        float pauseFraction = _pauseDuration / _searchDuration;
        float activeStart = pauseFraction;
        float activeEnd = 1f - pauseFraction;

        float angleOffset = 0f;

        if (time < activeStart)
        {
            angleOffset = 0f;
        }
        else if (time > activeEnd)
        {
            angleOffset = 0f;
        }
        else
        {
            float activeT = (time - activeStart) / (activeEnd - activeStart);
            angleOffset = Mathf.Sin(activeT * Mathf.PI * 2f) * _lookAngle;
        }

        _enemy.SetHeadOffset(angleOffset);
    }

    public override void OnStateEnter()
    {
        _searchTimer = 0f;
        _reachedPoint = false;

        _enemy.Agent.isStopped = false;
        _enemy.CanBeAlerted = false;
        _enemy.IsAlerted = false;

        _enemy.Detection.SetVision(1.3f, 1.2f);
    }

    public override void StateUpdate()
    {
        if (!_reachedPoint)
        {
            if (!_enemy.Agent.pathPending && _enemy.Agent.remainingDistance <= _enemy.ReachDistance)
            {
                _reachedPoint = true;
                _enemy.Agent.isStopped = true;
                OnStartLookingAround?.Invoke();
            }
        }
        else
        {
            UpdateSearchRotation();
        }
    }

    public override void OnStateExit()
    {
        _enemy.SetHeadOffset(0f);

        _enemy.Detection.ResetVision();
        _enemy.CanBeAlerted = true;
    }
}
