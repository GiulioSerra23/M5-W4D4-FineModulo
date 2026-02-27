using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SO_GranadeEffect : ScriptableObject
{
    [Header ("Overlap Sphere Settings")]
    [SerializeField] protected float _radius = 4f;
    [SerializeField] protected int _maxEntityEffected = 3;
    [SerializeField] protected LayerMask _layerMask;

    public float Radius => _radius;

    public abstract void Apply(GameObject user, Vector3 position);
}
