using UnityEngine;
using UnityEngine.AI;
using System;

public abstract class BaseEnemy : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] protected TargetDetection _detection;

    [Header ("Alert Settings")]
    [SerializeField] private float _alertRadius = 8f;
    [SerializeField] private int _maxAlliesToAlert = 5;
    [SerializeField] private LayerMask _enemyLayer;

    [Header("Debug")]
    [SerializeField] private bool _showAlertSphere = true;

    protected NavMeshAgent _agent;
    protected Collider[] _allies;
    protected Vector3 _lastKnownPosition;

    public TargetDetection Detection => _detection;
    public NavMeshAgent Agent => _agent;
    public bool IsAlerted { get; set; }
    public bool CanBeAlerted { get; set; } = true;
    public bool IsStunned { get; set; }
    public float StunDuration { get; private set; }

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _allies = new Collider[_maxAlliesToAlert];
    }

    public void AlertAllies(Vector3 position)
    {
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

    public abstract void HandlePatrol();

    private void OnDrawGizmos()
    {
        if (!_showAlertSphere) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _alertRadius);
    }
}
