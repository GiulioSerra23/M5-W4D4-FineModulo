
using UnityEngine;

public class MoveSelfTriggerable : MonoBehaviour, ITriggerable
{
    [Header ("References")]
    [SerializeField] private Transform _targetPosition;

    [Header ("Trigger Behavior")]
    [SerializeField] private TriggerBehavior _triggerBehavior = TriggerBehavior.HOLDWHILEINSIDE;

    [Header ("Movement Settings")]
    [SerializeField] private float _moveSpeed;

    private Vector3 _startPosition;
    private bool _hasToMove = false;

    private void Start()
    {
        _startPosition = transform.position;
    }

    public void TriggerEnter()
    {        
        _hasToMove = true;
    }

    public void TriggerExit() 
    {
        if (_triggerBehavior == TriggerBehavior.HOLDWHILEINSIDE)
        {
            _hasToMove = false;
        }       
    }

    public void ForceExit()
    {
        _hasToMove = false;
    }

    private void Update()
    {
        if (_hasToMove)
        {
            transform.position = Vector3.Lerp(transform.position, _targetPosition.position, _moveSpeed * Time.deltaTime);
        }
        else
        {
            transform.position = Vector3.Lerp(transform.position, _startPosition, _moveSpeed * Time.deltaTime);
        }
    }
}
