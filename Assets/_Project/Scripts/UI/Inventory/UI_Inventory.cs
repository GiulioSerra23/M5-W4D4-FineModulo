using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Inventory : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private InventoryManager _inventory; // Ho preso l'inventario serializzato invece di usare l'istanza perchè se no mi dava problemi con l'ordine in cui vengono chiamati Awake
    [SerializeField] private CanvasGroup _container;      // e OnEnable e quindi mi dava null l'instanza ad inizio del gioco

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
            _container.alpha = 0f;
            return;
        }

        _container.alpha = 1f;

        int slotCount = Mathf.Min(_slots.Count, _inventory.SlotCount);

        for (int i = 0; i < slotCount; i++)
        {
            if (_slots[i] == null) continue;

            _slots[i].SetData(_inventory.GetSlot(i));
        }

        for (int i = slotCount; i < _slots.Count; i++)
        {
            if (_slots[i] == null) continue;

            _slots[i].SetData(null);
        }

    }

    private void OnDisable()
    {
        _inventory.OnInventoryChanged -= Refresh;
    }
}
