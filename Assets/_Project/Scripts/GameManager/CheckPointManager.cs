
using UnityEngine;

public class CheckPointManager : MonoBehaviour
{
    private Vector3 _respawnPoint;
    private bool _hasCheckPoint = false;

    public void SetCheckPoint(Vector3 newCheckPoint)
    {
        _respawnPoint = newCheckPoint;
        _hasCheckPoint = true;
    }

    public Vector3 GetCheckPoint() => _respawnPoint;

    public bool HasCheckPoint() => _hasCheckPoint;
}
