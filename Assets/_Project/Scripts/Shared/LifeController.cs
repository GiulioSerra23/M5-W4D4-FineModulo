
using UnityEngine;
using UnityEngine.Events;

public class LifeController : MonoBehaviour
{
    [Header("Sound ID")]
    [SerializeField] protected SoundID _hitAudio;

    [Header("Life Settings")]
    [SerializeField] private int _maxHp;
    [SerializeField] private int _currentHp;
    [SerializeField] private bool _startFullHp = true;

    [Header("Events")]
    [SerializeField] private UnityEvent<int> _onHpChanged;
    [SerializeField] private UnityEvent _onDie;

    public int CurrentHp { get => _currentHp; private set => SetHp(value); }

    private void Start()
    {
        if (_startFullHp)
        {
            RestoreFullHp();
        }
    }

    public void RestoreFullHp() => SetHp(_maxHp);

    public void SetMaxHp(int maxHp) => _maxHp = maxHp;

    private void SetHp(int hp)
    {
        hp = Mathf.Clamp(hp, 0, _maxHp);

        if (hp != _currentHp)
        {
            _currentHp = hp;
            _onHpChanged.Invoke(_currentHp);

            if (_currentHp <= 0)
            {
                _onDie.Invoke();
            }
        }
    }

    public void AddHp(int amount) => SetHp(_currentHp + amount);

    public void TakeDamage(int amount)
    {
        SetHp(_currentHp - amount);
        AudioManager.Instance.Play(_hitAudio);
    }
}
