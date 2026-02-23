
using UnityEngine;

public class PoolManager : MonoBehaviour
{
    public static PoolManager Instance { get; private set; }

    [SerializeField] private PoolEntry[] _pools;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    public ObjectPool GetPool(PoolType poolType)
    {
        foreach (var poolEntry in _pools)
        {
            if (poolEntry.PoolType == poolType) return poolEntry.Pool;
        }
        return null;
    }
}
