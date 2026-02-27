using UnityEngine;

[CreateAssetMenu(fileName = "New Healing Item", menuName = "Data/Items/Healing Item")]
public class SO_HealingItem : SO_GenericItem
{
    [SerializeField] private int _healAmount;
    
    public int HealAmount => _healAmount;

    public override void Use(GameObject user)
    {
        if (!user.TryGetComponent<LifeController>(out var userLife)) return;

        userLife.AddHp(_healAmount);
    }
}
