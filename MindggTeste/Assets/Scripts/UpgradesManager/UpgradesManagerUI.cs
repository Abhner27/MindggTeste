using UnityEngine;

[RequireComponent(typeof(UpgradesManager))]
public class UpgradesManagerUI : MonoBehaviour
{
    private UpgradesManager _upgradesManager;

    [SerializeField]
    private TMPro.TextMeshProUGUI _lumberjackerUpgradePriceText, 
        _carrierUpgradePriceText, 
        _carpenterUpgradePriceText;

    [SerializeField]
    private TMPro.TextMeshProUGUI _lumberjackerLevelText,
    _carrierLevelText,
    _carpenterLevelText;

    private void Start()
    {
        _upgradesManager = GetComponent<UpgradesManager>();
        _upgradesManager.OnUpgradePurchased += UpdateUI;
    }

    private void UpdateUI()
    {
        _lumberjackerUpgradePriceText.text = GameManager.Instance.Lumberjacker.UpgradeCost.ToString();
        _lumberjackerLevelText.text = "Lvl. " + GameManager.Instance.Lumberjacker.Level.ToString();

        _carrierUpgradePriceText.text = GameManager.Instance.Carrier.UpgradeCost.ToString();
        _carrierLevelText.text = "Lvl. " + GameManager.Instance.Carrier.Level.ToString();

        _carpenterUpgradePriceText.text = GameManager.Instance.Carpenter.UpgradeCost.ToString();
        _carpenterLevelText.text = "Lvl. " + GameManager.Instance.Carpenter.Level.ToString();
    }

    private void OnDestroy()
    {
        _upgradesManager.OnUpgradePurchased -= UpdateUI;
    }
}