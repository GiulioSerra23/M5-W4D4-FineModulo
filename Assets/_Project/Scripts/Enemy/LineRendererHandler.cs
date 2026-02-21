using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class LineRendererHandler : MonoBehaviour
{
    [Header ("Settings")]
    [SerializeField] private int _subdivisions = 12;
    [SerializeField] private float _interval = 0.5f;
    [SerializeField] private LayerMask _whatIsObstacle;

    private LineRenderer _lineRenderer;
    private TargetDetection _targetDetection;

    private void Awake()
    {
        _lineRenderer = GetComponentInChildren<LineRenderer>();
        _targetDetection = GetComponent<TargetDetection>();
    }

    private void Start()
    {
        StartCoroutine(CustomUpdate());
    }

    private IEnumerator CustomUpdate()
    {
        while (true)
        {
            yield return new WaitForSeconds(_interval);
            EvaluateConeOfViewWithQuaternion(_subdivisions);
        }
    }

    private void EvaluateConeOfViewWithQuaternion(int subdivions)
    {
        _lineRenderer.positionCount = subdivions + 1;

        float startAngle = -_targetDetection.ViewAngle;

        Vector3 lineOrigin = Vector3.zero;
        Vector3 raycastOrigin = transform.position + new Vector3 (0f, 0.05f, 0f);
        Vector3 forward = Vector3.forward;

        _lineRenderer.SetPosition(0, lineOrigin);

        float deltaAngle = (2 * _targetDetection.ViewAngle / subdivions);

        for (int i = 0; i < subdivions; i++)
        {
            float currentAngle = startAngle + deltaAngle * i;
            Vector3 direction = Quaternion.Euler(0f, currentAngle, 0f) * forward;
            Vector3 point = lineOrigin + direction * _targetDetection.SightDistance;

            if (Physics.Raycast(raycastOrigin, direction, out RaycastHit hit, _targetDetection.SightDistance, _whatIsObstacle))
            {
                point = hit.point - (raycastOrigin - lineOrigin);
            }

            _lineRenderer.SetPosition(i + 1, point);
        }
    } 
}
