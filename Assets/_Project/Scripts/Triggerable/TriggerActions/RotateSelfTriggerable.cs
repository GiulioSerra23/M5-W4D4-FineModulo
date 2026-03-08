
using UnityEngine;

public class RotateSelfTriggerable : MonoBehaviour, ITriggerable
{
    [Header("Rotation Settings")]
    [SerializeField] private Vector3 _rotationAngles;
    [SerializeField] private float _rotationSpeed;

    private bool _isRotating = false;

    private Quaternion _targetRotation;
    private Quaternion _startRotation;

    private void Start()
    {
        _startRotation = transform.localRotation;
    }

    public void TriggerEnter(Collider other)
    {
        Quaternion rotationOffset = Quaternion.Euler(_rotationAngles);
        _targetRotation = transform.localRotation * rotationOffset;

        _isRotating = true;
    }

    public void TriggerExit(Collider other) { }

    public void RotateBack()
    {
        _targetRotation = _startRotation;
        _isRotating = true;
    }

    private void Rotation()
    {
        if (!_isRotating) return;

        transform.localRotation = Quaternion.Lerp(transform.localRotation, _targetRotation, _rotationSpeed * Time.deltaTime);

        if (Quaternion.Angle(transform.localRotation, _targetRotation) < 0.1f )
        {
            transform.localRotation = _targetRotation;
            _isRotating = false;
        }
    }

    private void Update()
    {
        Rotation();
    }
}
