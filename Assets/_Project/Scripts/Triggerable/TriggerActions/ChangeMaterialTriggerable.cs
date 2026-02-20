
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

    public void TriggerEnter()
    {
        _renderer.material = _material;
    }
    public void TriggerExit() { }
}
