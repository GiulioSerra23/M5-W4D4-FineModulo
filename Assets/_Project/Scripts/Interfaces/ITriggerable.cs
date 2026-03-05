

using UnityEngine;

public interface ITriggerable
{
    public void TriggerEnter(Collider other);
    public void TriggerExit(Collider other);
}
