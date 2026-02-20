
using UnityEngine;

public class SelfRotation : MonoBehaviour
{
    public enum RotationType { Perpetual, Swing }

    [Header ("Rotation Type")]
    [SerializeField] private RotationType _rotationType;

    [Header ("Rotation Settings")]
    [SerializeField]private Vector3 _rotationAngles = Vector3.up;

    [Header ("Perpetual Settings")]    
    [SerializeField] private float _rotationSpeed = 5f;

    [Header ("Swing Settings")]
    [SerializeField] private float _swingAngle = 30f;
    [SerializeField] private float _swingSpeed = 2f;

    private void PerpetualRotation()
    {
        transform.Rotate(_rotationAngles * _rotationSpeed * Time.deltaTime);
    }

    private void SwingRotation()
    {
        float angleOffset = Mathf.Sin(Time.time * _swingSpeed) * _swingAngle;
        Vector3 currentRotation = _rotationAngles * angleOffset;
        transform.rotation = Quaternion.Euler(currentRotation);
    }

    private void Update()
    {
        switch (_rotationType)
        {
            case RotationType.Perpetual:
                PerpetualRotation();
                break;

            case RotationType.Swing:
                SwingRotation();
                break;
        }
    }
}
