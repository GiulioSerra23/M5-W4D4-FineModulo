using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Key Item", menuName = "Data/Items/Key Item")]
public class SO_KeyItem : SO_GenericItem
{
    public override void Use(GameObject user)
    {
        Debug.Log($"{user.name} use the key {Name}");
    }
}
