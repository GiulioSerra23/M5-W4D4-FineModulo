
using UnityEngine;

public class SetActiveSelfTriggerable : MonoBehaviour, ITriggerable
{
    [Header ("Trigger Behavior")]
    [SerializeField] private TriggerBehavior _triggerableBehavior = TriggerBehavior.TOGGLE;

    [Header ("SetActive Settings")]
    [SerializeField] private bool _setActive;

    private bool _hasSetted = false;

    public void TriggerEnter()
    {
        if (_hasSetted) return;

        gameObject.SetActive(_setActive);
        _hasSetted = true;
    }

    public void TriggerExit() 
    {
        if (_triggerableBehavior == TriggerBehavior.HOLDWHILEINSIDE)
        {
            gameObject.SetActive(!_setActive);
            _hasSetted = false;
        }
    }
}
