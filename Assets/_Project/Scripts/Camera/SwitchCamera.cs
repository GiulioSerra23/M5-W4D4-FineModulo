using Cinemachine;
using UnityEngine;

public class SwitchCamera : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private CinemachineVirtualCamera[] _virtualCameras;

    [Header ("Debug (Press C)")]
    [SerializeField] private bool _debugSwitch = false;

    private bool _canChange = true;

    public void SwitchCam()
    {
        if (!_canChange) return;

        _canChange = false;

        DoSwitch();
    }

    private void DoSwitch()
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

    public void SetCanChange(bool canChange)
    {
        _canChange = canChange;
    }

    private void Update()
    {
        if (!_debugSwitch) return;

        if (Input.GetKeyDown(KeyCode.C))
        {
            DoSwitch();
        }
    }
}
