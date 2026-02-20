using UnityEngine;

public enum SoundID
{
    FOOTSTEPS_GRASS = 0,
    FOOTSTEPS_ROCK = 1,
    FOOTSTEPS_WOOD = 2,

    LANDING_GRASS = 10,
    LANDING_ROCK = 11,
    LANDING_WOOD = 12,
    LANDING_WATER = 13,

    PICKUP_COIN = 20,
    PICKUP_HEAL = 21,
    PICKUP_TIME = 22,
    PICKUP_PET = 23,

    BULLETCOLLISION_ROCK = 30,
    BULLETCOLLISION_BANANA = 31,
    BULLETCOLLISION_LOG = 32,
    BULLETCOLLISION_COCONUT = 33,        

    TURRET_SHOOT_CACTUS = 40,

    PULL_LEVER = 50,

    ACTIVE_RUNE = 55,
    ACTIVE_DOOR_STONE = 56,

    HIT_PLAYER = 60,

    NONE = 100,
}

[System.Serializable]
public class SoundData
{
    public SoundID ID;
    public AudioClip[] Clips;
}
