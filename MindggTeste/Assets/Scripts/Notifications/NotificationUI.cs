using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class NotificationUI : MonoBehaviour //used in the templates GO's
{
    [SerializeField]
    private TextMeshProUGUI _titleText, _descriptionText;
    [SerializeField]
    private Image _iconImage;

    public void UpdateUI(Notification notification)
    {
        if (_titleText != null)
            _titleText.text = notification.Title;
        if (_descriptionText != null)
            _descriptionText.text = notification.Description;
        if (_iconImage != null)
            _iconImage.sprite = notification.Icon;
    }
}
