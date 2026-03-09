using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateSoundController : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private FSM_Controller _controller;

    [Header("Sounds")]
    [SerializeField] private SoundID _chaseSound;
    [SerializeField] private SoundID _searchingSound;

    private void OnEnable()
    {
        _controller.OnStateChanged += HandleStateChanged;
    }

    public void HandleStateChanged(State state)
    {
        switch (state)
        {
            case State.CHASE:
                PlaySound(_chaseSound);
                break;
            case State.SEARCHING:
                PlaySound(_searchingSound);
                break;        
        }
    }

    private void PlaySound(SoundID sound)
    {
        if (sound == SoundID.NONE) return;

        AudioManager.Instance.Play3D(sound, transform);
    }

    private void OnDisable()
    {
        _controller.OnStateChanged -= HandleStateChanged;
    }
}
