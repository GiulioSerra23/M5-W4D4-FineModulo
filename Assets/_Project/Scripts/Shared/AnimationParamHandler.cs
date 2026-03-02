using UnityEngine;

public class AnimationParamHandler : MonoBehaviour
{
    [Header ("Param Names")]
    [SerializeField] private string _forwardName = "forward";
    [SerializeField] private string _isAttachedName = "isAttached";
    [SerializeField] private string _isSearchingName = "isSearching";
    [SerializeField] private string _isChasingName = "isChasing";
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

    public void SetIsSearching(bool isSearching)
    {
        _anim.SetBool(_isSearchingName, isSearching);
    }

    public void SetIsChasing(bool isChasing)
    {
        _anim.SetBool(_isChasingName, isChasing);
    }

    public void OnIsAttachedChanged(bool isAttached)
    {
        _anim.SetBool(_isAttachedName, isAttached);
    }

    public void OnPull()
    {
        _anim.SetTrigger(_pullName);
    }
}
