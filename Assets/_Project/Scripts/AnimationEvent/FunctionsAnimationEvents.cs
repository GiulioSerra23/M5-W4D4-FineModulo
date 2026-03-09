
using UnityEngine;

public class FunctionsAnimationEvents : MonoBehaviour
{
    private SurfaceImpactAudioController _surfaceAudioController;
    private AnimationParamHandler _animHanlder;
    private MoveSelfTriggerable _moveSelfTriggerable;

    private void Awake()
    {
        _surfaceAudioController = GetComponentInParent<SurfaceImpactAudioController>();
        _animHanlder = GetComponentInParent<AnimationParamHandler>();
        _moveSelfTriggerable = GetComponentInParent<MoveSelfTriggerable>();
    }

    public void OnFootStep()
    {
        _surfaceAudioController.OnFootStep();
    }

    public void OnLeverPulled()
    {
        AudioManager.Instance.Play3D(SoundID.PULL_LEVER, transform);
    }

    public void ReturnToPos()
    {
        _moveSelfTriggerable.ForceExit();
    }

    public void OnGrabbed()
    {
        _animHanlder.SetIsGrabbing(false);
    }
}
