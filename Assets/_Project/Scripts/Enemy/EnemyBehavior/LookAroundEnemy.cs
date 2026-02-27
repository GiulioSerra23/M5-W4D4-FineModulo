using System.Collections;
using System.Collections.Generic;
using UnityEditor.Tilemaps;
using UnityEngine;

public class LookAroundEnemy : BaseEnemy
{
    [Header("Patrol Settings")]
    [SerializeField] private float _rotationSpeed = 30f;

    protected override void Start()
    {
        base.Start();
        _agent.angularSpeed = _rotationSpeed;
    }

    public override void HandlePatrol()
    {
        transform.Rotate(Vector3.up, _agent.angularSpeed * Time.deltaTime);
    }
}
