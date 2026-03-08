using Cinemachine;
using System.Collections;
using UnityEngine;

// Questo script non mi piace per vari motivi ma avevo troppo poco tempo per pensare a come collegare meglio le cose

public class SwitchCamera : MonoBehaviour
{
    [Header ("Gameplay Cameras (Ping Pong)")]
    [SerializeField] private CinemachineVirtualCamera[] _gameplayCameras;

    [Header ("Cinematic Cameras (One Shot)")]
    [SerializeField] private CinemachineVirtualCamera[] _cinematicCameras;

    [Header ("DisactivedObjectDuringCinematic")]
    [SerializeField] private CanvasGroup _canvas;
    [SerializeField] private PlayerAgentController _player;
    [SerializeField] private MouseTargetIndicator _mouseTargetIndicator;

    [Header ("Debug (Press C)")]
    [SerializeField] private bool _debugSwitch = false;

    private int _currentGameplayIndex = 0;
    private bool _canChange = true;

    private int _gameplayPriority = 10;
    private int _cinematicPriority = 20;

    void Start()
    {
        for (int i = 0; i < _gameplayCameras.Length; i++)
        {
            _gameplayCameras[i].Priority = 0;
        }

        for (int i = 0; i < _cinematicCameras.Length; i++)
        {
            _cinematicCameras[i].Priority = 0;
        }

        if (_gameplayCameras.Length > 0)
        {
            _gameplayCameras[0].Priority = _gameplayPriority;
        }
    }

    private void SetActiveObjectsDuringCinematic(bool isActive)
    {
        _canvas.alpha = isActive ? 1 : 0;
        _player.enabled = isActive;
        _mouseTargetIndicator.enabled = isActive;
    }

    public void SwitchGameplayCamera()
    {
        if (!_canChange) return;

        int nextIndex = (_currentGameplayIndex + 1) % _gameplayCameras.Length;

        _gameplayCameras[_currentGameplayIndex].Priority = 0;
        _gameplayCameras[nextIndex].Priority = _gameplayPriority;

        _currentGameplayIndex = nextIndex;

        _canChange = false;
    }

    public void SetCanChange(bool canChange)
    {
        _canChange = canChange;
    }

    public void PlayCinematic(int cinematicIndex, float duration)
    {
        if (!_canChange) return;

        StartCoroutine(CinematicRoutine(cinematicIndex, duration));
    }

    public void PlayCinematic1()
    {
        PlayCinematic(0, 3f);
    }

    public void PlayCinematic2()
    {
        PlayCinematic(1, 3f);
    }

    private IEnumerator CinematicRoutine(int cinematicIndex, float duration)
    {
        _canChange = false;

        CinemachineVirtualCamera gameplayCam = _gameplayCameras[_currentGameplayIndex];
        CinemachineVirtualCamera cinematicCam = _cinematicCameras[cinematicIndex];

        SetActiveObjectsDuringCinematic(false);

        gameplayCam.Priority = 0;
        cinematicCam.Priority = _cinematicPriority;

        yield return new WaitForSeconds(duration);

        SetActiveObjectsDuringCinematic(true);

        cinematicCam.Priority = 0;
        gameplayCam.Priority = _gameplayPriority;

        _canChange = true;
    }

    private void Update()
    {
        if (!_debugSwitch) return;

        if (Input.GetKeyDown(KeyCode.C))
        {
            SwitchGameplayCamera();
        }
    }
}
