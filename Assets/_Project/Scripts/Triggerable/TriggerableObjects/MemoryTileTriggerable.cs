
using UnityEngine;
using UnityEngine.Events;

public class MemoryTileTriggerable : MonoBehaviour, ITriggerable
{
    [Header ("References")]
    [SerializeField] private MemoryPuzzleManager _manager;

    [Header ("Tile Settings")]
    [SerializeField] private int _id;    

    [Header ("Events")]
    [SerializeField] private UnityEvent _onTileLocked;
    [SerializeField] private UnityEvent _onTileHide;

    private bool _isRevealed;
    private bool _isLocked;

    public int ID => _id;

    public void Reveal()
    {
        _isRevealed = true;
    }

    public void Hide()
    {
        _isRevealed = false;
        _onTileHide.Invoke ();
    }

    public void Lock()
    {
        _isLocked = true;
        _onTileLocked.Invoke ();
    }

    public void TriggerEnter()
    {
        if (_isLocked) return;
        if (_isRevealed ) return;

        Reveal();
        _manager.TileRevealed(this);
    }

    public void TriggerExit() { }
}
