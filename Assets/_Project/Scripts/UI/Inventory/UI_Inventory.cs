using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private InventoryManager _inventory; // Ho preso l'inventario serializzato invece di usare l'istanza perchè se no mi dava problemi con l'ordine in cui vengono chiamati Awake
    [SerializeField] private GameObject _container;       // e OnEnable e quindi mi dava null l'instanza ad inizio del gioco

    [Header ("Slots")]
    [SerializeField] private List<UI_InventorySlot> _slots;

    private void OnEnable()
    {
        _inventory.OnInventoryChanged += Refresh;
        Refresh();
    }

    private void Refresh()
    {
        if (_inventory.SlotCount == 0)
        {
            _container.gameObject.SetActive(false);
            return;
        }

        _container.gameObject.SetActive(true);

        for (int i = 0; i < _slots.Count - 1; i++)
        {
            if (i < _inventory.SlotCount)
            {
                _slots[i].SetData(_inventory.GetSlot(i));
            }
            else
            {
                _slots[i].SetData(null);
            }
        }

    }

    private void OnDisable()
    {
        _inventory.OnInventoryChanged -= Refresh;
    }
}
