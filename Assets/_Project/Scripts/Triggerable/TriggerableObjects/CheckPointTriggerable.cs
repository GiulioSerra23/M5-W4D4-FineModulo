
using UnityEngine;

public class CheckPointTriggerable : MonoBehaviour, ITriggerable
{
    public void TriggerEnter(Collider other)
    {
        CheckPointManager.Instance.SetCheckPoint(transform.position);
    }

    public void TriggerExit(Collider other) { }
}
