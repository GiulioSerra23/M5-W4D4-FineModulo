
using UnityEngine;

public class CheckPointTriggerable : MonoBehaviour, ITriggerable
{
    [Header("References")]
    [SerializeField] private CheckPointManager _checkPointManager;

    public void TriggerEnter()
    {
        _checkPointManager.SetCheckPoint(transform.position);
    }

    public void TriggerExit() { }
}
