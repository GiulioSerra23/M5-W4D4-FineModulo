using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySlot : MonoBehaviour
{
    [Header ("Slot Settings")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _quatityText;

    public void SetData(InventorySlotData data)
    {
        if (data == null)
        {
            gameObject.SetActive(false);
            return;
        }

        gameObject.SetActive(true);

        _icon.sprite = data.Item.Icon;
        _quatityText.SetText(data.Quantity > 1 ? data.Quantity.ToString() : "");
    }
}

