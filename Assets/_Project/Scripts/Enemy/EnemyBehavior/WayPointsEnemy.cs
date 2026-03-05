using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class WayPointsEnemy : BaseEnemy
{
    [Header ("Path")]
    [SerializeField] private Transform[] _wayPoints;

    [Header("Patrol Settings")]
    [SerializeField] private float _waitTime = 2f;

    private int _currentIndex = 0;
    private bool _isWaiting = false;
    private WaitForSeconds _wfs;

    protected override void Start()
    {
        base.Start();
        _wfs = new WaitForSeconds(_waitTime);
    }

    public override void HandlePatrol()
    {
        if (_wayPoints.Length == 0 || _isWaiting) return;

        if (_agent.remainingDistance <= _reachDistance)
        {
            StartCoroutine(WaitAndAdvance());
        }
        else
        {
            _agent.isStopped = false;
        }
    }

    private void UpdateIndex()
    {
        _currentIndex = (_currentIndex + 1) % _wayPoints.Length;
    }

    private void GoToWayPoint()
    {
        _agent.SetDestination(_wayPoints[_currentIndex].position);
    }

    private IEnumerator WaitAndAdvance()
    {
        _isWaiting = true;
        _agent.isStopped = true;

        yield return _wfs;

        _agent.isStopped = false;
        _isWaiting = false;

        if (!IsStunned)
        {           
            UpdateIndex();
            GoToWayPoint();
        }       
    }
}
