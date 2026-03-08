using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class ItemMatchingPuzzleManager : MonoBehaviour
{
    [SerializeField] private ItemMatchingSlotTriggerable[] _slots;

    [Header("Events")]
    [SerializeField] private UnityEvent _onPuzzleCompleted;

    private bool _completed;

    private void OnEnable()
    {
        foreach (var slot in _slots)
        {
            slot.OnSlotStateChanged += HandleSlotStateChanged;
        }
    }

    private void HandleSlotStateChanged(ItemMatchingSlotTriggerable slot)
    {
        if (_completed) return;

        var allCorrect = _slots.All(slot => slot.IsCorrect);

        if (allCorrect)
        {
            CompletePuzzle();
        }
    }

    private void CompletePuzzle()
    {
        _completed = true;
        _onPuzzleCompleted.Invoke();
    }

    private void OnDisable()
    {
        foreach (var slot in _slots)
        {
            slot.OnSlotStateChanged -= HandleSlotStateChanged;
        }
    }
}
