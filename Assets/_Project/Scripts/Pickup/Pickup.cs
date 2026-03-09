using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour, ITriggerable
{
    public enum PickupBehavior { ONE, MULTIPLE };

    [Header ("Pickup Behavior")]
    [SerializeField] private PickupBehavior _behavior;

    [Header ("One Pikcup Settings")]
    [SerializeField] private SO_GenericItem _item;

    [Header ("Multiple Pikcup Settings")]
    [SerializeField] private int _quantity;

    public void TriggerEnter(Collider other)
    {
        switch (_behavior)
        {
            case PickupBehavior.ONE:
                InventoryManager.Instance.AddItem(_item);
                break;
            case PickupBehavior.MULTIPLE:
                InventoryManager.Instance.AddItems(_item, _quantity);
                break;
        }

        Destroy(gameObject);
    }

    public void TriggerExit(Collider other) { }
}
