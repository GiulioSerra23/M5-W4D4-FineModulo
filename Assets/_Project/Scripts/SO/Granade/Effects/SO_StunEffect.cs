using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(fileName = "New Stun Effect", menuName = "Data/Granade Effect/Stun")]
public class SO_StunEffect : SO_GranadeEffect
{
    [Header ("Stun Settings")]
    [SerializeField] private float _stunDuration = 3f;

    private Collider[] _hits;

    private void Awake()
    {
        _hits = new Collider[_maxEntityEffected];
    }

    public override void Apply(GameObject user, Vector3 position)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(position, _radius, _hits, _layerMask);

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
