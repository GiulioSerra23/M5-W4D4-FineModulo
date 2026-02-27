using System.Collections;
using UnityEngine;

public class TargetDetection : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private Transform _head;
    [SerializeField] private Transform _target;

    [Header ("Vision Settings")]
    [SerializeField] private int _subdivisions = 12;
    [SerializeField] private float _interval = 0.3f;
    [SerializeField] private float _baseViewAngle = 45f;
    [SerializeField] private float _baseSightDistance = 45f;

    [Header ("Obstacles")]
    [SerializeField] private LayerMask _whatIsObstacle;

    [Header ("Show")]
    [SerializeField] private bool _showFov = true;
    [SerializeField] private bool _showDebugLine = true;

    private float _currentViewAngle;
    private float _currentSightDistance;
    private LineRenderer _lineRenderer;

    public Transform Target => _target;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
    }

    private void Start()
    {
        SetVision(1f, 1f);

        if (_showFov)
        {
            StartCoroutine(DrawFieldOfView());
        }        
    }

    public void SetShowFov(bool showFov)
    {
        _showFov = showFov;
        _lineRenderer.enabled = showFov;
    }

    private IEnumerator DrawFieldOfView()
    {
        while (true)
        {
            yield return new WaitForSeconds(_interval);

            if (_showFov)
            {                
                LineRendererUtility.DrawFieldOfView(_lineRenderer, transform, _subdivisions, _currentViewAngle, _currentSightDistance, _whatIsObstacle);
            }            
        }
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
