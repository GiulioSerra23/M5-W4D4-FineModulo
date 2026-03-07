
using UnityEngine;

public enum PoolType
{
    POOL_GRANADE_STUN,
    POOL_GRANADE_NOISE,
}

[System.Serializable]
public class PoolEntry
{
    [SerializeField] private PoolType _poolType;
    [SerializeField] private ObjectPool _pool;

    public PoolType PoolType => _poolType;
    public ObjectPool Pool => _pool;
}
