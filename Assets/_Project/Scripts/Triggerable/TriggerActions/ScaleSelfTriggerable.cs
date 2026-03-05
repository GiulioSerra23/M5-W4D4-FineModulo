using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleSelfTriggerable : MonoBehaviour, ITriggerable
{
    [Header ("Trigger Behavior")]
    [SerializeField] private TriggerBehavior _triggerableBehavior = TriggerBehavior.TOGGLE;

    [Header ("Scale Settings")]
    [SerializeField] private float _scaleMultiplier = 0.5f;

    private Vector3 _startScale;
    private bool _hasScaled = false;

    public void TriggerEnter(Collider other)
    {
        if (_hasScaled) return;
        
        _startScale = transform.localScale;
        transform.localScale *= _scaleMultiplier;

        _hasScaled = true;
    }

    public void TriggerExit(Collider other)
    {
        if (_triggerableBehavior == TriggerBehavior.HOLDWHILEINSIDE)
        {
            transform.localScale = _startScale;
            _hasScaled = false;
        }        
    }
}
