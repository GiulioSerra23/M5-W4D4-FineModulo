using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] protected TargetDetection _detection;

    [Header ("Head Settings")]
    [SerializeField] private Transform _head;
    [SerializeField] private float _headReturnSpeed = 360f;

    [Header ("Alert Settings")]
    [SerializeField] private bool _canAlert = true;
    [SerializeField] private float _alertRadius = 8f;
    [SerializeField] private int _maxAlliesToAlert = 5;
    [SerializeField] private LayerMask _enemyLayer;

    [Header ("Movement Settings")]
    [SerializeField] protected float _reachDistance = 0.3f;

    [Header("Grabbed Settings (Optional)")]
    [SerializeField] private Rigidbody _grabbedRb;

    [Header("Debug")]
    [SerializeField] private bool _showAlertSphere = true;

    private DangerZone _dangerZone;
    private AnimationParamHandler _animHandler;    
    private Collider[] _allies;
    protected NavMeshAgent _agent;

    protected float _baseSpeed;
    protected float _headCurrentOffset;
    protected float _headTargetOffset;

    public DangerZone DangerZone => _dangerZone;
    public TargetDetection Detection => _detection;
    public NavMeshAgent Agent => _agent;
    public bool IsAlerted { get; set; }
    public bool CanBeAlerted { get; set; } = true;
    public bool IsStunned { get; set; }
    public float StunDuration { get; private set; }
    public float ReachDistance => _reachDistance;

    private void Awake()
    {
        _animHandler = GetComponent<AnimationParamHandler>();
        _agent = GetComponent<NavMeshAgent>();
        _dangerZone = GetComponent<DangerZone>();
        _allies = new Collider[_maxAlliesToAlert];
    }

    protected virtual void Start()
    {
        _baseSpeed = _agent.speed;
    }

    public void SetSpeed(float speed)
    {
        _agent.speed = speed;
    }

    public void ResetSpeed()
    {
        _agent.speed = _baseSpeed;
    }

    public void SetHeadOffset(float offset)
    {
        _headTargetOffset = offset;
    }

    private void RotateHead()
    {
        if (_headCurrentOffset != _headTargetOffset)
        {
            _headCurrentOffset = Mathf.MoveTowards(_headCurrentOffset, _headTargetOffset, _headReturnSpeed * Time.deltaTime);
            _head.localRotation = Quaternion.Euler(0f, _headCurrentOffset, 0f);
        }        
    }

    public void RotateToTarget()
    {
        Vector3 direction = (_detection.Target.position - transform.position).normalized;
        direction.y = 0f;

        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, _agent.angularSpeed * Time.deltaTime);
    }

    public void AlertAllies(Vector3 position)
    {
        if (!_canAlert) return;

        int count = Physics.OverlapSphereNonAlloc(transform.position, _alertRadius, _allies, _enemyLayer);

        for (int i = 0; i < count; i ++)
        {
            Collider ally = _allies[i];

            if (ally.TryGetComponent<BaseEnemy>(out var enemy) && enemy != this)
            {
                enemy.ReceiveAlert(position);
            } 
        }
    }

    public void ReceiveAlert(Vector3 position)
    {
        if (!CanBeAlerted) return;

        _agent.SetDestination(position);
        IsAlerted = true;
    }

    public void ApplyStun(float stunDuration)
    {
        if (IsStunned) return;
        
        IsStunned = true;
        StunDuration = stunDuration;
    }

    public void SetGrabbedState(bool grabbed)
    {
        _dangerZone.CanDoDamage = !grabbed;
        _grabbedRb.isKinematic = !grabbed;
        Agent.enabled = !grabbed;
        enabled = !grabbed;
    }

    public abstract void HandlePatrol();

    private void Update()
    {
        _animHandler.SetForward(_agent.velocity.magnitude);
        RotateHead();
    }

    private void OnDrawGizmos()
    {
        if (!_showAlertSphere) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _alertRadius);
    }
}
