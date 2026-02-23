
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    private LifeController _lifeController;

    private void Awake()
    {
        _lifeController = GetComponent<LifeController>();
    }
     
    public void Respawn()
    {
        if (!CheckPointManager.Instance.HasCheckPoint()) return;

        transform.position = CheckPointManager.Instance.GetCheckPoint();
        _lifeController.RestoreFullHp();
    }
}
