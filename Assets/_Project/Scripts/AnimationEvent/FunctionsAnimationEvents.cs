
using UnityEngine;

public class FunctionsAnimationEvents : MonoBehaviour
{
    private SurfaceImpactAudioController _surfaceAudioController;

    private void Awake()
    {
        _surfaceAudioController = GetComponentInParent<SurfaceImpactAudioController>();
    }

    public void OnFootStep()
    {
        _surfaceAudioController.OnFootStep();
    }

    public void OnLanding()
    {
        _surfaceAudioController.OnLanding();
    }

    public void OnLeverPulled()
    {
        AudioManager.Instance.Play(SoundID.PULL_LEVER);
    }
}
