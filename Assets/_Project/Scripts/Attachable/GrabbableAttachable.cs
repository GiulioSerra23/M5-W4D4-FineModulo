using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableAttachable : MonoBehaviour, IAttachable, IIdentificable
{
    [Header("Attach Settings")]
    [SerializeField] private Transform _attachPoint;
    [SerializeField] private ObjectID _id;

    private FixedJoint _joint;

    public bool RequiresInputToAttach => true;

    public ObjectID ID { get => _id; set => _id = value; }

    public void OnAttach(Component attachableHandler)
    {
        _joint = gameObject.AddComponent<FixedJoint>();
        _joint.connectedBody = _attachPoint.GetComponentInParent<Rigidbody>();
        _joint.breakForce = Mathf.Infinity;
        _joint.breakTorque = Mathf.Infinity;
    }

    public void HandleAttachedInput(Component attachableHandler) { }   

    public void OnDetach(Component attachableHandler, bool isForced)
    {
        if (_joint != null)
        {
            Destroy(_joint);
            _joint = null;
        }
    }
}
