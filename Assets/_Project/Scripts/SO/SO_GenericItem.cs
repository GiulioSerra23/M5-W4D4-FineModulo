using UnityEngine;

public abstract class SO_GenericItem : ScriptableObject
{
    [SerializeField] private int _id;
    [SerializeField] private string _name;
    [SerializeField] private Sprite _icon;
    [SerializeField] private string _description;

    public int ID => _id;
    public Sprite Icon => _icon;
    public string Name => _name;
    public string Description => _description;

    public abstract void Use(GameObject user);
}
