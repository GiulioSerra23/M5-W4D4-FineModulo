using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_InventorySlot : MonoBehaviour
{
    [Header ("Slot Settings")]
    [SerializeField] private Image _icon;
    [SerializeField] private TextMeshProUGUI _quatityText;

    private void Awake()
    {
        //WarmUpSlot(); // Ho fatto questa funzione perchè non mi è ben chiaro il motivo ma ad un certo punto prendere il primo pickup mi faceva laggare leggermente, facendo questo
    }                 // attiva e disattiva ad inizio game, non mi deve caricare gli slot tutti insieme quando prendo il pickup e quindi lagga meno, probabilmente converebbe avere un Manager
                      // che fa il WarmUp di tutto ad inizio game così da non avere questi tipi di problemi
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

    private void WarmUpSlot()
    {
        gameObject.SetActive(true);
        gameObject.SetActive(false);
    }
}

