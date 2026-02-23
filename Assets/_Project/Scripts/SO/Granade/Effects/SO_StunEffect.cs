using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stun Effect", menuName = "Data/Granade Effect/Stun")]
public class SO_StunEffect : SO_GranadeEffect
{
    [Header ("Stun Settings")]
    [SerializeField] private float _radius = 4f;
    [SerializeField] private float _stunDuration = 3f;
    [SerializeField] private int _maxEntityStun = 3;
    [SerializeField] private LayerMask _stunMask;

    private Collider[] _hits;

    private void Awake()
    {
        _hits = new Collider[_maxEntityStun];
    }

    public override void Apply(GameObject user, Vector3 position)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(position, _radius, _hits, _stunMask);

        for (int i = 0; i < hitCount; i++)
        {
            Collider hit = _hits[i];

            if (hit.TryGetComponent<BaseEnemy>(out var enemy))
            {
                enemy.ApplyStun(_stunDuration);
            }
        }
    }
}
