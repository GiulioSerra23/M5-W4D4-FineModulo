using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    [SerializeField] private SO_GenericItem _item;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag(Tags.Player)) return;

        InventoryManager.Instance.AddItem(_item);
        Destroy(gameObject);
    }
}
