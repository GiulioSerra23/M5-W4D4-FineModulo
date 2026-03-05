using System.Collections.Generic;
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
    private HashSet<string> _params;

    private void Awake()
    {
        _anim = GetComponentInChildren<Animator>();

        SetHashes();
    }

    private void SetHashes()                      // Ho fatto questa funzione perchè avevo riscontrato un problema con le animazioni degli stati dei nemici, in quanto alcuni avevano bisogno
    {                                             // di un'animazione per uno stato invece altri no, e questo generava un warning, allora ho cercato un po in giro e ho visto che controllare 
        _params = new HashSet<string>();          // l'hash di una stringa e vedere quindi se l'animator contiene un paramentro con quel nome, era il modo più efficiente per farlo, senza
                                                  // usare una lista che dovrebbe ciclarsi ogni paramentro per controllare che sia quello giusto
        foreach (var param in _anim.parameters)
        {
            _params.Add(param.name);
        }
    }

    private bool HasParam(string name)
    {
        return _params.Contains(name);
    }

    public void SetForward(float speed)
    {
        if (!HasParam(_forwardName)) return;
        _anim.SetFloat(_forwardName, speed);
    }

    public void SetIsSearching(bool isSearching)
    {
        if (!HasParam(_isSearchingName)) return;
        _anim.SetBool(_isSearchingName, isSearching);
    }

    public void SetIsChasing(bool isChasing)
    {
        if (!HasParam(_isChasingName)) return;
        _anim.SetBool(_isChasingName, isChasing);
    }

    public void OnIsAttachedChanged(bool isAttached)
    {
        if (!HasParam(_isAttachedName)) return;
        _anim.SetBool(_isAttachedName, isAttached);
    }

    public void OnPull()
    {
        if (!HasParam(_pullName)) return;
        _anim.SetTrigger(_pullName);
    }
}
