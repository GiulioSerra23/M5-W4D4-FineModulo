using UnityEngine;

public static class LineRendererUtility
{
    public static void DrawFieldOfView(LineRenderer lineRenderer, Transform transform, int subdivions, float viewAngle, float sightDistance, LayerMask layerMask)
    {
        lineRenderer.positionCount = subdivions + 1;

        Vector3 lineOrigin = Vector3.zero;
        lineRenderer.SetPosition(0, lineOrigin);

        float startAngle = -viewAngle;
        float deltaAngle = (2 * viewAngle / subdivions);

        Vector3 worldOrigin = transform.position;
        Vector3 forward = transform.forward;

        for (int i = 0; i < subdivions; i++)
        {
            float currentAngle = startAngle + deltaAngle * i;
            Vector3 direction = Quaternion.AngleAxis(currentAngle, transform.up) * forward;

            Vector3 worldEndPoint;

            if (Physics.Raycast(worldOrigin, direction, out RaycastHit hit, sightDistance, layerMask))
            {
                worldEndPoint = hit.point;
            }
            else
            {
                worldEndPoint = worldOrigin + direction * sightDistance;
            }

            Vector3 localPoint = transform.InverseTransformPoint(worldEndPoint);

            lineRenderer.SetPosition(i + 1, localPoint);
        }
    }

    public static void DrawParabola(LineRenderer lineRenderer, int subdivisions, float height, Vector3 start, Vector3 end)
    {
        Vector3 direction = end - start;
        float distance = direction.magnitude;
        Vector3 flatDir = direction.normalized;

        for (int i = 0; i < subdivisions; i++)
        {
            float time = i / (float)(subdivisions - 1);

            Vector3 position = start + flatDir * distance * time;
            float parabola = 4f * height * time * (1 - time);
            position.y += parabola;

            lineRenderer.SetPosition(i, position);
        }
    }
}
