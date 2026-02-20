
using UnityEngine;

public class SelfPerpetualRise : MonoBehaviour
{
    [Header ("Rise Settings")]
    [SerializeField] private float _altitude = 1f;
    [SerializeField] private float _altitudeSpeed = 2f;

    private Vector3 _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Rise()
    {
        float offSetY = Mathf.Sin(Time.time * _altitudeSpeed) * _altitude;
        transform.position = _startPosition + Vector3.up * offSetY;
    }

    private void Update()
    {
        Rise();
    }
}
