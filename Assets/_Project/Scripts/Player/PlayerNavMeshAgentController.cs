using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerNavMeshAgentController : MonoBehaviour
{
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
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                _agent.SetDestination(hit.point);
            }
        }
    }

    void Update()
    {
        HandleClickToMove();
    }
}
