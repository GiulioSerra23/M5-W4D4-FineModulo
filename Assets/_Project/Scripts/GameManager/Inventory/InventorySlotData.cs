
[System.Serializable]
public class InventorySlotData
{
    public SO_GenericItem Item;
    public int Quantity;

    public InventorySlotData(SO_GenericItem item)
    {
        Item = item;
        Quantity = 1;
    }
}
