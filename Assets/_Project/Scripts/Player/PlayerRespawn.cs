
using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerRespawn : MonoBehaviour
{
    [Header("Lives Settings")]
    [SerializeField] private int _maxLives;

    [Header("Events")]    
    [SerializeField] private UnityEvent _onLivesEnded;   

    private PlayerAgentController _playerController;
    private LifeController _lifeController;
    private int _currentLives;

    public event Action<int> OnLifeLost;

    private void OnEnable()
    {
        _lifeController.OnDie += handleDeath;
    }

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
        OnLifeLost?.Invoke(_currentLives);
        
    }

    private void handleDeath()
    {
        LoseALive();

        if (_currentLives <= 0)
        {
            _onLivesEnded.Invoke();
            return;
        }
        
        Respawn();
    }

    public void Respawn()
    {
        if (!CheckPointManager.Instance.HasCheckPoint()) return;        

        transform.position = CheckPointManager.Instance.GetCheckPoint();
        
        _lifeController.RestoreFullHp();
        _playerController.Agent.ResetPath();
    }

    private void OnDisable()
    {
        _lifeController.OnDie -= handleDeath;
    }
}
