using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabHandler : MonoBehaviour
{
    [Header("Grab Settings")]
    [SerializeField] private string _grabButton = Inputs.E;
    [SerializeField] private string _dropButton = Inputs.E;

    private IAttachable _currentAttachable;
    private IAttachable _pendingAttachable;

    public IAttachable CurrentAttachable => _currentAttachable;
    public bool IsAttached => _currentAttachable != null;

    private void Attach(IAttachable attachable)
    {
        _currentAttachable = attachable;
        attachable.OnAttach(this);
        _pendingAttachable = null;
    }

    private void Detach(bool isForced)
    {
        _currentAttachable.OnDetach(this, isForced);
        _currentAttachable = null;
    }

    public void ForceDetach()
    {
        if (!IsAttached) return;

        Detach(true);
    }

    private void Update()
    {
        if (!IsAttached && _pendingAttachable != null)
        {
            if (Input.GetButtonDown(_grabButton))
            {
                Attach(_pendingAttachable);
            }
        }

        if (!IsAttached) return;

        _currentAttachable.HandleAttachedInput(this);

        if (Input.GetButtonDown(_dropButton))
        {
            Detach(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (IsAttached) return;

        if (!other.TryGetComponent<IAttachable>(out var attachable)) return;

        if (attachable.RequiresInputToAttach)
        {
            _pendingAttachable = attachable;
        }
        else
        {
            Attach(attachable);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.TryGetComponent<IAttachable>(out var attachable)) return;

        if (_pendingAttachable == attachable)
        {
            _pendingAttachable = null;
        }
    }
}
