
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    public static CheckPointManager Instance { get; private set; }

    private Vector3 _respawnPoint;
    private bool _hasCheckPoint = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    public void SetCheckPoint(Vector3 newCheckPoint)
    {
        _respawnPoint = newCheckPoint;
        _hasCheckPoint = true;
    }

    public Vector3 GetCheckPoint() => _respawnPoint;

    public bool HasCheckPoint() => _hasCheckPoint;
}
