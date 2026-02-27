using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTargetIndicator : MonoBehaviour
{
    public static MouseTargetIndicator Instance { get; private set; }

    [Header("Line Settings")]
    [SerializeField] private Transform _origin;
    [SerializeField] private int _linePoints = 30;
    [SerializeField] private float _arcHeight = 2f;
    [SerializeField] private bool _showLine = true;

    private Camera _camera;
    private LineRenderer _lineRenderer;
    private Vector3 _currentTarget;

    public Vector3 CurrentTarget => _currentTarget;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.positionCount = _linePoints;
        _lineRenderer.enabled = _showLine;

        _camera = Camera.main;
    }

    private Vector3 GetMouseWorldPosition()
    {
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);

        if (groundPlane.Raycast(ray, out float enter))
        {
            return ray.GetPoint(enter);
        }

        return _origin.position;
    }

    private void Update()
    {
        if (_origin == null) return;

        _currentTarget = GetMouseWorldPosition();
        LineRendererUtility.DrawParabola(_lineRenderer, _linePoints, _arcHeight, _origin.position, _currentTarget);
    }
}
