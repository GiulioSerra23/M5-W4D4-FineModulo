using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, ITriggerable
{
    [SerializeField] private SO_GenericItem _item;

    public void TriggerEnter()
    {
        InventoryManager.Instance.AddItem(_item);
        Destroy(gameObject);
    }

    public void TriggerExit() { }
}
