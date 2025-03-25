using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AddItemAnim : MonoBehaviour
{
    [SerializeField]
    private MoveToPostion _moveToPosition;
    [SerializeField]
    private ChangeColorOverTimeEffectUI _iconChangeColorEffect;
    [SerializeField]
    private ChangeColorOverTimeEffectUIText _textChangeColorEffect;
    [SerializeField]
    private Transform _rootItem;

    private Image _iconImage;
    private TextMeshProUGUI _text;

    private Vector2 _startPosition;
    private Color _startColor;

    private void Awake()
    {
        _iconImage = GetComponentInChildren<Image>();
        _text = GetComponentInChildren<TextMeshProUGUI>();

        _startPosition = _rootItem.position + Vector3.up;
        _startColor = _iconImage.color;

        _iconImage.color = Color.clear;
        _text.color = Color.clear;
    }

    public void Play()
    {
        GetComponent<RectTransform>().position = _startPosition;
        _iconImage.color = _startColor;
        _text.color = _startColor;

        _moveToPosition.Move(_startPosition + Vector2.up, 0.5f);

        Color trasnparentColor = _startColor;
        trasnparentColor.a = 0;
        _iconChangeColorEffect.UpdateColor(trasnparentColor, 2f);
        _textChangeColorEffect.UpdateColor(trasnparentColor, 2f);
    }
}