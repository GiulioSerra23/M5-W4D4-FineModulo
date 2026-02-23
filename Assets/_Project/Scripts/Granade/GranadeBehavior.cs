using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeBehavior : PoolableObject
{
    private SO_GranadeItem _data;
    private GameObject _user;

    private Vector3 _start;
    private Vector3 _end;    

    private float _timer;

    public void SetUp(Vector3 start, Vector3 end, SO_GranadeItem data, GameObject user)
    {
        _start = start;
        _end = end;
        _data = data;
        _user = user;
        _timer = 0f;
    }

    public override void OnSpawned()
    {
        _timer = 0f;
    }

    public override void OnDespawned()
    {
        _timer = 0f;
        _data = null;
        _user = null;
    }

    private void Explode()
    {
        if (_data.Effect != null)
        {
            _data.Effect.Apply(_user, transform.position);
        }

        Release();
    }    

    private void Update()
    {
        if (_data == null) return;

        _timer += Time.deltaTime;
        float time = _timer / _data.TravelTime;

        if (time >= 1f)
        {
            Explode();
            return;
        }

        Vector3 position = Vector3.Lerp( _start, _end, time);
        position.y += Mathf.Sin(time * Mathf.PI) * _data.ArcHeight;

        transform.position = position;
    }
}
