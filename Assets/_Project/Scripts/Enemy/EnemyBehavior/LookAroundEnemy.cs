using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAroundEnemy : BaseEnemy
{
    [Header("Patrol Settings")]
    [SerializeField] private float _rotationSpeed = 30f;

    public override void HandlePatrol()
    {
        transform.Rotate(Vector3.up, _rotationSpeed * Time.deltaTime);
    }
}
