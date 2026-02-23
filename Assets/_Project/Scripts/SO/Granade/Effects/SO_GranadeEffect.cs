using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SO_GranadeEffect : ScriptableObject
{
    public abstract void Apply(GameObject user, Vector3 position);
}
