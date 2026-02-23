
using UnityEngine;

public abstract class PoolableObject : MonoBehaviour, IPoolable
{
    private ObjectPool _pool;

    public void SetPool(ObjectPool pool) => _pool = pool;
    
    public void Release() => _pool.ReturnToPool(this);

    public virtual void OnSpawned() { }
    public virtual void OnDespawned() { }
}
