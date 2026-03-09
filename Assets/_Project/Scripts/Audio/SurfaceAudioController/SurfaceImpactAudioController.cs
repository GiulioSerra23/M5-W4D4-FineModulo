
using UnityEngine;

public class SurfaceImpactAudioController : MonoBehaviour
{
    [Header ("RayCast Settings")]
    [SerializeField] private float _maxDistance = 1.5f;
    [SerializeField] private LayerMask _layerMask;
    [SerializeField] private Transform _origin;

    private void TryPlaySurfaceSound()
    {
        Vector3 origin = _origin != null ? _origin.position : transform.position;

        Debug.DrawRay(_origin.position, Vector3.down * _maxDistance, Color.yellow, 0.1f);
        if (Physics.Raycast(origin, Vector3.down, out RaycastHit hit, _maxDistance, _layerMask))
        {
            if (!hit.collider.TryGetComponent<SurfaceType>(out var surfaceType)) return;

            SoundID sound = ConvertSurfaceToSound(surfaceType.IDSurface);
            AudioManager.Instance.Play3D(sound, transform);
        }
    }

    public void OnFootStep()
    {
        TryPlaySurfaceSound();
    }

    private SoundID ConvertSurfaceToSound(SurfaceID surface)
    {
        switch (surface)
        {
            case SurfaceID.GRASS:
                return SoundID.FOOTSTEPS_GRASS;
            case SurfaceID.ROCK:
                return SoundID.FOOTSTEPS_ROCK;
            case SurfaceID.WOOD:
                return SoundID.FOOTSTEPS_WOOD;
            default:
                return SoundID.FOOTSTEPS_GRASS;
        }
    }
}
