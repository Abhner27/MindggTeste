using TMPro;
using UnityEngine;

[RequireComponent(typeof(Money))]
public class MoneyUI : MonoBehaviour
{
    private Money _money;

    [SerializeField]
    private TextMeshProUGUI _moneyText;

    [SerializeField]
    private TextMeshProUGUI _changedValueText;

    [SerializeField]
    private AddItemAnim _moneyAddItemAnim;

    private void Start()
    {
        _money = GetComponent<Money>();

        UpdateUI(0,0);

        _money.OnValueChanged += UpdateUI;
    }

    private void UpdateUI(int currentAmount, int changedValue)
    {
        _moneyText.text = currentAmount.ToString();

        if (changedValue != 0)
        {
            _changedValueText.text = changedValue.ToString("+0;-0;0");
            _moneyAddItemAnim.Play();
        }
    }

    private void OnDestroy()
    {
        _money.OnValueChanged -= UpdateUI;
    }
}