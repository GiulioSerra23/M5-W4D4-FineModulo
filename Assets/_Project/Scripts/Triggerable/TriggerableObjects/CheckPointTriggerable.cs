
using UnityEngine;

public class CheckPointTriggerable : MonoBehaviour, ITriggerable
{
    public void TriggerEnter()
    {
        CheckPointManager.Instance.SetCheckPoint(transform.position);
    }

    public void TriggerExit() { }
}
