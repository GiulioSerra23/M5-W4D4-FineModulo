using UnityEngine;

[CreateAssetMenu(fileName = "New Noise Effect", menuName = "Data/Granade Effect/Noise")]
public class SO_NoiseEffect : SO_GranadeEffect
{
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
                enemy.ReceiveAlert(position);
            }
        }
    }
}
