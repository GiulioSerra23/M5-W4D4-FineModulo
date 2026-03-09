using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMarkerController : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private GameObject _markerPrefab;

    [Header ("Marker Settings")]
    [SerializeField] private float _duration = 1f;
    [SerializeField] private float _spawnOffset = 0.025f;

    private GameObject _currentMarker;
    private Renderer _renderer;
    private Material _material;
    private Coroutine _animation;

    private void Awake()
    {
        _renderer = _markerPrefab.GetComponent<Renderer>();
        _material = _renderer.sharedMaterial; // Non ho capito perchè ma mi dava errore se non cambiavo lo shared
    }

    public void ShowMarker(RaycastHit hit)
    {
        if (_currentMarker == null) _currentMarker = Instantiate(_markerPrefab, transform);

        if (_animation != null) StopCoroutine(_animation);

        _currentMarker.SetActive(true);
        _currentMarker.transform.position = hit.point + hit.normal * _spawnOffset;
        _currentMarker.transform.rotation = Quaternion.LookRotation(hit.normal);

        _animation = StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        float timer = 0f;

        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        Color color = _material.color;

        _currentMarker.transform.localScale = startScale;

        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float time = timer / _duration;

            color.a = Mathf.Lerp(1f, 0f, time);
            _material.color = color;

            _currentMarker.transform.localScale = Vector3.Lerp(startScale, endScale, time);
            yield return null;
        }

        _currentMarker.SetActive(false);
    }
}
