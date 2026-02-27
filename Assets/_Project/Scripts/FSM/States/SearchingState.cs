using System.Collections;
using UnityEngine;

public class SearchingState : FSM_BaseState
{
    [Header ("Search Settings")]
    [SerializeField] private float _searchDuration = 3f;
    [SerializeField] private float _pauseDuration = 0.5f;
    [SerializeField] private float _rotationSpeed = 120f;
    [SerializeField] private float _lookAngle = 60f;

    private float _pauseTimer;
    private float _timer;
    private bool _reachedPoint;
    private float _currentAngle;
    private int _direction = 1;

    private BaseEnemy _enemy;

    public override EnemyState State => EnemyState.SEARCHING;

    public bool HasFinished => _reachedPoint && _timer >= _searchDuration;

    public override void SetUp(FSM_Controller controller, Component owner)
    {
        base.SetUp(controller, owner);
        _enemy = owner as BaseEnemy;
    }

    private void RotateDuringSearch()
    {
        if (_pauseTimer > 0)
        {
            _pauseTimer -= Time.deltaTime;
            return;
        }

        _timer += Time.deltaTime;
        float rotatioStep = _rotationSpeed * Time.deltaTime * _direction;
        _enemy.transform.Rotate(Vector3.up, rotatioStep);
        _currentAngle += rotatioStep;

        if (Mathf.Abs(_currentAngle) >= _lookAngle)
        {
            _direction *= -1;
            _currentAngle = Mathf.Sign(_currentAngle) * _lookAngle;
            _pauseTimer = _pauseDuration;
        }
    }

    public override void OnStateEnter()
    {
        _timer = 0f;
        _reachedPoint = false;
        _currentAngle = 0f;
        _direction = 1;

        _enemy.Agent.isStopped = false;

        _enemy.CanBeAlerted = false;
        _enemy.IsAlerted = false;

        _enemy.Detection.SetVision(1.3f, 1.2f);
    }

    public override void StateUpdate()
    {
        if (!_reachedPoint)
        {
            if (!_enemy.Agent.pathPending && _enemy.Agent.remainingDistance <= 0.3f)
            {
                _reachedPoint = true;
                _enemy.Agent.isStopped = true;
            }
        }
        else
        {
            RotateDuringSearch();
        }
    }

    public override void OnStateExit()
    {
        _enemy.Detection.ResetVision();
        _enemy.CanBeAlerted = true;
    }    
}
