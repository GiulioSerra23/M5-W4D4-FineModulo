using UnityEngine;

public enum SoundID
{
    FOOTSTEPS_GRASS = 0,
    FOOTSTEPS_ROCK = 1,
    FOOTSTEPS_WOOD = 2,

    ITEMMATCH_CHESSPIECE = 10,

    ENEMY_STATE_CHASE = 20,
    ENEMY_STATE_SEARCHING = 21,

    COLLISION_PAWN = 30,
    COLLISION_BOOK = 31,

    PULL_LEVER = 50,

    HIT_PLAYER = 60,

    NONE = 100,
}

[System.Serializable]
public class SoundData
{
    [SerializeField] private SoundID _iD;
    [SerializeField] private AudioClip[] _clips;

    public SoundID ID => _iD;
    public AudioClip[] Clips => _clips;
}
