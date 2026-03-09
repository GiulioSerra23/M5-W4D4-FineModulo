using TMPro;
using UnityEngine;

public class UI_Life : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _livesText;
    [SerializeField] private PlayerRespawn _player;

    private void OnEnable()
    {
        _player.OnLifeLost += UpdateLivesUI;
    }

    public void UpdateLivesUI(int lives)
    {
        _livesText.SetText($"{lives}");
    }

    private void OnDisable()
    {
        _player.OnLifeLost -= UpdateLivesUI;
    }
}