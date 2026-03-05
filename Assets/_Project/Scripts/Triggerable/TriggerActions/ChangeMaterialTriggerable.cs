
using UnityEngine;

public class ChangeMaterialTriggerable : MonoBehaviour, ITriggerable
{
    [Header ("Material Settings")]
    [SerializeField] private Material _material;

    private Renderer _renderer;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void TriggerEnter(Collider other)
    {
        _renderer.material = _material;
    }
    public void TriggerExit(Collider other) { }
}
