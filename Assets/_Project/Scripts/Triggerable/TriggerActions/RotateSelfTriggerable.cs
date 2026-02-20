
using UnityEngine;

public class RotateSelfTriggerable : MonoBehaviour, ITriggerable
{
    [Header ("Settings")]
    [SerializeField] private bool _canRetrigger = false;

    [Header("Rotation Settings")]
    [SerializeField] private Vector3 _rotationAngles;
    [SerializeField] private float _rotationSpeed;

    private bool _isRotating = false;
    private bool _hasRotated = false;

    private Quaternion _targetRotation;
    private Quaternion _startRotation;

    private void Start()
    {
        _startRotation = transform.localRotation;
    }

    public void TriggerEnter()
    {
        if (!_canRetrigger && _hasRotated) return;

        _targetRotation = Quaternion.Euler(_rotationAngles);
        _isRotating = true;
        _hasRotated = true;
    }

    public void TriggerExit() { }

    public void RotateBack()
    {
        if (!_canRetrigger) return;

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
