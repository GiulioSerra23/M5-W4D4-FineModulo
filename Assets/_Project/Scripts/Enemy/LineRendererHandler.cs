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

        float viewAngle = _targetDetection.ViewAngle;       
        float sightDistance = _targetDetection.SightDistance;

        Vector3 lineOrigin = Vector3.zero;
        _lineRenderer.SetPosition(0, lineOrigin);

        float startAngle = -viewAngle;
        float deltaAngle = (2 * viewAngle / subdivions);

        Vector3 worldOrigin = transform.position;
        Vector3 forward = transform.forward;

        for (int i = 0; i < subdivions; i++)
        {
            float currentAngle = startAngle + deltaAngle * i;
            Vector3 direction = Quaternion.AngleAxis(currentAngle, transform.up) * forward;

            Vector3 worldEndPoint;

            if (Physics.Raycast(worldOrigin, direction, out RaycastHit hit, sightDistance, _whatIsObstacle))
            {
                worldEndPoint = hit.point;
            }
            else
            {
                worldEndPoint = worldOrigin + direction * sightDistance;
            }

            Vector3 localPoint = transform.InverseTransformPoint(worldEndPoint);

            _lineRenderer.SetPosition(i + 1, localPoint);
        }
    } 
}
