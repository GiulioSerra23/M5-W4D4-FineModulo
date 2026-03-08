
using UnityEngine;
using UnityEngine.Events;

public class TriggerAction : MonoBehaviour
{
    [Header ("Sound ID")]
    [SerializeField] private SoundID _triggerEnterSound = SoundID.NONE;
    [SerializeField] private SoundID _triggerExitSound = SoundID.NONE;

    [Header ("Object ID")]
    [SerializeField] private ObjectID _requiredID = ObjectID.NONE;

    [Header ("Trigger Settings")]
    [SerializeField] private bool _canRetrigger = true;

    [Header ("Tag")]
    [SerializeField] private string _tag = Tags.Player;

    [Header ("Input Setting")]
    [SerializeField] private bool _useAnInput = false;
    [SerializeField] private string _input = Inputs.E;

    [Header ("Trigger Targets")]
    [SerializeField] protected MonoBehaviour[] _targets;

    [Header ("Events")]
    [SerializeField] protected UnityEvent _onEnter;
    [SerializeField] protected UnityEvent _onExit;

    protected Collider _other;
    protected ITriggerable[] _triggerables;
    protected bool _isInside;
    protected bool _hasActivated;    

    protected virtual void Awake()
    {
        _triggerables = new ITriggerable[_targets.Length];
        for (int i = 0; i < _targets.Length; i++)
        {
            _triggerables[i] = _targets[i] as ITriggerable;
        }
    }

    protected void Activate(Collider other)
    {
        foreach (var triggerable in _triggerables)
        {
            triggerable.TriggerEnter(other);
            AudioManager.Instance.Play(_triggerEnterSound);
        }

        _onEnter.Invoke();
    }

    protected void Update()
    {
        if (!_useAnInput) return;
        if (!_isInside) return;
        if (_hasActivated) return;

        if (Input.GetButtonDown(_input))
        {
            Activate(_other);
            _hasActivated = true;
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(_tag)) return;

        if (other.TryGetComponent<IIdentificable>(out var identificable))
        {
            if (identificable.ID != _requiredID) return;
        }

        _other = other;
        _isInside = true;        

        if (!_useAnInput)
        {
            Activate(other);
        }        
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag(_tag)) return;

        if (other.TryGetComponent<IIdentificable>(out var identificable))
        {
            if (identificable.ID != _requiredID) return;
        }

        _isInside = false;
        if (_canRetrigger) _hasActivated = false;

        foreach (var triggerable in _triggerables)
        {
            triggerable.TriggerExit(other);
            AudioManager.Instance.Play(_triggerExitSound);
        }

        _onExit.Invoke();
    }
}
