using Cinemachine;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera[] _virtualCameras;

    private void ChangeCam()
    {
        int currentIndex = 0;
        int highestPriority = 0;

        for (int i = 0; i < _virtualCameras.Length; i++)
        {
            if (_virtualCameras[i].Priority > highestPriority)
            {
                highestPriority = _virtualCameras[i].Priority;
                currentIndex = i;
            }
        }
        _virtualCameras[currentIndex].Priority = 0;
        int nextIndex = (currentIndex + 1) % _virtualCameras.Length;
        _virtualCameras[nextIndex].Priority = highestPriority;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            ChangeCam();
        }
    }
}
