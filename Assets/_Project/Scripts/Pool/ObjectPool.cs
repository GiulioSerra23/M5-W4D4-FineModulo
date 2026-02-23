
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool: MonoBehaviour
{
    [SerializeField] private PoolableObject _prefab;
    [SerializeField] private int _poolSize = 20;

    private List<PoolableObject> _objects = new List<PoolableObject>();

    private void Awake()
    {
        for (int i = 0; i < _poolSize; i++)
        {
            CreateObject();
        }
    }

    private PoolableObject CreateObject()
    {
        PoolableObject obj = Instantiate(_prefab, transform);
        obj.gameObject.SetActive(false);
        obj.SetPool(this);
        _objects.Add(obj);
        return obj;
    }

    public PoolableObject Get()
    {
        foreach (var obj in _objects)
        {
            if (!obj.gameObject.activeInHierarchy)
            {
                obj.gameObject.SetActive(true);
                obj.OnSpawned();
                return obj;
            }
        }
        return null;
    }

    public void ReturnToPool(PoolableObject obj)
    {
        obj.OnDespawned();
        obj.gameObject.SetActive(false);
    }
}


