
using System.Collections.Generic;
using UnityEngine;

public class MemoryPuzzleManager : MonoBehaviour
{
    [Header ("Delay Settings")]
    [SerializeField] private float _resetDelay = 1.5f;

    private List<MemoryTileTriggerable> _revealedTiles = new List<MemoryTileTriggerable>();

    private MemoryTileTriggerable _firstTile;
    private MemoryTileTriggerable _secondTile;

    private bool _isChecking;
    private float _checkTimer;

    public void TileRevealed(MemoryTileTriggerable tile)
    {
        if (_isChecking) return;

        _revealedTiles.Add(tile);

        if (_revealedTiles.Count == 2)
        {
            _firstTile = _revealedTiles[0];
            _secondTile = _revealedTiles[1];

            if (_firstTile.ID == _secondTile.ID)
            {
                _firstTile.Lock();
                _secondTile.Lock();
                _revealedTiles.Clear();
            }
            else
            {
                _isChecking = true;
                _checkTimer = _resetDelay;
            }
        }
    }

    private void ResolveCheck()
    {
        if (_firstTile != null && _secondTile != null)
        {
            _firstTile.Hide();
            _secondTile.Hide();
        }

        _revealedTiles.Clear();
        _isChecking = false;
        _firstTile = null;
        _secondTile = null;
    }

    private void Update()
    {
        if (_isChecking)
        {
            _checkTimer -= Time.deltaTime;

            if (_checkTimer <= 0f)
            {
                ResolveCheck();
            }
        }
    }
}
