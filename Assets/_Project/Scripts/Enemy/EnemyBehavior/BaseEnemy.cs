using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public abstract class BaseEnemy : MonoBehaviour
{
    [SerializeField] protected TargetDetection _detection;

    protected NavMeshAgent _agent;

    public TargetDetection Detection => _detection;
    public NavMeshAgent Agent => _agent;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
    }

    public virtual void HandleChase(Transform target)
    {
        _agent.isStopped = false;
        _agent.SetDestination(target.position);
    }

    public abstract void HandlePatrol();    
}
