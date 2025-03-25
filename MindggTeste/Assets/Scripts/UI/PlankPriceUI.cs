using UnityEngine;

[RequireComponent(typeof(Carpenter))]
public class PlankPriceUI : MonoBehaviour
{
    private Carpenter _carpenter;
    [SerializeField]
    private TMPro.TextMeshProUGUI _plankPriceText;

    private void Awake()
    {
        _carpenter = GetComponent<Carpenter>();
        _carpenter.OnPlankPriceChanged += UpdateUI;
    }

    private void UpdateUI(int value) => _plankPriceText.text = value.ToString();

    private void OnDestroy()
    {
        _carpenter.OnPlankPriceChanged -= UpdateUI;
    }
}