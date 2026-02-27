using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerAgentController : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private ClickMarkerController _clickMarker;

    [Header ("Raycast Settings")]
    [SerializeField] private LayerMask _layerMask;

    private NavMeshAgent _agent;
    private Camera _mainCam;

    private void Awake()
    {
        _agent = GetComponent<NavMeshAgent>();
        _mainCam = Camera.main;
    }

    private void HandleClickToMove()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = _mainCam.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, Mathf.Infinity, _layerMask))
            {
                _agent.SetDestination(hit.point);
                _clickMarker.ShowMarker(hit);
            }
        }
    }

    void Update()
    {
        HandleClickToMove();
    }
}
