
using UnityEngine;

public class AnimationParamHandler : MonoBehaviour
{
    [Header ("Param Names")]
    [SerializeField] private string _forwardName = "forward";
    [SerializeField] private string _isGroundedName = "isGrounded";
    [SerializeField] private string _isAttached = "isAttached";
    [SerializeField] private string _jumpName = "jump";
    [SerializeField] private string _pullName = "pull";

    private Animator _anim;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();
    }

    public void SetForward(float speed)
    {
        _anim.SetFloat(_forwardName, speed);
    }

    public void OnIsGroundedChanged(bool isGrounded)
    {
        _anim.SetBool(_isGroundedName, isGrounded);
    }

    public void OnIsAttachedChanged(bool isAttached)
    {
        _anim.SetBool(_isAttached, isAttached);
    }

    public void OnJump()
    {
        _anim.SetTrigger(_jumpName);
    }

    public void OnPull()
    {
        _anim.SetTrigger(_pullName);
    }
}
