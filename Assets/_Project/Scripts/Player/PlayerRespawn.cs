
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Events")]
    [SerializeField] private UnityEvent<int> _onLifeLost;
    [SerializeField] private UnityEvent _onLivesEnded;

    [Header("Lives Settings")]
    [SerializeField] private int _maxLives;

    private PlayerAgentController _playerController;
    private LifeController _lifeController;
    private int _currentLives;

    private void Start()
    {
        _currentLives = _maxLives;
    }

    private void Awake()
    {
        _playerController = GetComponent<PlayerAgentController>();
        _lifeController = GetComponent<LifeController>();
    }

    private void LoseALive()
    {
        _currentLives--;

        _onLifeLost.Invoke(_currentLives);
        if (_currentLives <= 0)
        {
            _onLivesEnded.Invoke();
        }
    }

    public void Respawn()
    {
        if (!CheckPointManager.Instance.HasCheckPoint()) return;        

        transform.position = CheckPointManager.Instance.GetCheckPoint();
        LoseALive();
        _lifeController.RestoreFullHp();
        _playerController.Agent.ResetPath();
    }
}
