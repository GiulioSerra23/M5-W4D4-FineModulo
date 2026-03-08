using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabbableAttachable : MonoBehaviour, IAttachable, IIdentificable
{
    [Header ("Attach Settings")]
    [SerializeField] private Transform _attachPoint;
    [SerializeField] private ObjectID _id;

    [Header("RigidBody Settings")]
    [SerializeField] private bool _setRbSettings = false;
    [SerializeField] private float _mass;
    [SerializeField] private float _drag;
    [SerializeField] private float _angularDrag;

    private Rigidbody _rb;
    private FixedJoint _joint;

    public bool RequiresInputToAttach => true;

    public ObjectID ID { get => _id; set => _id = value; }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    public void OnAttach(Component attachableHandler)
    {
        if (_setRbSettings)
        {
            _rb.constraints = RigidbodyConstraints.FreezePositionY;
            _rb.drag = _drag;
            _rb.mass = _mass;
            _rb.angularDrag = _angularDrag;
        }
        
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
