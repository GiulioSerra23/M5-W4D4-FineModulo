using UnityEngine;
using UnityEngine.UI;

public class EnemyVisualFeedback : MonoBehaviour
{
    [Header ("References")]
    [SerializeField] private FSM_Controller _controller;

    [Header ("Visual Settings")]
    [SerializeField] private Image _icon;
    [SerializeField] private Sprite _chaseSprite;
    [SerializeField] private Sprite _stunnedSprite;
    [SerializeField] private Sprite _searchingSprite;

    private void OnEnable()
    {
        _controller.OnStateChanged += HandleStateChanged;
    }

    public void HandleStateChanged(EnemyState state)
    {
        switch (state)
        {
            case EnemyState.CHASE:
                ChangeSprite(_chaseSprite);
                break;
            case EnemyState.STUNNED:
                ChangeSprite(_stunnedSprite);
                break;
            case EnemyState.SEARCHING:
                ChangeSprite(_searchingSprite);
                break;
            default:
                _icon.gameObject.SetActive(false);
                break;
        }
    }

    private void ChangeSprite(Sprite sprite)
    {
        _icon.sprite = sprite;
        _icon.gameObject.SetActive(true);
    }

    private void OnDisable()
    {
        _controller.OnStateChanged -= HandleStateChanged;
    }
}
