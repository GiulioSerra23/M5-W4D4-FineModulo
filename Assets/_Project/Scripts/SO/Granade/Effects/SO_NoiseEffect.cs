using UnityEngine;

[CreateAssetMenu(fileName = "New Noise Effect", menuName = "Data/Granade Effect/Noise")]
public class SO_NoiseEffect : SO_GranadeEffect
{
    [Header("Noise Settings")]
    [SerializeField] private float _radius = 4f;
    [SerializeField] private int _maxEntityAlerted = 3;
    [SerializeField] private LayerMask _alertMask;

    private Collider[] _hits;

    private void Awake()
    {
        _hits = new Collider[_maxEntityAlerted];
    }

    public override void Apply(GameObject user, Vector3 position)
    {
        int hitCount = Physics.OverlapSphereNonAlloc(position, _radius, _hits, _alertMask);

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
