using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ItemMatchingSlotTriggerable : MonoBehaviour, ITriggerable
{
    [Header ("Events")]
    [SerializeField] private UnityEvent _onSlotCorrect;

    [Header ("Snap")]
    [SerializeField] private Transform _snapPoint;
    [SerializeField] private bool _lockOnCorrect = true;
    [SerializeField] private bool _deactivateRigidbody = true;

    private bool _isCorrect;

    public event Action OnSlotStateChanged;
    public bool IsCorrect => _isCorrect;

    private void SnapObject(Collider other)
    {
        if (_deactivateRigidbody) DeactivateRigidbody(other);

        other.transform.position = _snapPoint.position;
        other.transform.rotation = _snapPoint.rotation;
    }

    private void DeactivateRigidbody(Collider other)
    {
        Rigidbody[] rbs = other.GetComponentsInChildren<Rigidbody>();

        if (rbs != null && rbs.Length > 0)
        {
            foreach (Rigidbody rb in rbs)
            {
                rb.isKinematic = true;
            }
        }
    }

    public void TriggerEnter(Collider other)
    {
        if (_isCorrect) return;

        _isCorrect = true;
        
        if (_lockOnCorrect) SnapObject(other);

        _onSlotCorrect.Invoke();
        OnSlotStateChanged?.Invoke();
    }

    public void TriggerExit(Collider other) { }
}
