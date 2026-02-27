using UnityEngine;

[CreateAssetMenu(fileName = "New Granade Item", menuName = "Data/Items/Granade Item")]
public class SO_GranadeItem : SO_GenericItem
{
    [Header ("Granade SetUp")]
    [SerializeField] private PoolType _poolType = PoolType.POOL_GRANADE_STUN;
    [SerializeField] private SO_GranadeEffect _effect;

    [Header("Throw Settings")]
    [SerializeField] private float _arcHeight = 2f;
    [SerializeField] private float _travelTime = 1f;

    public float ArcHeight => _arcHeight;
    public float TravelTime => _travelTime; 
    public SO_GranadeEffect Effect => _effect;

    public override void Use(GameObject user)
    {
        Vector3 targetPosition = MouseTargetIndicator.Instance.CurrentTarget;

        if (targetPosition == null) return;

        ObjectPool trapPool = PoolManager.Instance.GetPool(_poolType);
        PoolableObject obj = trapPool.Get();
        GranadeBehavior granade = obj as GranadeBehavior;

        granade.transform.position = user.transform.position;

        granade.SetUp(user.transform.position, targetPosition, this, user);
    }
}
