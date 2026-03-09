using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header ("Gameplay Cameras (Ping Pong)")]
    [SerializeField] private CinemachineVirtualCamera[] _gameplayCameras;

    [Header ("Cinematic Cameras (One Shot)")]
    [SerializeField] private CinemachineVirtualCamera[] _cinematicCameras;
    [SerializeField] private float[] _cinematicDurations;
    [SerializeField] private bool _isCamPerspective = true;

    [Header("Special Perspective Camera")]
    [SerializeField] private CinemachineVirtualCamera _specialCamera;

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
    private int _specialPriority = 20;

    private Camera _mainCamera;

    private void Awake()
    {
        _mainCamera = Camera.main;
    }

    void Start()
    {
        SetUpCams();
    }

    private void SetUpCams()
    {
        foreach (var cam in _gameplayCameras) cam.Priority = 0;

        foreach (var cam in _cinematicCameras) cam.Priority = 0;

        if (_specialCamera != null) _specialCamera.Priority = 0;

        if (_gameplayCameras.Length > 0)
        {
            _gameplayCameras[0].Priority = _gameplayPriority;
        }

        _mainCamera.orthographic = true;
    }

    private void SetActiveObjectsDuringCinematic(bool isActive)
    {
        _canvas.alpha = isActive ? 1 : 0;        
        _player.Agent.isStopped = !isActive;        
        _mouseTargetIndicator.ShowLine = isActive;
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

    public void SetCamPerspective(bool isCamPerspective)
    {
        _isCamPerspective = isCamPerspective;
    }

    public void PlayCinematic(int cinematicIndex)
    {
        if (!_canChange) return;

        StartCoroutine(CinematicRoutine(cinematicIndex));
    }

    private IEnumerator CinematicRoutine(int cinematicIndex)
    {
        _canChange = false;

        CinemachineVirtualCamera gameplayCam = _gameplayCameras[_currentGameplayIndex];
        CinemachineVirtualCamera cinematicCam = _cinematicCameras[cinematicIndex];

        SetActiveObjectsDuringCinematic(false);

        if (_isCamPerspective) _mainCamera.orthographic = false;

        gameplayCam.Priority = 0;
        cinematicCam.Priority = _cinematicPriority;

        yield return new WaitForSeconds(_cinematicDurations[cinematicIndex]);

        SetActiveObjectsDuringCinematic(true);

        if (_isCamPerspective) _mainCamera.orthographic = true;

        cinematicCam.Priority = 0;
        gameplayCam.Priority = _gameplayPriority;

        _canChange = true;
    }

    public void ActivateSpecialCamera()
    {
        if (!_canChange) return;

        CinemachineVirtualCamera gameplayCam = _gameplayCameras[_currentGameplayIndex];
        gameplayCam.Priority = 0;

        _specialCamera.Priority = _specialPriority;

        _mainCamera.orthographic = false;
    }


    public void ReturnToGameplayCamera()
    {
        _specialCamera.Priority = 0;

        CinemachineVirtualCamera gameplayCam = _gameplayCameras[_currentGameplayIndex];
        gameplayCam.Priority = _gameplayPriority;

        _mainCamera.orthographic = true;
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
