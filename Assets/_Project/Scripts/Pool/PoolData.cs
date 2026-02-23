
public enum PoolType
{
    POOL_GRANADE_STUN,
    POOL_GRANADE_NOISE,
}

[System.Serializable]
public class PoolEntry
{
    public PoolType PoolType;
    public ObjectPool Pool;
}
