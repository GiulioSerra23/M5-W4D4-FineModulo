
using UnityEngine;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private int _damageAmount;

    private bool _canDoDamage = false;

    public bool CanDoDamage { get => _canDoDamage; set => _canDoDamage = value; }

    private void OnTriggerEnter(Collider other)
    {
        if (!_canDoDamage) return;
        if (!other.TryGetComponent<LifeController>(out var lifeController)) return;

        lifeController.TakeDamage(_damageAmount);
    }
}
