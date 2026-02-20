
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private CheckPointManager _checkPointManager;

    private LifeController _lifeController;

    private void Awake()
    {
        _lifeController = GetComponent<LifeController>();
    }
     
    public void Respawn()
    {
        if (!_checkPointManager.HasCheckPoint()) return;

        transform.position = _checkPointManager.GetCheckPoint();
        _lifeController.RestoreFullHp();
    }
}
