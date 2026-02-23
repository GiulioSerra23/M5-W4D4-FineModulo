using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _target;

    [Header ("Vision Settings")]
    [SerializeField] private float _baseViewAngle = 45f;
    [SerializeField] private float _baseSightDistance = 45f;

    [Header ("Obstacles")]
    [SerializeField] private LayerMask _whatIsObstacle;

    [Header ("Debug")]
    [SerializeField] private bool _showDebugLine = true;

    private float _currentViewAngle;
    private float _currentSightDistance;

    public Transform Target => _target;
    public float ViewAngle => _currentViewAngle;
    public float SightDistance => _currentSightDistance;

    private void Awake()
    {
        SetVision(1f, 1f);
    }

    public bool CanSeeTarget()
    {
        if (Target == null) return false;

        Vector3 toTarget = _target.position - _head.position;
        float distance = toTarget.magnitude;

        if (distance > _currentSightDistance) return false;

        toTarget.Normalize();

        if (Vector3.Dot(_head.forward, toTarget) < Mathf.Cos(_currentViewAngle * Mathf.Deg2Rad)) return false;
        
        if (Physics.Linecast(_head.position, _target.position, _whatIsObstacle)) return false;

        return true;
    }

    public void SetVision(float angleMultiplier, float distanceMultiplier)
    {
        _currentViewAngle = _baseViewAngle * angleMultiplier;
        _currentSightDistance = _baseSightDistance * distanceMultiplier;
    }

    public void ResetVision()
    {
        _currentViewAngle = _baseViewAngle;
        _currentSightDistance = _baseSightDistance;
    }

    private void OnDrawGizmos() 
    {
        if (!_showDebugLine) return;

        Debug.DrawLine(_head.position, _target.position, Color.red);
    }
}
