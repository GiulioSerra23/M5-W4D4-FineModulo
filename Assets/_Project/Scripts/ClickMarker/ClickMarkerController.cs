using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickMarkerController : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private GameObject _markerPrefab;

    [Header ("Marker Settings")]
    [SerializeField] private float _duration = 1f;

    private GameObject _currentMarker;

    public void ShowMarker(RaycastHit hit)
    {
        if (_currentMarker == null) _currentMarker = Instantiate(_markerPrefab, transform);

        _currentMarker.SetActive(true);
        _currentMarker.transform.position = hit.point + Vector3.one * 0.01f;
        _currentMarker.transform.rotation = Quaternion.LookRotation(hit.normal);

        StartCoroutine(Animate());
    }

    private IEnumerator Animate()
    {
        float timer = 0f;

        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;

        _currentMarker.transform.localScale = startScale;

        while (timer < _duration)
        {
            timer += Time.deltaTime;
            float time = timer / _duration;

            _currentMarker.transform.localScale = Vector3.Lerp(startScale, endScale, time);
            yield return null;
        }

        _currentMarker.SetActive(false);
    }
}
