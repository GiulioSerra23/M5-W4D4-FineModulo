using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranadeBehavior : PoolableObject
{
    [Header ("Impact Effect Settings")]
    [SerializeField] private Transform _visualRadius;
    [SerializeField] private float _expandDuration = 0.3f;

    private SO_GranadeItem _data;
    private GameObject _user;

    private Vector3 _start;
    private Vector3 _end;    

    private float _timer;
    private bool _hasExploded = false;

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
        _hasExploded = false;

        if (_visualRadius != null)
        {
            _visualRadius.gameObject.SetActive(false);
            _visualRadius.localScale = Vector3.zero;
        }
    }

    private void Explode()
    {
        if (_hasExploded) return;
        _hasExploded = true;

        StartCoroutine(ExpandAndApply());
    }

    private IEnumerator ExpandAndApply()
    {
        if (_visualRadius != null)
        {
            _visualRadius.gameObject.SetActive(true);
        }

        float timer = 0f;
        float targetRadius = _data.Effect.Radius;

        while (timer < _expandDuration)
        {
            timer += Time.deltaTime;
            float time = timer / _expandDuration;

            float currentRadius = Mathf.Lerp(0f, targetRadius, time);

            if (_visualRadius != null)
            {
                _visualRadius.localScale = Vector3.one * currentRadius * 2f;
            }

            yield return null;
        }

        _data.Effect.Apply(_user, transform.position);

        Release();
    }
    
    private void Shoot()
    {
        if (_hasExploded) return; 

        _timer += Time.deltaTime;
        float time = _timer / _data.TravelTime;

        if (time >= 1f)
        {
            Explode();
            return;
        }

        Vector3 position = Vector3.Lerp(_start, _end, time);
        position.y += Mathf.Sin(time * Mathf.PI) * _data.ArcHeight;

        transform.position = position;
    }

    private void Update()
    {
        if (_data == null) return;

        Shoot();
    }

    private void OnTriggerEnter(Collider other)
    {
        Explode();
    }
}
